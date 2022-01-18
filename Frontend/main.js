import { ETeam } from "./ETeam.js";
import { Pozicija } from "./Pozicija.js";
import { Sponzor } from "./Sponzor.js";
import { Trener } from "./Trener.js";

var PozocijaList = [];
var SponzorList = [];
var TrenerList = [];
fetch("https://localhost:5001/Pozicija/PreuzmiPoziciju")
.then(p=>{
    p.json().then(Th=>{
        Th.forEach(element => {
            
            var obj = new Pozicija(element.id,element.naziv);
            PozocijaList.push(obj);      
        });  
    })
    fetch("https://localhost:5001/Sponzor/PreuzmiSponzore")
        .then(p=>{
             p.json().then(Th=>{
              Th.forEach(element => {
                var obj = new Sponzor(element.id,element.naziv,element.iznos);
                SponzorList.push(obj);
             });
            })        
         fetch("https://localhost:5001/Trener/PreuzmiTrenera")
         .then(p=>{
         p.json().then(Th=>{
         Th.forEach(element => {
         var obj = new Trener(element.id,element.ime,element.prezime,element.godineStaza);
          TrenerList.push(obj);
            });
            for( let i = 1; i <= 2; i++)
            {
                var f = new ETeam(PozocijaList,SponzorList,TrenerList,i); 
                f.setKont(document.body);  
                 if( i == 1)
                   f.crtajHeader();
                f.crtajCont();
            }
            })
        });
    })  
})