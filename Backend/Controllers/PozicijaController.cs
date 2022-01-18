using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PozicijaController : ControllerBase
    {
        public ProjectContext Context{get;set;}
       public PozicijaController(ProjectContext context)
       {
           Context = context;
       }

       [Route("PreuzmiPoziciju")]
       [HttpGet]
       public ActionResult Preuzmi()
       {
           return Ok(Context.Pozicije.Select(p=> new{p.ID,p.Naziv}));
       }

       [Route("DodajPoziciju/{Naziv}")]
       [HttpPost]
       public async Task<ActionResult> DodajPoziciju(string Naziv)
        {      
            if (string.IsNullOrWhiteSpace(Naziv) == true ||  Naziv.Length > 25 || Context.Pozicije.Where(p=>p.Naziv == Naziv).FirstOrDefault() != null)
                return BadRequest("tekst");  
            try
            {        
              Pozicija NewPoz = new Pozicija
              {
                Naziv = Naziv      
              };
           
             Context.Pozicije.Add(NewPoz);
             await Context.SaveChangesAsync();   
              return Ok($"Uspesno dodata pozicija: {NewPoz.Naziv}");        
            }
            catch(Exception e)
            {
              return BadRequest(e.Message);
            }
        }

        
        [Route("DeletePoziciju/{ID}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteIgraca(int ID)
        {
             if(Context.Pozicije.Where( p =>p.ID  == ID).FirstOrDefault() == null)
                return BadRequest("Tekst");
             try    
             {
                var DelPoz = Context.Pozicije.Where(p =>p.ID  == ID).FirstOrDefault();
                Context.Pozicije.Remove(DelPoz);
                await Context.SaveChangesAsync();
                return Ok($"Delete pozicije sa ID-em: {ID} je uspesan");
             }
             catch(Exception e)
             {
                return BadRequest(e.Message);
             }
        }
    }
}