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
    public class IgracController : ControllerBase
    {
        public ProjectContext Context{get;set;}
       public IgracController(ProjectContext context)
       {
           Context = context;
       }

       [Route("Preuzmi/{PozicijaID}")]
       [HttpGet]
       public async Task<ActionResult> Preuzmi(int PozicijaID)
       {
          if (Context.Pozicije.Where(p => p.ID == PozicijaID).FirstOrDefault() == null)
                return BadRequest("Uneta pozicija nije validna !");

            var RetVal = Context.Igraci
            .Include( p => p.Pozicija)
            .Where(p => p.Pozicija.ID == PozicijaID)
            .Where(p => p.Tim == null);
            try
            {
                return Ok(await RetVal.Select( p => new {p.Ime,p.Nadimak,p.Prezime,p.Pozicija.Naziv,p.GodineStaza}).ToListAsync());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
       }
       
       [Route("PreuzmiIgraca/{Nadimak}")]
       [HttpGet]
       public async Task<ActionResult> PreuzmiIgraca(string Nadimak)
       {
            if (Context.Igraci.Where(p => p.Nadimak == Nadimak).FirstOrDefault() == null)
                return BadRequest("Uneti nadimak igraca nije validan !");

             var RetVal = Context.Igraci
             .Include( p => p.Pozicija)
             .Where(p => p.Nadimak == Nadimak);

             try
            {
                return Ok(await RetVal.Select( p => new {p.Ime,p.Nadimak,p.Prezime,p.Pozicija.Naziv,p.GodineStaza}).ToListAsync());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
       }
       
       [Route("PreuzmiIgraca/{Nadimak}/{TeamName}")] /// Treba prerada na kraju
       [HttpGet]
       public async Task<ActionResult> PreuzmiIgraca(string Nadimak,string TeamName)
       {
            if (Context.Igraci.Where(p => p.Nadimak == Nadimak).FirstOrDefault() == null)
                return BadRequest("Uneti nadimak igraca nije validan !");
            if (Context.ETeamovi.Where(p => p.Naziv == TeamName).FirstOrDefault() == null)
                return BadRequest("Uneto ime Etima nije validno !");

             var RetVal = Context.Igraci
             .Include( p => p.Pozicija)
             .Where(p => p.Nadimak == Nadimak)
             .Where(p => p.Tim == Context.ETeamovi.Where(p => p.Naziv == TeamName).FirstOrDefault());

             try
            {
                return Ok(await RetVal.Select( p => new {p.Ime,p.Nadimak,p.Prezime,p.Pozicija.Naziv,p.GodineStaza}).ToListAsync());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
       }

       [Route("DodajIgraca")]
       [HttpPost]
       public async Task<ActionResult> DodajIgraca([FromBody] Igrac Igrac)
        {  
                bool isDigitIme = Igrac.Ime.Any(c => char.IsDigit(c));
                bool isDigitPrezime = Igrac.Prezime.Any(c => char.IsDigit(c));
            if (string.IsNullOrWhiteSpace(Igrac.Ime)|| Igrac.Ime.Length > 20 || isDigitIme == true)
                return BadRequest("Uneto ime igraca nije validno !");  
            if (string.IsNullOrWhiteSpace(Igrac.Prezime) || Igrac.Prezime.Length > 40 || isDigitPrezime == true)
                return BadRequest("Uneto prezime nije validno !");    
            if (string.IsNullOrWhiteSpace(Igrac.Nadimak) || Igrac.Nadimak.Length > 40 || Context.Igraci.Where(p=>p.Nadimak == Igrac.Nadimak).FirstOrDefault() != null)
                return BadRequest("Uneti nadimak nije validan !");  
            if(Igrac.GodineStaza < 1 || Igrac.GodineStaza > 10 )
                return BadRequest("Unete godine staza nisu validne !"); 
            if(Context.Pozicije.Where(p=>p.ID == Igrac.Pozicija.ID).FirstOrDefault() == null)
                return BadRequest("Uneta pozicija ne postoji !");
            try
            {        
              Igrac NewIgrac = new Igrac
              {
                ID = Igrac.ID,
                Ime = Igrac.Ime,
                Prezime =Igrac.Prezime,
                Nadimak =Igrac.Nadimak,
                GodineStaza =Igrac.GodineStaza ,
                Pozicija= Context.Pozicije.Where(p=>p.ID == Igrac.Pozicija.ID).FirstOrDefault()
              
              };
           
             Context.Igraci.Add(NewIgrac);
             await Context.SaveChangesAsync();   
              return Ok($"Uspesno dodat igrac sa nadimkom: {NewIgrac.Nadimak}");        
            }
            catch(Exception e)
            {
              return BadRequest(e.Message);
            }
        }
        
    [Route("DodajIgraca/{Ime}/{Prezime}/{Nadimak}/{GodineStaza}/{PozicijaID}/{NazivTima}")]
       [HttpPost]
       public async Task<ActionResult> DodajIgraca(string Ime, string Prezime, string Nadimak, int GodineStaza,int PozicijaID,string NazivTima) ///TeamId je jos u razmatranji
        {      
            bool isDigitIme = Ime.Any(c => char.IsDigit(c));
            bool isDigitPrezime = Prezime.Any(c => char.IsDigit(c));

            if (string.IsNullOrWhiteSpace(Ime) || Ime.Length > 20 || isDigitIme == true)
                return BadRequest("Uneto ime igraca nije validno !");  
            if (string.IsNullOrWhiteSpace(Prezime) || Prezime.Length > 40 || isDigitPrezime == true)
                return BadRequest("Uneto prezime nije validno !");    
            if (string.IsNullOrWhiteSpace(Nadimak) || Nadimak.Length > 40 || Context.Igraci.Where(p=>p.Nadimak == Nadimak).FirstOrDefault()!= null)
                return BadRequest("Uneti nadimak nije validan !");   
            if(GodineStaza < 1 || GodineStaza > 10 )
                return BadRequest("Unete godine staza nisu validne !"); 
            if(Context.Pozicije.Where(p=>p.ID == PozicijaID).FirstOrDefault() == null)
                return BadRequest("Uneta pozicija ne postoji !");
            if(Context.ETeamovi.Where(p=>p.Naziv == NazivTima).FirstOrDefault() == null)
                return BadRequest("Uneto ime E-tima ne odgovara ni jednom timu !");
            try
            {        
              Igrac NewIgrac = new Igrac
              {
                Ime = Ime,
                Prezime = Prezime,
                Nadimak = Nadimak,
                GodineStaza = GodineStaza ,
                Pozicija = Context.Pozicije.Where(p=>p.ID == PozicijaID).FirstOrDefault(),
                Tim = Context.ETeamovi.Where(p=>p.Naziv == NazivTima).FirstOrDefault()          
              };
            
             Context.Igraci.Add(NewIgrac);
             await Context.SaveChangesAsync();       
              return Ok($"Uspesno dodat igrac sa nadimkom: {NewIgrac.Nadimak}");    
            }
            catch(Exception e)
            {
              return BadRequest(e.Message);
            }
        }


        [Route("UpdateIgraca/{Nadimak}/{PozicijaID}")]
        [HttpPut]
        public async Task<ActionResult> UpdateIgraca(string Nadimak,int PozicijaID )
        {

             if (string.IsNullOrWhiteSpace(Nadimak) || Nadimak.Length > 20 || Context.Igraci.Where(p=>p.Nadimak == Nadimak).FirstOrDefault() == null)
                return BadRequest("Uneti nadimak nije validan !");  

             if(Context.Pozicije.Where(p=>p.ID == PozicijaID).FirstOrDefault() == null)
                return BadRequest("Uneta pozicija nije validna !");

            try
            {
                var Existing = Context.Igraci.Where(p=>p.Nadimak == Nadimak).FirstOrDefault();
    
                Igrac NewIgrac = new Igrac
                {
                ID = Existing.ID,
                Ime = Existing.Ime,
                Prezime = Existing.Prezime,
                Nadimak = Existing.Nadimak,
                GodineStaza = Existing.GodineStaza ,
                Pozicija = Context.Pozicije.Where(p=>p.ID == PozicijaID).FirstOrDefault()
                };    
                Context.Igraci.Remove(Existing);
                Context.Igraci.Add(NewIgrac);            
                await Context.SaveChangesAsync();
                return Ok($"Update igraca sa nadimkom: {NewIgrac.Nadimak} je uspesan");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message); 
            }    
        }   

        
        [Route("UpdateIgraca/{Nadimak}/{PozicijaID}/{NazivTima}")]
        [HttpPut]
        public async Task<ActionResult> UpdateIgracaTim(string Nadimak,int PozicijaID,string NazivTima )
        {

             if (string.IsNullOrWhiteSpace(Nadimak) || Nadimak.Length > 20 || Context.Igraci.Where(p=>p.Nadimak == Nadimak).FirstOrDefault() == null)
                return BadRequest("Uneti nadimak nije validan !");  

             if(Context.Pozicije.Where(p=>p.ID == PozicijaID).FirstOrDefault() == null)
                return BadRequest("Uneta pozicija nije validna !");

             if(Context.ETeamovi.Where(p=>p.Naziv == NazivTima).FirstOrDefault()==null)
                return BadRequest("Uneti naziv tima nije validan !");

            try
            {
                var Existing = Context.Igraci.Where(p=>p.Nadimak == Nadimak).FirstOrDefault();
    
                Igrac NewIgrac = new Igrac
                {
                ID = Existing.ID,
                Ime = Existing.Ime,
                Prezime = Existing.Prezime,
                Nadimak = Existing.Nadimak,
                GodineStaza = Existing.GodineStaza ,
                Pozicija = Context.Pozicije.Where(p=>p.ID == PozicijaID).FirstOrDefault(),
                Tim = Context.ETeamovi.Where(p=>p.Naziv == NazivTima).FirstOrDefault()
                };    
                Context.Igraci.Remove(Existing);
                Context.Igraci.Add(NewIgrac);            
                await Context.SaveChangesAsync();
                return Ok($"Update igraca sa nadimkom: {NewIgrac.Nadimak} je uspesan");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message); 
            }    
        }
        
        [Route("DeleteIgraca/{Nadimak}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteIgraca(string Nadimak)
        {
             if(Context.Igraci.Where( p =>p.Nadimak  == Nadimak).FirstOrDefault() == null)
                return BadRequest("Uneti nadimak nije validan !");  
             try    
             {
                var Dev = Context.Igraci.Where(p =>p.Nadimak  == Nadimak).FirstOrDefault();
                Context.Igraci.Remove(Dev);
                await Context.SaveChangesAsync();
                return Ok($"Delete igraca sa nadimkom: {Nadimak} je uspesan");
             }
             catch(Exception e)
             {
                return BadRequest(e.Message);
             }
        }
        
        [Route("DeleteIgraca/{Nadimak}/{ImeTima}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteIgracaTim(string Nadimak,string ImeTima)
        {
            if(Context.Igraci.Where( p =>p.Nadimak  == Nadimak).FirstOrDefault() == null)
                return BadRequest("Uneti nadimak nije validan !");
            if(Context.ETeamovi.Where( p =>p.Naziv  == ImeTima).FirstOrDefault() == null || Context.Igraci.Where( p =>p.Nadimak  == Nadimak).FirstOrDefault().Tim.Naziv != ImeTima)
                return BadRequest("Uneto ime tima ne odgovara timu igraca !");    
             try    
             {
                 var Dev = Context.Igraci.Where(p =>p.Nadimak  == Nadimak).FirstOrDefault();
                 Context.Igraci.Remove(Dev);
                 await Context.SaveChangesAsync();
                 return Ok($"Delete igraca sa nadimkom: {Nadimak} je uspesan");
             }
             catch(Exception e)
             {
                return BadRequest(e.Message);
             }
        }
    }
}
