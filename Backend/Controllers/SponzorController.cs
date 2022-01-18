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
    public class SponzorController : ControllerBase
    {
        public ProjectContext Context{get;set;}
       public SponzorController(ProjectContext context)
       {
           Context = context;
       }

       [Route("PreuzmiSponzore")]
       [HttpGet]
       public ActionResult PreuzmiSponzore()
       {
           return Ok(Context.Sponzori.Select(p=> new{p.ID,p.Naziv,p.Iznos}));
       }

       [Route("DodajSponzor/{naziv}/{iznos}")]
       [HttpPost]
        public async Task<ActionResult> AddSponzor(string naziv, int iznos )
        {
         if (string.IsNullOrWhiteSpace(naziv) || naziv.Length > 25 || Context.Sponzori.Where( p => p.Naziv == naziv ).FirstOrDefault() != null)
                return BadRequest("Unesen naziv nije validan !");      
          if(iznos < 0 | iznos > 1000000)
                return BadRequest("Unesen iznos nije validan !");
         try
            {      
               Sponzor NewSponzor = new Sponzor
               {
                Naziv = naziv,    
                Iznos = iznos
               };
               Context.Sponzori.Add(NewSponzor);
               await Context.SaveChangesAsync();
               return Ok($"Uspesno dodaj sponzor sa nazivom: {NewSponzor.Naziv}");
            }
            catch(Exception e)
            {
              return BadRequest(e.Message);
            }
        }
        
       [Route("UpdateSponzor/{ID}/{iznos}")]
       [HttpPut]
       public async Task<ActionResult> UpdateSponzor (int ID, int iznos)
         {
             if (Context.Sponzori.Where( p => p.ID == ID).FirstOrDefault() == null)
                return BadRequest("Unesen ID sponzora nije validan !");  

             if (iznos < 1000 || iznos > 1000000)
                return BadRequest("Unesen iznos nije validan !");   

            try
            {
             var Existing = Context.Sponzori.Where( p => p.ID == ID).FirstOrDefault();
             Sponzor NewSpozor = new Sponzor
             {
               ID = Existing.ID,
               Iznos = iznos,
               Naziv = Existing.Naziv
             };     

              Context.Sponzori.Remove(Existing);
              Context.Sponzori.Add(NewSpozor);            
              await Context.SaveChangesAsync();
              return Ok($"Uspesno promenjeno iznos sponzora sa ID-em  {Existing.ID}");
            }
            catch(Exception e)
            {
              return BadRequest(e.Message);
            }
         }

        [Route("DeleteSponzor/{ID}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteSponzor (int ID)
         {        
           if(Context.Sponzori.Where(p => p.ID == ID).FirstOrDefault() == null)
           return BadRequest("Uneseni ID nije pronadjen !");
            try    
             {
                 var DelSponzor = Context.Sponzori.Where(p => p.ID == ID).FirstOrDefault();
                 Context.Sponzori.Remove(DelSponzor);
                 await Context.SaveChangesAsync();
                 return Ok($"Delete Tehnologija sa nazivom {ID} je uspesan");
             }
             catch(Exception e)
             {
                  return BadRequest(e.Message);
             }
             
         }
    }
}