import { Igrac } from "./Igrac.js";
import { Pozicija } from "./Pozicija.js";
export class ETeam
{
    constructor(PozicijaList,SponzorList,TrenerList,Instanca)
    {
        this.PozicijaList = PozicijaList;
        this.SponzorList = SponzorList;
        this.Kont = null;
        this.ImeETima = null;
        this.Max = null;
        this.TrenutniBr = null;
        this.Sponzor = null;
        this.Instanca = Instanca;
        this.TrenerList = TrenerList;
        this.Trener = null;
    }

    setKont(host) // Postavljanje host-a
    {
        this.Kont = document.createElement("div"); 
        this.Kont.classList.add("body_div","body_div"+this.Instanca );
        host.appendChild(this.Kont); 
    }

    crtajHeader()  // Crtanje header-a
    {
       
        let kontNav = document.createElement("div");
        kontNav.classList.add( "navbar","navbar"+this.Instanca );
        this.Kont.appendChild(kontNav);
        
        let kontLogo = document.createElement("div");
        kontLogo.classList.add( "logo_div","logo_div"+this.Instanca );
        kontNav.appendChild(kontLogo);

        let kontInfo = document.createElement("div");
        kontInfo.classList.add( "info_div","info_div"+this.Instanca );
        kontNav.appendChild(kontInfo);

        let kontImg = document.createElement('img');
        kontImg.classList.add( "logo_img","logo_img"+this.Instanca );
        kontImg.src = "assets/Logo.png";
        kontLogo.appendChild(kontImg); 

        let p1 = document.createElement("p");
        p1.innerHTML = " This site is my personal project for the web programming class which can demonstrate my designer and programmer capabilities.<br>-Stefan Stanimirovic 17942-";
        kontInfo.appendChild(p1);      
    }

crtajCont() // Crtanje content-a
{
    
       let KontContent = document.createElement('div');
       KontContent.classList.add( "content","content"+this.Instanca );
       this.Kont.appendChild(KontContent);

       let KontButtDiv = document.createElement('div');
       KontButtDiv.classList.add( "button_div","button_div"+this.Instanca );
       KontContent.appendChild(KontButtDiv);

       let KontWorkSp = document.createElement('div');
       KontWorkSp.classList.add( "workspace",""+this.Instanca );
       KontContent.appendChild(KontWorkSp);
       this.DodajWorkspace(KontWorkSp);
}

DodajWorkspace(host) // Crtanje worspace-a
{
  let KontAddForm = document.createElement('div'); // Div za CRUD operacije
  KontAddForm.classList.add("splitted","CRUD_Form","CRUD_Form"+this.Instanca );
  host.appendChild(KontAddForm);

  let Team = document.createElement('div'); // Div za informacije o timu
  Team.classList.add("splitted","TeamInfoForm","TeamInfoForm"+this.Instanca );
  host.appendChild(Team);

  let Izbaze = document.createElement('div'); // Div za biranje iz baze podataka
  Izbaze.classList.add("splitted","ModifyForm","ModifyForm"+this.Instanca );
  host.appendChild(Izbaze);

  // Definisanje labela i njihovog izlgeda i teksta
  let Timbreak = document.createElement("label");
  let lIme = document.createElement("label");
  let lSize = document.createElement("label");
  Timbreak.className = "40"+this.Instanca;
  lIme.className = "40"+this.Instanca;
  lSize.className = "40"+this.Instanca;
  lIme.innerHTML = "Ime:";
  lSize.innerHTML = "Velicina tima:";
  Timbreak.innerHTML = "E-Tim:";
  Timbreak.style.marginTop = '20px';
  Timbreak.style.marginBottom = '10px';
  Timbreak.style.fontSize = '20px';

  // Definisanje input-a
  let ImeInput = document.createElement("input");
  let TeamSizeInput = document.createElement("input");
  TeamSizeInput.type = "Number";
  
  KontAddForm.appendChild(Timbreak);

  KontAddForm.appendChild(lIme);
  KontAddForm.appendChild(ImeInput);

  KontAddForm.appendChild(lSize);
  KontAddForm.appendChild(TeamSizeInput);

  // Kreiranje select listi sponzora i trenera
  let l =  document.createElement("label");
  l.innerHTML= "Sponzor";
  l.className = "40"+this.Instanca;
  KontAddForm.appendChild(l);

  let ListT =  document.createElement("select");
  ListT.classList.add("SelectList","SelectList"+this.Instanca );
  KontAddForm.appendChild(ListT);

  this.SponzorList.forEach(el=>{
    let op = document.createElement("option");
    op.innerHTML = el.Naziv;
    op.value = el.ID;
    ListT.appendChild(op);  
  });

  
  let l2 =  document.createElement("label");
  l2.innerHTML= "Trener";
  l2.className = "40"+this.Instanca;
  KontAddForm.appendChild(l2);

  let ListT2 =  document.createElement("select");
  ListT2.classList.add("SelectList","SelectList"+this.Instanca);
  KontAddForm.appendChild(ListT2);

  this.TrenerList.forEach(el=>{
    let op = document.createElement("option");
    op.innerHTML = el.Ime + " " + el.Prezime;
    op.value = el.ID;
    ListT2.appendChild(op);
  });

   let SubTim = document.createElement("button");
   SubTim.type = "Submit";
   SubTim.classList.add("SubmitBtn","SubmitBtn"+this.Instanca );
   SubTim.innerHTML = "Kreiraj";
   SubTim.onclick = (ev) =>{ 
    this.DodajETim(ImeInput,TeamSizeInput,ListT,ListT2);   
   };
   KontAddForm.appendChild(SubTim);
}

DodajETim(Ime,Size,Spon,Tren) // Dodavanje E-tima
{

    if(Ime.value.length > 20 || Ime.value.length === 0 )
    {
        alert("Ime tima nije validno !");
        return false;
    }
    if(Size.value < 5 || Size.value  > 10)
    {
        alert("Velicina tima nije validna !");
        return false;
    }
    if(Spon.value  <= 0)
    {
        alert("Izaberite sponzora tima !");
        return false;
    }
    let ime = Ime.value;
    let size = Size.value;
    let spon = Spon.value;
    
    var t = fetch("https://localhost:5001/ETeam/DodajETeam"+ime+"/"+size+"/"+spon+"/"+Tren.value,
    {
        method:"POST"
    }).then(s=>{
        if(s.ok)
          {
            alert("Uspesno dodat E-tim");
            this.CrtanjeSetup(Ime,Size,Spon,Tren);
          }
                                     
        else
         {
            alert("Doslo je do greske tim nije kreiran");  
         }
      }
        );
    return true;
}

CrtanjeSetup(Ime,Size,Spon,Tren) // F-ja za poretanje crtanja potrebnih formi
{  
    if(this.DodajInfomacijeOTimu(Ime.value,Size.value,Spon.value,Tren.value) == true)            
  {
    this.ObrisiAddETeam(Ime,Size,Spon,Tren);
    this.crtajAddIgrac(); 
    this.CrtajFormuIgraca();
  }
}

ObrisiAddETeam(input1,input2,input3,input4) // Brisanje unosa i labela za unos E-tima
{
   var V = document.getElementsByClassName("40"+this.Instanca);
    V = [].slice.call(V)
    V.forEach(el =>{
        el.remove();
        });
    input1.remove();
    input2.remove();
    input3.remove();
    input4.remove();
    V = document.getElementsByClassName("SubmitBtn"+this.Instanca);
    V[0].remove();  
}

DodajInfomacijeOTimu(ime,size,spon,tren) // Postavljanje neophodnih vrednosti atributa
{
    this.ImeETima = ime;
    this.Max = size;
    this.TrenutniBr = 0;
    this.TrenerList.forEach(el=>{
        if(el.ID == tren)
           this.Trener = el;
       })  
      
    this.SponzorList.forEach(el=>{
     if(el.ID == spon)
        this.Sponzor = el.Naziv;
    })
    return true;
}

CrtajFormuIgraca() // Zrta formu za informacije E-tima
{
  let v = document.getElementsByClassName("TeamInfoForm"+this.Instanca );
  let host = v[0];
  let InfoDiv = document.createElement("div");
  InfoDiv.classList.add("InfoTeam","InfoTeam"+this.Instanca );
  host.appendChild(InfoDiv);

  let lImeTima = document.createElement("label");
  lImeTima.innerHTML = "Ime E-Tima:"+" "+this.ImeETima;

  let lImeSponzor = document.createElement("label");
  lImeSponzor.innerHTML = "Sponzor E-Tima:"+" "+this.Sponzor;

  let lTrener = document.createElement("label");
  lTrener.innerHTML = "Trener:"+" "+ this.Trener.Ime +" " + this.Trener.Prezime ;

  InfoDiv.append(lImeTima);
  InfoDiv.append(lImeSponzor);
  InfoDiv.append(lTrener);

  let TeamVisual = document.createElement("div");
  TeamVisual.classList.add("TeamVisual_Div","TeamVisual_Div"+this.Instanca );
  host.appendChild(TeamVisual);
  for(let i = 0 ; i < this.Max ;i++)
  {
      let pd = document.createElement("div");
      pd.classList.add("IgracDiv",""+this.Instanca);
      TeamVisual.appendChild(pd);

      let l = document.createElement("label");
      l.classList.add("Kockica/"+this.Instanca)
      l.innerHTML = "Prazno";
      pd.appendChild(l);
  }

  v = document.getElementsByClassName("ModifyForm"+this.Instanca);
  let host2 = v[0];

  let lObjasnjenje = document.createElement("label");
  lObjasnjenje.innerHTML = "Unesite poziciju igraca koju zelite u svom timu, zatim nadimak igraca koga ste odabrali.";
  lObjasnjenje.style.outline = "solid 2px black";
  lObjasnjenje.style.margin = "5px 5px";
  lObjasnjenje.style.padding = "2px 2px";
  host2.appendChild(lObjasnjenje);

  let lNadimak = document.createElement("label");
  lNadimak.innerHTML = "Nadimak";
  host2.appendChild(lNadimak);

  let inputNadimak = document.createElement("input");
  inputNadimak.classList.add("InputNadimak",""+this.Instanca );
  host2.appendChild(inputNadimak);


  let l =  document.createElement("label");
  l.innerHTML= "Pozicija";
  l.className = "40"+this.Instanca;
  host2.appendChild(l);

  let ListT =  document.createElement("select");
  ListT.classList.add("SelectList","SelectList"+this.Instanca );
  ListT.onchange = (ev) =>
  {
    this.VratiIgrace(ListT.value);
  }
  
  host2.appendChild(ListT);

  this.PozicijaList.forEach(el=>{
    let op = document.createElement("option");
    op.innerHTML = el.Naziv;
    op.value = el.ID;
    ListT.appendChild(op);
  });
    this.VratiIgrace(ListT.value);

   let SubTim = document.createElement("button");
   SubTim.type = "Submit";
   SubTim.classList.add("SubmitBtn","SubmitBtn"+this.Instanca );
   SubTim.innerHTML = "Dodaj";
   SubTim.onclick = (ev) =>{ 
    if(this.dodajIgracaTim(inputNadimak,ListT) == true)
    this.VratiIgrace(ListT.value);
   }
   host2.appendChild(SubTim);
}

dodajIgracaTim(Nadimak,Poz)
{
    if(Nadimak.value.lenght > 20 || Nadimak.value.lenght < 0)
    {
        alert("Nadimak igraca nije validan !");
        return false;
    }
    fetch("https://localhost:5001/ETeam/DodajIgraceTim/"+this.ImeETima+"/"+Nadimak.value+"/"+Poz.value,
    {
        method:"PUT"
    }).then(s=>{
        if(s.ok)
        {
            let Pozicija;
            this.PozicijaList.forEach(el => {
                if(el.ID == Poz.value)
                Pozicija = el
            });
            const obj = new Igrac(" "," ",Nadimak.value,0,Pozicija);
            obj.dodajGrafiku(this.Instanca,this.Max);
            this.increaseNum();
        }           
        else
        alert("Doslo je do greske igrac nije dodat");
    });
    return true;
}

VratiIgrace(PozicijaID)
{
    if(PozicijaID < 0 )
    {
        alert("Pozicija nije validna !");
        return false;
    }
    fetch("https://localhost:5001/Igrac/Preuzmi/"+PozicijaID,
    {
        method:"GET"
    }).then(s=>{
        if(s.ok)
        { 
            let Tablebody = this.CrtajTabelu();
            let ColNames=["Nadimak", "Ime", "Prezime", "GodineStaza"];
            ColNames.forEach(el=>{
                let head = document.createElement("th");
                head.innerHTML=el;
                Tablebody.appendChild(head);
            });
            s.json().then(Th=>{
                Th.forEach(element => {
                    var Pboj = new Pozicija(PozicijaID,element.naziv);
                    const obj = new Igrac(element.ime,element.prezime,element.nadimak,element.godineStaza,Pboj);
                    obj.popuniTabelu(Tablebody); 
                }); 
            });
        }           
        else
        alert("Doslo je do greske, igraci sa ovom pozicijom se ne nalaze u nasoj evidenciji");
    }
        );
        return true;
}

CrtajTabelu() // Crtanje forme za preuzimanje igraca iz baze
{
    let host = document.getElementsByClassName("ModifyForm"+this.Instanca);
    var tabela = document.getElementsByClassName("PlayerTable/"+this.Instanca);
    tabela = tabela[0];
    if(tabela != null)
    tabela.remove();

    host = host[0];
    let Tabl = document.createElement("tbody"); 
    Tabl.classList.add("PlayerTable","PlayerTable/"+this.Instanca);
    host.appendChild(Tabl); 
    return Tabl;
}

crtajAddIgrac()
{
    let host = document.getElementsByClassName("CRUD_Form"+this.Instanca);
    host = host[0];
    let lADD = document.createElement("label");
    lADD.innerHTML = "ADD";
    lADD.style.fontWeight = "600";
    lADD.style.outline = "1px solid black";
    lADD.style.background = "#149900";
    lADD.style.color = "black";
    lADD.classList.add("40"+this.Instanca,"labelCRUD","labelCRUD_ADD");
    host.appendChild(lADD);

    let lIme = document.createElement("label");
    lIme.innerHTML = "Ime"
    lIme.className = "40"+this.Instanca;

    let lPrezime = document.createElement("label");
    lPrezime.innerHTML = "Prezime"
    lPrezime.className = "40"+this.Instanca;

    let lNadimak = document.createElement("label");
    lNadimak.innerHTML = "Nadimak"
    lNadimak.className = "40"+this.Instanca;

    let lGodine = document.createElement("label");
    lGodine.innerHTML = "Godine staza :"
    lGodine.className = "40"+this.Instanca;

    
    let ImeIN = document.createElement("input");
    ImeIN.className = "40"+this.Instanca;

    let PrezimeIN = document.createElement("input");
    PrezimeIN.className = "40"+this.Instanca;

    let NadimakIN = document.createElement("input");
    NadimakIN.className = "40"+this.Instanca;

    let GodineIN = document.createElement("input");
    GodineIN.className = "40"+this.Instanca;
    GodineIN.type = "number";

    let SubTim = document.createElement("button");
    SubTim.type = "Submit";
    SubTim.classList.add("SubmitBtn","SubmitBtn"+this.Instanca );
    SubTim.innerHTML = "Kreiraj";
    SubTim.onclick = (ev) =>{ 
      if(this.TrenutniBr < this.Max)
      this.DodajIgraca(ImeIN,PrezimeIN,NadimakIN,GodineIN,ListT);
      else
      alert("Kapaciteti tima su popunjeni");    
     }

    host.appendChild(lIme);
    host.appendChild(ImeIN);

    host.appendChild(lPrezime);
    host.appendChild(PrezimeIN);

    host.appendChild(lNadimak);
    host.appendChild(NadimakIN);


    host.appendChild(lGodine);
    host.appendChild(GodineIN);

    let l =  document.createElement("label");
    l.innerHTML= "Pozicija";
    l.className = "40"+this.Instanca;
    host.appendChild(l);

    let ListT =  document.createElement("select");
    ListT.classList.add("SelectList","SelectList"+this.Instanca );
    host.appendChild(ListT);

  
    this.PozicijaList.forEach(el=>{
      let op = document.createElement("option");
      op.innerHTML = el.Naziv;
      op.value = el.ID;
      ListT.appendChild(op);
      
    });

    host.appendChild(SubTim);
    this.crtajUpdateIgrac(host)
}

crtajUpdateIgrac(host)
{
    let lUpdate = document.createElement("label");
    lUpdate.innerHTML = "UPDATE";
    lUpdate.style.outline = "1px solid black";
    lUpdate.style.background = "#D1D100";
    lUpdate.style.fontWeight = "600";
    lUpdate.style.color = "black";
    lUpdate.classList.add("40"+this.Instanca,"labelCRUD");
    host.appendChild(lUpdate);
    

    let lNadimak = document.createElement("label");
    lNadimak.innerHTML = "Nadimak"
    lNadimak.className = "40"+this.Instanca;

    let NadimakIN = document.createElement("input");
    NadimakIN.className = "40"+this.Instanca;

    host.appendChild(lNadimak);
    host.appendChild(NadimakIN);

    let l =  document.createElement("label");
    l.innerHTML= "Pozicija";
    l.className = "40"+this.Instanca;
    host.appendChild(l);

    let ListT =  document.createElement("select");
    ListT.classList.add("SelectList","SelectList"+this.Instanca );
    host.appendChild(ListT);

  
    this.PozicijaList.forEach(el=>{
      let op = document.createElement("option");
      op.innerHTML = el.Naziv;
      op.value = el.ID;
      ListT.appendChild(op);
      
    });

    let SubTim = document.createElement("button");
    SubTim.type = "Submit";
    SubTim.classList.add("SubmitBtn","SubmitBtn"+this.Instanca );
    SubTim.innerHTML = "Izmeni";
    SubTim.onclick = (ev) =>{ 
     if(this.TrenutniBr > 0)
     this.IzmeniIgraca(NadimakIN,ListT);
     else
     alert("Nedovoljan broj igraca za ovu operaciju");
    }
    host.appendChild(SubTim);
    this.crtajDeleteIgrac(host);
}

crtajDeleteIgrac(host)
{
    let lDelete = document.createElement("label");
    lDelete.innerHTML = "DELETE";
    lDelete.style.outline = "1px solid black";
    lDelete.style.background = "#752626";
    lDelete.style.fontWeight = "600";
    lDelete.style.color = "black";
    lDelete.classList.add("40"+this.Instanca,"labelCRUD");
    host.appendChild(lDelete);
    

    let lNadimak = document.createElement("label");
    lNadimak.innerHTML = "Nadimak"
    lNadimak.className = "40"+this.Instanca;

    let NadimakIN = document.createElement("input");
    NadimakIN.className = "40"+this.Instanca;

    host.appendChild(lNadimak);
    host.appendChild(NadimakIN);

    let SubTim = document.createElement("button");
    SubTim.type = "Submit";
    SubTim.classList.add("SubmitBtn","SubmitBtn"+this.Instanca );
    SubTim.innerHTML = "Obrisi";
    SubTim.onclick = (ev) =>{ 
     if(this.TrenutniBr > 0)
     this.ObrisiIgraca(NadimakIN);
     else
     alert("Nedovoljno igraca za ovu operaciju")
    }
    host.appendChild(SubTim);
}

DodajIgraca(Ime,Prezime,Nadimak,Godine,Poz)
{
    if(Ime.value.lenght > 20 || Ime.value.lenght == 0)
    {
        alert("Ime igraca nije validno !");
        return false;
    }
    if(Prezime.value.lenght > 40 || Prezime.value.lenght == 0)
    {
        alert("Prezime igraca nije validno !");
        return false;
    }
    if(Nadimak.value.lenght > 20 || Nadimak.value.lenght < 0)
    {
        alert("Nadimak igraca nije validan !");
        return false;
    }
    if(Godine.value > 10 || Godine.value < 1)
    {
        alert("Godine staza igraca nisu validne !");
        return false;
    }
    if(Poz < 0)
    {
        alert("Unesite poziciju igraca!");
        return false;
    }
    fetch("https://localhost:5001/Igrac/DodajIgraca/"+Ime.value+"/"+Prezime.value+"/"+Nadimak.value+"/"+Godine.value+"/"+Poz.value+"/"+this.ImeETima,
    {
        method:"POST"
    }).then(s=>{
        if(s.ok)
        {
            alert("Uspesno dodat igrac u nase evidencije !");

                let Pozicija;
                this.PozicijaList.forEach(el => {
                    if(el.ID == Poz.value)
                    Pozicija = el
                });
                const obj = new Igrac(Ime.value,Prezime.value,Nadimak.value,Godine.value,Pozicija);
                obj.dodajGrafiku(this.Instanca,this.Max);
                this.increaseNum();
             
        }           
        else
        alert("Doslo je do greske igrac nije uveden u evidenciju");
    }
        );

}

IzmeniIgraca(Nadimak,Poz)
{
    if(Nadimak.value.lenght > 20 || Nadimak.value.lenght < 0)
    {
        alert("Nadimak igraca nije validan !");
        return false;
    }
    if(Poz < 0)
    {
        alert("Unesite poziciju igraca !");
        return false;
    }

    fetch("https://localhost:5001/Igrac/UpdateIgraca/"+Nadimak.value+"/"+Poz.value+"/"+this.ImeETima,
    {
        method:"PUT"
    }).then(s=>{
        if(s.ok)
        {
            alert("Uspesno izmenjen igrac u nasoj evidenciji !");
            let tmp = new Igrac();
            let PozName;
            this.PozicijaList.forEach(el=>{            
                if(el.ID == Poz.value)
                PozName = el.Naziv;
            })
            tmp.UpdateGrafiku(this.Instanca,this.Max,Nadimak.value,PozName)
        }           
        else
        alert("Doslo je do greske igrac nije izmenjen");
    });
}

ObrisiIgraca(Nadimak)
{
    if(Nadimak.value.lenght > 20 || Nadimak.value.lenght < 0)
    {
        alert("Nadimak igraca nije validan !");
        return false;
    }

    fetch("https://localhost:5001/Igrac/DeleteIgraca/"+Nadimak.value+"/"+this.ImeETima,
    {
        method:"DELETE"
    }).then(s=>{
        if(s.ok)
        {
            alert("Uspesno obrisan igrac u nasoj evidenciji !");
            let tmp = new Igrac();
            tmp.obrisiGrafiku(this.Instanca,this.Max,Nadimak.value);
            this.decreaseNum()
        }           
        else
        alert("Doslo je do greske igrac nije obrisan");
    });       
}

increaseNum()
{
this.TrenutniBr++;
}
decreaseNum()
{
this.TrenutniBr--;  
}
}