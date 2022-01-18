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
    public class TrenerController : ControllerBase
    {
        public ProjectContext Context{get;set;}
       public TrenerController(ProjectContext context)
       {
           Context = context;
       }

       [Route("PreuzmiTrenera")]
       [HttpGet]
       public ActionResult PreuzmiTrenera()
       {
        var RetVal = Context.Treneri
            .Include( p => p.Tim)
            .Where(p => p.Tim == null);
            return Ok(Context.Treneri.Select(p=> new{p.ID,p.Ime,p.Prezime,p.GodineStaza}));//p.Tim == null
       }

       [Route("DodajTrenera/{ime}/{prezime}/{godineStaza}")]
       [HttpPost]
       public async Task<ActionResult> DodajTrenera(string ime, string prezime, int godineStaza) 
       {
            if(string.IsNullOrWhiteSpace(ime) || ime.Length > 25 ) 
                return BadRequest("Uneto ime trenera nije validno !");

            if(string.IsNullOrWhiteSpace(prezime) || prezime.Length > 40) 
                return BadRequest("Uneto prezime trenera nije validno !");

            if(godineStaza < 1 || godineStaza > 10) 
                return BadRequest("Unete godine staza nisu validne !");

            try
            {
                Trener NewTrener = new Trener  // Tim = null jer inicijalno trener nema svoj tim
                {                 
                    Ime = ime,
                    Prezime = prezime,
                    GodineStaza = godineStaza,
                    Tim = null
                };
                Context.Treneri.Add(NewTrener);
                await Context.SaveChangesAsync();
                return Ok($"Uspeno kreiran trener sa ID-em {NewTrener.ID}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
       }

        [Route("DeleteTrener/{ID}")]
        [HttpDelete]
         public async Task<ActionResult> DeleteTrener (int ID)
         {        
           if(Context.Treneri.Where(p => p.ID == ID).FirstOrDefault() == null)
           return BadRequest("Uneseni ID nije pronadjen !");
            try    
             {
                 Context.Treneri.Remove(Context.Treneri.Where(dv => dv.ID == ID).FirstOrDefault());
                 await Context.SaveChangesAsync();
                 return Ok($"Delete trenera sa ID-em {ID} je uspesan");
             }
             catch(Exception e)
             {
                  return BadRequest(e.Message);
             }
             
         }
      
     }  
        
}