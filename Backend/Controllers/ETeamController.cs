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
    public class ETeamController : ControllerBase
    {
        public ProjectContext Context{get;set;}
       public ETeamController(ProjectContext context)
       {
           Context = context;
       }
       [Route("Preuzmi/{Naziv}/{MaxIgraci}")]
       [HttpGet]
       public async Task<ActionResult> Preuzmi(string Naziv , int MaxIgraci)
       {
            if (Context.ETeamovi.Where(p => p.Naziv == Naziv).FirstOrDefault() == null)
                return BadRequest("Uneseno ime nije validno !");
                if (MaxIgraci < 5 || MaxIgraci > 10)
                return BadRequest("Unesen maksimalan broj clanova tima je izvan opsega !");

            var RetVal = Context.ETeamovi
            .Include(p => p.Spozor)
            .Include(p => p.Igraci)
            .Where(p => p.Naziv == Naziv)
            .Where(p => p.MaxIgraci == MaxIgraci);
            try
            {
                return Ok( await RetVal.Select( p => new {p.Naziv,p.MaxIgraci,p.Spozor}).ToListAsync());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
       }

       [Route("DodajETeam")]
       [HttpPost]
       public async Task<ActionResult> DodajETeam([FromBody] ETeam Tim)  
        {
             if (Tim.MaxIgraci < 5 || Tim.MaxIgraci > 10)
                return BadRequest("Unesen maksimalan broj clanova tima je izvan opsega !");

            if (string.IsNullOrWhiteSpace(Tim.Naziv) || Tim.Naziv.Length > 20 || Context.ETeamovi.Where(p => p.Naziv == Tim.Naziv).FirstOrDefault() != null)
                return BadRequest("Uneseno ime nije validno !");

            try 
            {
                Context.ETeamovi.Add(Tim); 
                await Context.SaveChangesAsync();
                return Ok($"Uspesno dodat tim sa nazivom {Tim.Naziv}");    
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
  
        }
       
       [Route("DodajETeam{naziv}/{maxIgraci}/{SpozorID}/{TrenerID}")] 
       [HttpPost]
       public async Task<ActionResult> DodajETeam(string naziv,int maxIgraci,int SpozorID,int TrenerID)  
        {

             var TimVal = Context.Treneri
            .Include( p => p.Tim)
            .Where(p => p.Tim == null)
            .Where(p => p.ID == TrenerID);

             if (maxIgraci < 5 || maxIgraci > 10)
                return BadRequest("Unesen maksimalan broj clanova tima je izvan opsega !");

            if (string.IsNullOrWhiteSpace(naziv) || naziv.Length > 20 || Context.ETeamovi.Where(p => p.Naziv == naziv).FirstOrDefault() != null)
                return BadRequest("Uneseno ime nije validno !");

            if(Context.Sponzori.Where(p =>p.ID == SpozorID).FirstOrDefault() == null)
                return BadRequest("Uneti ID ne odgovara ni jednom sponzoru !s");   

            if(Context.Treneri.Where(p=>p.ID == TrenerID).FirstOrDefault() == null)
                return BadRequest("ID trenera nije validan !"); 

            if(TimVal.FirstOrDefault() == null)
                return BadRequest("Trener da unetim ID-em je ima tim !"); 
            try 
            {
                ETeam NewTim = new ETeam
                {
                    Naziv = naziv,
                    MaxIgraci = maxIgraci,
                    Spozor = Context.Sponzori.Where(p =>p.ID == SpozorID).FirstOrDefault(),
                };
                Trener NewTrener = new Trener
                {
                    ID = TrenerID,
                    Ime = Context.Treneri.Where(p=>p.ID == TrenerID).FirstOrDefault().Ime,
                    Prezime = Context.Treneri.Where(p=>p.ID == TrenerID).FirstOrDefault().Prezime,
                    Tim = NewTim,
                };

                Context.Treneri.Remove(Context.Treneri.Where(p=>p.ID == TrenerID).FirstOrDefault());
                Context.ETeamovi.Add(NewTim);
                Context.Treneri.Add(NewTrener); 
                await Context.SaveChangesAsync();
                return Ok($"Uspesno dodat E-team sa nazivom {naziv} {NewTrener.Tim.Naziv}"); 
            }
            catch(Exception e)
            {
               return BadRequest(e.Message);
            }    
        }

        [Route("UpdateETeam/{TeamID}/{naziv}")] 
        [HttpPut]
        public async Task<ActionResult> UpdateETeam (int TeamID, string naziv)
        {
                if (Context.ETeamovi.Where(p =>p.ID == TeamID).FirstOrDefault() == null)
                return BadRequest("Uneseni ID ne odgovara ni jednom E-timu !");

              if (string.IsNullOrWhiteSpace(naziv) && (naziv.Length > 20 || Context.ETeamovi.Where(p => p.Naziv == naziv).FirstOrDefault() == null))
                return BadRequest("Uneseno ime nije validno !");
           
                    try
                    {
                        ETeam NewETeam = new ETeam
                        {
                            ID = TeamID,
                            Naziv = naziv,
                            MaxIgraci = Context.ETeamovi.Where(p => p.ID == TeamID).FirstOrDefault().MaxIgraci
                        };
                     Context.ETeamovi.Remove(Context.ETeamovi.Where(p => p.ID == TeamID).FirstOrDefault());
                     Context.ETeamovi.Add(NewETeam);
                     await Context.SaveChangesAsync();
                     return Ok($"Update E-teama sa ID: {TeamID}");
                    }
                    catch(Exception e)
                    {
                        BadRequest(e.Message);
                    }


            await Context.SaveChangesAsync();
            return Ok();
        }

        [Route("DodajIgraceTim/{NazivTima}/{Nadimak}/{pozicijaID}")]
        [HttpPut]
        public async Task<ActionResult> DodajIgraceTim (string NazivTima, string Nadimak,int pozicijaID)
        {
          
           
           if(Context.ETeamovi.Where( p => p.Naziv == NazivTima).FirstOrDefault() == null)
            return BadRequest("Uneto ime E-tima ne odgovara ni jednom E-timu !");
           if(Context.Igraci.Where( p => p.Nadimak == Nadimak).FirstOrDefault() == null)
            return BadRequest("Uneti nadimak igraca ne odgovara ni jednom igracu !");
          if(Context.Igraci.Where( p => p.Nadimak == Nadimak).FirstOrDefault().Tim != null )
            return BadRequest("Uneti igrac je vec ima E-tim !");

            try
            {
                var RetVal = Context.Igraci
                 .Include( p => p.Pozicija)
                 .Where(p => p.Pozicija.ID == pozicijaID)
                 .Where(p => p.Tim == null)
                 .Where(p => p.Nadimak == Nadimak); 
                var ExIg = RetVal.FirstOrDefault();               
                var ExTim = Context.ETeamovi.Where( p => p.Naziv == NazivTima).FirstOrDefault();             
                Igrac NewIgrac = new Igrac
              {
                ID = ExIg.ID,
                Ime = ExIg.Ime,
                Prezime = ExIg.Prezime,
                Nadimak = ExIg.Nadimak,
                GodineStaza = ExIg.GodineStaza ,
                Pozicija = Context.Pozicije.Where(p=>p.ID == ExIg.Pozicija.ID).FirstOrDefault(),
                Tim = ExTim
              };
                Context.Igraci.Remove(ExIg);
                 Context.Igraci.Add(NewIgrac);                             
                await Context.SaveChangesAsync();
                return Ok($"Uspesno dodat igrac za nadimkom {NewIgrac.Nadimak}");
              
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
       
        [Route("DeleteETim/{naziv}")]
        [HttpDelete]
         public async Task<ActionResult> DeleteETim (string naziv)
         {        
           if(Context.ETeamovi.Where(p => p.Naziv == naziv).FirstOrDefault() == null)
           return BadRequest("ID E-tima nije pronadjen !");
            try    
             {
                  
                 if(Context.Treneri.Where(p => p.Tim.Naziv == naziv).FirstOrDefault() != null)           
                 Context.Treneri.Where(p => p.Tim.Naziv == naziv).FirstOrDefault().Tim = null;
                if(Context.Igraci.Where(p => p.Tim.Naziv == naziv).ToList() != null)
                {
                 var array = Context.Igraci.Where(p => p.Tim.Naziv == naziv).ToList();
                
                 foreach(var el in array)
                 {
                     el.Tim = null;
                 }
                }
               
                 Context.ETeamovi.Remove(Context.ETeamovi.Where(p => p.Naziv == naziv).FirstOrDefault());
                 await Context.SaveChangesAsync();
                 return Ok($"Delete E-tima sa nazivom {naziv} je uspesan"); 
             }
             catch(Exception e)
             {
                  return BadRequest(e.Message);
             }
             
         }
    }
    
}