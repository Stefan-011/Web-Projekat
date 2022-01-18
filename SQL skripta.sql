SET IDENTITY_INSERT Sponzor ON;
INSERT INTO Sponzor(ID, Naziv, Iznos) VALUES (1,'LG', 12500);
INSERT INTO Sponzor(ID, Naziv, Iznos) VALUES (2,'Cooler Master', 25000);
INSERT INTO Sponzor(ID, Naziv, Iznos) VALUES (3,'G FUEL', 22000);
INSERT INTO Sponzor(ID, Naziv, Iznos) VALUES (4,'HyperX', 18900);
INSERT INTO Sponzor(ID, Naziv, Iznos) VALUES (5,'MSI Gaming', 15000);
INSERT INTO Sponzor(ID, Naziv, Iznos) VALUES (6,'Logitech', 16000);
INSERT INTO Sponzor(ID, Naziv, Iznos) VALUES (7,'Razer', 24000);
INSERT INTO Sponzor(ID, Naziv, Iznos) VALUES (8,'Viper Gaming', 8000);

SET IDENTITY_INSERT Sponzor OFF;

SET IDENTITY_INSERT Trener ON;

INSERT INTO Trener(ID, Ime, Prezime,GodineStaza) VALUES (1,'Stephen','Patterson',2);
INSERT INTO Trener(ID, Ime, Prezime,GodineStaza) VALUES (2,'Tucker','Hill',3);
INSERT INTO Trener(ID, Ime, Prezime,GodineStaza) VALUES (3,'Milos','Jovic',3);
INSERT INTO Trener(ID, Ime, Prezime,GodineStaza) VALUES (4,'Kenneth','Scott',1);
INSERT INTO Trener(ID, Ime, Prezime,GodineStaza) VALUES (5,'Nikolaj','Lindberg',4);
INSERT INTO Trener(ID, Ime, Prezime,GodineStaza) VALUES (6,'Kjeld','Rask',2);
INSERT INTO Trener(ID, Ime, Prezime,GodineStaza) VALUES (7,'Laurent','Alard',2);
INSERT INTO Trener(ID, Ime, Prezime,GodineStaza) VALUES (8,'Hermann','Ungerer',6);
INSERT INTO Trener(ID, Ime, Prezime,GodineStaza) VALUES (9,'Leontiy','Karantirov',3);
  
SET IDENTITY_INSERT Trener OFF;

SET IDENTITY_INSERT Pozicija ON;

INSERT INTO Pozicija(ID, Ime) VALUES (1,'Entry fragger');
INSERT INTO Pozicija(ID, Ime) VALUES (2,'Lurker');
INSERT INTO Pozicija(ID, Ime) VALUES (3,'In-game leader');
INSERT INTO Pozicija(ID, Ime) VALUES (4,'Support');
INSERT INTO Pozicija(ID, Ime) VALUES (5,'AWP-er');
INSERT INTO Pozicija(ID, Ime) VALUES (6,'Rifler');
INSERT INTO Pozicija(ID, Ime) VALUES (7,'Streamer');

  
SET IDENTITY_INSERT Pozicija OFF;

SET IDENTITY_INSERT Pozicija ON;

INSERT INTO Pozicija(ID, Ime) VALUES (1,'Entry fragger');
INSERT INTO Pozicija(ID, Ime) VALUES (2,'Lurker');
INSERT INTO Pozicija(ID, Ime) VALUES (3,'In-game leader');
INSERT INTO Pozicija(ID, Ime) VALUES (4,'Support');
INSERT INTO Pozicija(ID, Ime) VALUES (5,'AWP-er');
INSERT INTO Pozicija(ID, Ime) VALUES (6,'Rifler');
INSERT INTO Pozicija(ID, Ime) VALUES (7,'Streamer');

  
SET IDENTITY_INSERT Pozicija OFF;

SET IDENTITY_INSERT Igrac ON;

INSERT INTO Igrac(ID, Ime, Nadimak, Prezime, GodineStaza, PozicijaID) VALUES (1,'Aleksandar','Palm','Stankovic', 3 ,1);
INSERT INTO Igrac(ID, Ime, Nadimak, Prezime, GodineStaza, PozicijaID) VALUES (2,'Mirko','MisFit','Stankovic', 1 ,1);
INSERT INTO Igrac(ID, Ime, Nadimak, Prezime, GodineStaza, PozicijaID) VALUES (3,'Goran','GoVAC','Kovacevic', 2 ,1);
INSERT INTO Igrac(ID, Ime, Nadimak, Prezime, GodineStaza, PozicijaID) VALUES (4,'Ejvind','Frosty','Frost', 4 ,1);

