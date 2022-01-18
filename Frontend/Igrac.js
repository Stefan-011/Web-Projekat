export class Igrac
{
    constructor(Ime,Prezime,Nadimak,GodineStaza,PozicijaIg)
    {
        this.Ime = Ime;
        this.Prezime = Prezime
        this.Nadimak = Nadimak;
        this.GodineStaza = GodineStaza;
        this.PozicijaIg = PozicijaIg;
    }
    dodajGrafiku(instanca,max)
    {
        let Kockica = document.getElementsByClassName("Kockica/"+instanca);  
        for( let i = 0 ; i < max ; i++)
        {              
            if(Kockica[i].innerHTML == "Prazno")         
            {   
                let IMG = document.createElement('img');
                IMG.src = "assets/player.png";
                IMG.className = "IgracImage";
                Kockica[i].innerHTML = this.Nadimak +"<br>" +this.PozicijaIg.Naziv+"<br>";
                Kockica[i].appendChild(IMG);
                Kockica[i].className = ""+this.Nadimak+"/"+instanca;               
                i = max;
            }        
        }
    }
    obrisiGrafiku(instanca,max,nadimak)
    {
        let Kockica = document.getElementsByClassName(nadimak+"/"+instanca); 
        Kockica[0].innerHTML = "Prazno";
        Kockica[0].className = "Kockica/"+instanca;
    }
    UpdateGrafiku(instanca,max,nadimak,NewPozicija)
    {
        let IMG = document.createElement('img');
        IMG.src = "assets/player.png";
        IMG.className = "IgracImage";
        let Kockica = document.getElementsByClassName(nadimak+"/"+instanca);         
        Kockica[0].innerHTML = nadimak+"<br>"+NewPozicija +"<br>";
        Kockica[0].appendChild(IMG);         
    }
    popuniTabelu(host)
    {
        var tr = document.createElement("tr");
        host.appendChild(tr);

        var el = document.createElement("td");
        el.innerHTML=this.Nadimak;
        tr.appendChild(el);

        var el = document.createElement("td");
        el.innerHTML=this.Ime;
        tr.appendChild(el);

        el = document.createElement("td");
        el.innerHTML=this.Prezime;
        tr.appendChild(el);

        el = document.createElement("td");
        el.innerHTML=this.GodineStaza;
        tr.appendChild(el);
    }
}