INSERT INTO Igrac(ID, Ime, Nadimak, Prezime, GodineStaza, PozicijaID) VALUES (5,'Niklas','Klus','Klausen', 1 ,2);
INSERT INTO Igrac(ID, Ime, Nadimak, Prezime, GodineStaza, PozicijaID) VALUES (6,'Andreas','Guard','Daugaard', 4 ,2);    
INSERT INTO Igrac(ID, Ime, Nadimak, Prezime, GodineStaza, PozicijaID) VALUES (7,'Patrick','VACman','Wahrmann', 2 ,2);  

INSERT INTO Igrac(ID, Ime, Nadimak, Prezime, GodineStaza, PozicijaID) VALUES (8,'Dimitrije','Dime','Nikolic', 2 ,3);
INSERT INTO Igrac(ID, Ime, Nadimak, Prezime, GodineStaza, PozicijaID) VALUES (9,'Jovan','Limeni','Stamenkovic', 1 ,3);  
INSERT INTO Igrac(ID, Ime, Nadimak, Prezime, GodineStaza, PozicijaID) VALUES (10,'Kaspar','KaHun','Huhn', 4 ,3); 
INSERT INTO Igrac(ID, Ime, Nadimak, Prezime, GodineStaza, PozicijaID) VALUES (11,'Boris','Stamen','Stamenov', 2 ,3);

INSERT INTO Igrac(ID, Ime, Nadimak, Prezime, GodineStaza, PozicijaID) VALUES (12,'Jordan','JD','Dieudonné', 2 ,4);     
INSERT INTO Igrac(ID, Ime, Nadimak, Prezime, GodineStaza, PozicijaID) VALUES (13,'Anton','aTon','Zaporozhets', 2 ,4);     

INSERT INTO Igrac(ID, Ime, Nadimak, Prezime, GodineStaza, PozicijaID) VALUES (14,'Petar','Petrol','Timotijevic', 2 ,5);    
INSERT INTO Igrac(ID, Ime, Nadimak, Prezime, GodineStaza, PozicijaID) VALUES (15,'Viktor','Victory','Risticevic', 3 ,5);
INSERT INTO Igrac(ID, Ime, Nadimak, Prezime, GodineStaza, PozicijaID) VALUES (16,'Pascal','KilloPascal','Sylvestre', 2 ,5);       

INSERT INTO Igrac(ID, Ime, Nadimak, Prezime, GodineStaza, PozicijaID) VALUES (17,'Swen','Sweny','Weiskopf', 2 ,6);    
INSERT INTO Igrac(ID, Ime, Nadimak, Prezime, GodineStaza, PozicijaID) VALUES (18,'Kevin','Baker','Berkelder', 1 ,6);
INSERT INTO Igrac(ID, Ime, Nadimak, Prezime, GodineStaza, PozicijaID) VALUES (19,'Bronisław','Iron','Gwóźd', 1 ,6);        
INSERT INTO Igrac(ID, Ime, Nadimak, Prezime, GodineStaza, PozicijaID) VALUES (20,'Milyutin','Milys','Yakovich', 2 ,6);

INSERT INTO Igrac(ID, Ime, Nadimak, Prezime, GodineStaza, PozicijaID) VALUES (21,'Elliot','Akers','Akers', 3 ,7);        
INSERT INTO Igrac(ID, Ime, Nadimak, Prezime, GodineStaza, PozicijaID) VALUES (22,'Cooper','Cobalc','Black', 4 ,7);
INSERT INTO Igrac(ID, Ime, Nadimak, Prezime, GodineStaza, PozicijaID) VALUES (23,'Maxwell','Maximum','Maxwell ', 2 ,7);
INSERT INTO Igrac(ID, Ime, Nadimak, Prezime, GodineStaza, PozicijaID) VALUES (24,'Ralf','Backerman','Becker ', 5 ,7);
INSERT INTO Igrac(ID, Ime, Nadimak, Prezime, GodineStaza, PozicijaID) VALUES (25,'Denis','Denny','Milojevic ', 7 ,7);  
  
SET IDENTITY_INSERT Igrac OFF;




