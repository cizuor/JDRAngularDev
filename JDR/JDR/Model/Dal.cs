using JDR.Model.Personnage;
using JDR.Model.Statistique;
using JDR.Model.Statistique.ValeurStat;
using JDR.ViewModel;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDR.Model
{
    public class Dal
    {
        private BddContext db = new BddContext();

        public List<Stat> GetAllStatLimited()
        {
            return db.Stats.ToList();
        }
        public List<Stat> GetAllStat()
        {
            return db.Stats.Include(s => s.StatUtils).ThenInclude(su => su.StatUtile).ToList();
        }
        public List<Stat> GetAllStatJson()
        {
            return db.Stats.Include(s => s.StatUtils).ToList();
        }
        public Stat GetStatByIdLimited(int idStat)
        {
            return db.Stats.FirstOrDefault(e => e.Id == idStat);
        }
        public Stat GetStatById(int idStat)
        {
            return db.Stats.Include(s => s.StatUtils).ThenInclude(su => su.StatUtile).FirstOrDefault(e => e.Id == idStat);
        }

        public Stat GetStatByStat(Stat.stats stat)
        {
            return db.Stats.Include(s => s.StatUtils).ThenInclude(su => su.StatUtile).FirstOrDefault(e => e.Stats == stat);
        }

        public List<Race> GetAllRaceJson()
        {
            return db.Races.Include(r => r.Stat).Include(r => r.StatDee).Include(r => r.ListSousRace).ToList();
        }
        public List<Race> GetAllRace()
        {
            return db.Races.Include(r => r.Stat).ThenInclude(s => s.Stat).Include(r => r.StatDee).ThenInclude(sd => sd.Stat).Include(r => r.ListSousRace).ToList();
        }
        public Race GetRaceById(int idRace)
        {
            return db.Races.Include(r => r.Stat).ThenInclude(s => s.Stat).Include(r => r.StatDee).ThenInclude(sd => sd.Stat).Include(r => r.ListSousRace).FirstOrDefault(e => e.Id == idRace);
        }

        public List<Classe> GetAllClasse()
        {
            return db.Classes.Include(c => c.Stat).ThenInclude(s => s.Stat).ToList() ;
        }
        public Classe GetClasseById(int id)
        {
            return db.Classes.Include(c => c.Stat).ThenInclude(s => s.Stat).FirstOrDefault(c => c.Id == id);
        }

        public List<SousRace> GetAllSousRace()
        {
            return db.SousRaces.Include(sr => sr.Stat).ThenInclude(s => s.Stat).Include(sr => sr.Race).ToList();
        }

        public SousRace GetSousRaceById(int id)
        {
            return db.SousRaces.Include(sr => sr.Stat).ThenInclude(s => s.Stat).Include(sr=> sr.Race).FirstOrDefault(sr => sr.Id == id);
        }
        public List<User> GetAllUser()
        {
            return db.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return db.Users.Include(u => u.Persos).FirstOrDefault(u => u.Id == id);
        }
        public User GetUserByMail(String mail)
        {
            return db.Users.Include(u => u.Persos).FirstOrDefault(u => u.Email == mail);
        }

        public User Autentifie(String mail , String pass)
        {
            User u = GetUserByMail(mail);
            if (u != null)
            {
                if (u.VerifMdp(pass))
                {
                    return u;
                }
            }
            return null;
        }
        public void AddUser(NewUser newUser)
        {
            User u = new User (newUser.Pseudo,newUser.Email,newUser.Password);
            db.Users.Add(u);
            db.SaveChanges();
        }


        public List<Perso> GetPersoByUserId(int id)
        {

            User u = db.Users
                .Include(u => u.Persos).ThenInclude(p => p.Race)
               .Include(u => u.Persos).ThenInclude(p => p.Classe)
               .Include(u => u.Persos).ThenInclude(p => p.SousRace)
               .Include(u => u.Persos).ThenInclude(p => p.User).FirstOrDefault(u => u.Id == id);
            return u.Persos;
        }


        public Perso GetPersoById(int id)
        {
            return db.Persos
                .Include(p => p.Stats).ThenInclude(s => s.Stat)
                .Include(p => p.Race).ThenInclude(r => r.Stat).ThenInclude(s => s.Stat)
                .Include(p => p.Race).ThenInclude(r => r.StatDee).ThenInclude(s => s.Stat)
                .Include(p => p.Classe).ThenInclude(c => c.Stat).ThenInclude(s => s.Stat)
                .Include(p => p.SousRace).ThenInclude(sr => sr.Stat).ThenInclude(s => s.Stat)
                .Include(p => p.Buff).ThenInclude(b => b.Stat)
                .Include(p => p.User)
                .FirstOrDefault(p => p.Id == id);
        }

        public void AddStat(Stat stat)
        {
            db.Stats.Add(stat);
            db.SaveChanges();
        }

        public void AddStatUtil(StatUtil Util, Stat ForStat, Stat StatUtils)
        {
            Util.StatUtile = StatUtils;
            db.Entry(StatUtils).State = EntityState.Modified;
            Util.ForStat = ForStat;
            db.Entry(ForStat).State = EntityState.Modified;
            ForStat.StatUtils.Add(Util);
            db.SaveChanges();
        }

        public void AddRace(Race race)
        {
            foreach (ValeurRaceStat s in race.Stat)
            {
                s.Stat = GetAllStat().FirstOrDefault(sta => sta.Id == s.Stat.Id);
                db.Entry(s.Stat).State = EntityState.Modified;
            }
            foreach (DeeStat d in race.StatDee)
            {
                d.Stat = GetAllStat().FirstOrDefault(sta => sta.Id == d.Stat.Id);
                db.Entry(d.Stat).State = EntityState.Modified;
            }
            db.Races.Add(race);
            db.SaveChanges();
        }



        public void MajStat(NewStat newStat)
        {

            Stat stat = GetStatById(newStat.Id);

            stat.Nom = newStat.Nom;
            stat.Definition = newStat.Definition;
            stat.Stats = newStat.Stats;
            stat.Type = newStat.Type;
            foreach(StatUtil su in stat.StatUtils)
            {
                db.Entry(su).State = EntityState.Deleted;
            }
            stat.StatUtils.Clear();
            foreach (NewStatUtil su in newStat.StatUtils)
            {
                StatUtil statUtil = new StatUtil { Valeur = su.Valeur };
                Stat statUse = GetStatById(su.StatUtile);
                statUtil.StatUtile = statUse;
                if (stat.StatUtils == null)
                {
                    stat.StatUtils = new List<StatUtil>();
                }
                stat.StatUtils.Add(statUtil);
            }
            db.Entry(stat).State = EntityState.Modified;
            db.SaveChanges();

        }


        public void MajRace(NewRace newRace)
        {
            Race race = GetRaceById(newRace.Id);
            race.Nom = newRace.Nom;
            race.Definition = newRace.Definition;
            
            foreach(ValeurRaceStat vr in race.Stat)
            {
                db.Entry(vr.Stat).State = EntityState.Modified;
                db.Entry(vr).State = EntityState.Deleted;
            }
            race.Stat.Clear(); 
            foreach (DeeStat ds in race.StatDee)
            {
                db.Entry(ds.Stat).State = EntityState.Modified;
                db.Entry(ds).State = EntityState.Deleted;
            }
            race.StatDee.Clear();
            foreach (NewValeurStat newValeur in newRace.Stat)
            {
                if (newValeur.Valeur != 0)
                {
                    Stat statUse = GetStatById(newValeur.StatId);
                    ValeurRaceStat vr = new ValeurRaceStat { Stat = statUse, Valeur = newValeur.Valeur };
                    if (race.Stat == null)
                    {
                        race.Stat = new List<ValeurRaceStat>();
                    }
                    race.Stat.Add(vr);
                    db.Entry(statUse).State = EntityState.Modified;
                }
            }
            foreach (NewDeeStat newValeur in newRace.StatDee)
            {
                if (newValeur.TailleDee != 0 && newValeur.NbDee != 0)
                {
                    Stat statUse = GetStatById(newValeur.StatId);
                    DeeStat ds = new DeeStat { Stat = statUse, TailleDee = newValeur.TailleDee, NbDee = newValeur.NbDee };
                    if (race.StatDee == null)
                    {
                        race.StatDee = new List<DeeStat>();
                    }
                    race.StatDee.Add(ds);
                    db.Entry(statUse).State = EntityState.Modified;
                }
            }
            db.Entry(race).State = EntityState.Modified;
            db.SaveChanges();

            Race racetest = GetRaceById(newRace.Id);
        }



        public void AddRace(NewRace newRace)
        {
            Race race = new Race();
            race.Nom = newRace.Nom;
            race.Definition = newRace.Definition;

            foreach (NewValeurStat newValeur in newRace.Stat)
            {
                if (newValeur.Valeur != 0)
                {
                    Stat statUse = GetStatById(newValeur.StatId);
                    ValeurRaceStat vr = new ValeurRaceStat { Stat = statUse, Valeur = newValeur.Valeur };
                    if (race.Stat == null)
                    {
                        race.Stat = new List<ValeurRaceStat>();
                    }
                    race.Stat.Add(vr);
                    db.Entry(statUse).State = EntityState.Modified;
                }
            }
            foreach (NewDeeStat newValeur in newRace.StatDee)
            {
                if (newValeur.TailleDee != 0 && newValeur.NbDee != 0)
                {
                    Stat statUse = GetStatById(newValeur.StatId);
                    DeeStat ds = new DeeStat { Stat = statUse, TailleDee = newValeur.TailleDee, NbDee = newValeur.NbDee };
                    if (race.StatDee == null)
                    {
                        race.StatDee = new List<DeeStat>();
                    }
                    race.StatDee.Add(ds);
                    db.Entry(statUse).State = EntityState.Modified;
                }
            }
            db.Races.Add(race);
            db.SaveChanges();

        }



        public void AddClasse(NewClasse newClasse)
        {
            Classe classe = new Classe { Nom = newClasse.Nom , Definition = newClasse.Definition};

            foreach (NewValeurStat newValeur in newClasse.Stat)
            {
                if (newValeur.Valeur != 0)
                {
                    Stat statUse = GetStatById(newValeur.StatId);
                    ValeurClasseStat vc = new ValeurClasseStat { Stat = statUse, Valeur = newValeur.Valeur };
                    if (classe.Stat == null)
                    {
                        classe.Stat = new List<ValeurClasseStat>();
                    }
                    classe.Stat.Add(vc);
                    db.Entry(statUse).State = EntityState.Modified;
                }
            }
            db.Classes.Add(classe);
            db.SaveChanges();
        }


        public void MajClasse(NewClasse newClasse)
        {
            Classe classe = GetClasseById(newClasse.Id);
            classe.Nom = newClasse.Nom;
            classe.Definition = newClasse.Definition;


            foreach(ValeurClasseStat vc in classe.Stat)
            {
                db.Entry(vc.Stat).State = EntityState.Modified;
                db.Entry(vc).State = EntityState.Deleted;
            }
            classe.Stat.Clear();



            foreach (NewValeurStat newValeur in newClasse.Stat)
            {
                if (newValeur.Valeur != 0)
                {
                    Stat statUse = GetStatById(newValeur.StatId);
                    ValeurClasseStat vc = new ValeurClasseStat { Stat = statUse, Valeur = newValeur.Valeur };
                    if (classe.Stat == null)
                    {
                        classe.Stat = new List<ValeurClasseStat>();
                    }
                    classe.Stat.Add(vc);
                    db.Entry(statUse).State = EntityState.Modified;
                }
            }


            db.Entry(classe).State = EntityState.Modified;
            db.SaveChanges();

            Classe classeTest = GetClasseById(newClasse.Id);

        }


        public void AddSousRace(NewSousRace newSousRace)
        {

            SousRace sousRace = new SousRace {Nom = newSousRace.Nom,Definition=newSousRace.Definition };
            sousRace.Race = GetRaceById(newSousRace.IdRace);

            foreach (NewValeurStat newValeur in newSousRace.Stat)
            {
                if (newValeur.Valeur != 0)
                {
                    Stat statUse = GetStatById(newValeur.StatId);
                    ValeurSousRaceStat vsr = new ValeurSousRaceStat { Stat = statUse, Valeur = newValeur.Valeur };
                    if (sousRace.Stat == null)
                    {
                        sousRace.Stat = new List<ValeurSousRaceStat>();
                    }
                    sousRace.Stat.Add(vsr);
                    db.Entry(statUse).State = EntityState.Modified;
                }
            }
            db.SousRaces.Add(sousRace);
            db.SaveChanges();
        }


        public void MajSousRacee(NewSousRace newSousRace)
        {
            SousRace sousRace = GetSousRaceById(newSousRace.Id);
            sousRace.Nom = newSousRace.Nom;
            sousRace.Definition = newSousRace.Definition;
            
            Race oldRace = GetRaceById(sousRace.Race.Id);
            oldRace.ListSousRace.Remove(sousRace);
            db.Entry(sousRace.Race).State = EntityState.Modified;
            sousRace.Race = GetRaceById(newSousRace.IdRace);
            sousRace.Race.ListSousRace.Add(sousRace);
            db.Entry(sousRace.Race).State = EntityState.Modified;


            foreach (ValeurSousRaceStat vsr in sousRace.Stat)
            {
                db.Entry(vsr.Stat).State = EntityState.Modified;
                db.Entry(vsr).State = EntityState.Deleted;
            }
            sousRace.Stat.Clear();

            foreach (NewValeurStat newValeur in newSousRace.Stat)
            {
                if (newValeur.Valeur != 0)
                {
                    Stat statUse = GetStatById(newValeur.StatId);
                    ValeurSousRaceStat vsr = new ValeurSousRaceStat { Stat = statUse, Valeur = newValeur.Valeur };
                    if (sousRace.Stat == null)
                    {
                        sousRace.Stat = new List<ValeurSousRaceStat>();
                    }
                    sousRace.Stat.Add(vsr);
                    db.Entry(statUse).State = EntityState.Modified;
                }
            }
            db.Entry(sousRace).State = EntityState.Modified;
            db.SaveChanges();
        }


        public void AddPerso(NewPerso newPerso)
        {
            Perso perso = new Perso
            {
                Nom = newPerso.Nom,
                Prenom = newPerso.Prenom,
                Definition = newPerso.Definition,
                Vivant = newPerso.Vivant,
                Lvl = newPerso.Lvl,
                posX = newPerso.posX,
                posY = newPerso.posY
            };
            perso.Race = GetRaceById(newPerso.IdRace);
            perso.SousRace = GetSousRaceById(newPerso.IdSousRace);
            perso.Classe = GetClasseById(newPerso.IdClasse);
            db.Entry(perso.Race).State = EntityState.Modified;
            db.Entry(perso.SousRace).State = EntityState.Modified;
            db.Entry(perso.Classe).State = EntityState.Modified;


            foreach(NewValeurStat vs in newPerso.Stats)
            {
                if(vs.Valeur != 0)
                {
                    Stat stat = GetStatById(vs.StatId);
                    ValeurPersoStat vp = new ValeurPersoStat { Stat = stat, Valeur = vs.Valeur };
                    if(perso.Stats == null)
                    {
                        perso.Stats = new List<ValeurPersoStat>();
                    }
                    perso.Stats.Add(vp);
                    db.Entry(stat).State = EntityState.Modified;
                }
            }
            foreach (NewValeurStat vs in newPerso.Buff)
            {
                if (vs.Valeur != 0)
                {
                    Stat stat = GetStatById(vs.StatId);
                    ValeurBuffStat vb = new ValeurBuffStat { Stat = stat, Valeur = vs.Valeur };
                    if (perso.Buff == null)
                    {
                        perso.Buff = new List<ValeurBuffStat>();
                    }
                    perso.Buff.Add(vb);
                    db.Entry(stat).State = EntityState.Modified;
                }
            }
            db.Persos.Add(perso);
            db.SaveChanges();
        }


        public void MajPerso(NewPerso newPerso)
        {
            Perso perso = GetPersoById(newPerso.Id);
            perso.Nom = newPerso.Nom;
            perso.Prenom = newPerso.Prenom;
            perso.Definition = newPerso.Definition;
            perso.Vivant = newPerso.Vivant;
            perso.Lvl = newPerso.Lvl;
            perso.posX = newPerso.posX;
            perso.posY = newPerso.posY;
            db.Entry(perso.Race).State = EntityState.Modified;
            db.Entry(perso.SousRace).State = EntityState.Modified;
            db.Entry(perso.Classe).State = EntityState.Modified;
            perso.Race = GetRaceById(newPerso.IdRace);
            perso.SousRace = GetSousRaceById(newPerso.IdSousRace);
            perso.Classe = GetClasseById(newPerso.IdClasse);
            db.Entry(perso.Race).State = EntityState.Modified;
            db.Entry(perso.SousRace).State = EntityState.Modified;
            db.Entry(perso.Classe).State = EntityState.Modified;

            foreach (ValeurPersoStat vps in perso.Stats)
            {
                db.Entry(vps.Stat).State = EntityState.Modified;
                db.Entry(vps).State = EntityState.Deleted;
            }
            perso.Stats.Clear();
            foreach (ValeurBuffStat vbs in perso.Buff)
            {
                db.Entry(vbs.Stat).State = EntityState.Modified;
                db.Entry(vbs).State = EntityState.Deleted;
            }
            perso.Buff.Clear();



            foreach (NewValeurStat vs in newPerso.Stats)
            {
                if (vs.Valeur != 0)
                {
                    Stat stat = GetStatById(vs.StatId);
                    ValeurPersoStat vp = new ValeurPersoStat { Stat = stat, Valeur = vs.Valeur };
                    if (perso.Stats == null)
                    {
                        perso.Stats = new List<ValeurPersoStat>();
                    }
                    perso.Stats.Add(vp);
                    db.Entry(stat).State = EntityState.Modified;
                }
            }
            foreach (NewValeurStat vs in newPerso.Buff)
            {
                if (vs.Valeur != 0)
                {
                    Stat stat = GetStatById(vs.StatId);
                    ValeurBuffStat vb = new ValeurBuffStat { Stat = stat, Valeur = vs.Valeur };
                    if (perso.Buff == null)
                    {
                        perso.Buff = new List<ValeurBuffStat>();
                    }
                    perso.Buff.Add(vb);
                    db.Entry(stat).State = EntityState.Modified;
                }
            }
            db.Entry(perso).State = EntityState.Modified;
            db.SaveChanges();
        } 


        public void RecupFromJson()
        {
            /*List<Race> Races = GetAllRaceJson();
            foreach(Race r in Races)
            {
                foreach(ValeurRaceStat ve in r.Stat)
                {
                    ve.Race = null;
                    ve.Stat = null;
                }
                foreach (DeeStat ds in r.StatDee)
                {
                    ds.Race = null;
                    ds.Stat = null;
                }
                if (r.ListSousRace != null)
                {
                    foreach (SousRace sousRace in r.ListSousRace)
                    {
                        sousRace.Race = null;
                    }
                }
            }*/




            /*List<Stat> lStat = GetAllStat();
            int i = 0;
            List<Race> lRace = GetAllRace();
            int j = 0;
            Stat s = GetStatById(12);
            Stat s2 = GetStatById(13);
            foreach(StatUtil su in s.StatUtils)
            {
                StatUtil dutmp = su;
            }
            foreach (StatUtil su in s2.StatUtils)
            {
                StatUtil dutmp = su;
            }*/

            /*  Race gobelin = new Race();
              gobelin.Nom = "Gobelin";
              gobelin.Definition = "vert sale fourbe opportuniste ";
              gobelin.Stat = new List<ValeurRaceStat>();
              gobelin.StatDee = new List<DeeStat>();


              ValeurRaceStat CC = new ValeurRaceStat();
              CC.Stat = db.Stats.ToList().Find(s => s.Stats == Stat.stats.CC);
              CC.Valeur = 20;
              ValeurRaceStat Ag = new ValeurRaceStat();
              Ag.Stat = db.Stats.ToList().Find(s => s.Stats == Stat.stats.Ag);
              Ag.Valeur = 20;
              ValeurRaceStat F = new ValeurRaceStat();
              F.Stat = db.Stats.ToList().Find(s => s.Stats == Stat.stats.F);
              F.Valeur = 20;
              gobelin.Stat.Add(CC);
              gobelin.Stat.Add(Ag);
              gobelin.Stat.Add(F);

              DeeStat DCC = new DeeStat { NbDee = 2, TailleDee = 6 };
              DeeStat DAg = new DeeStat { NbDee = 2, TailleDee = 6 };
              DeeStat DF = new DeeStat { NbDee = 2, TailleDee = 6 };
              DCC.Stat = db.Stats.ToList().Find(s => s.Stats == Stat.stats.CC);
              DAg.Stat = db.Stats.ToList().Find(s => s.Stats == Stat.stats.Ag);
              DF.Stat = db.Stats.ToList().Find(s => s.Stats == Stat.stats.F);
              gobelin.StatDee.Add(DCC);
              gobelin.StatDee.Add(DAg);
              gobelin.StatDee.Add(DF);

              AddRace(gobelin);*/

            /*String jsonString = "[{\"StatCalculer\":null,\"Id\":1,\"Nom\":\"Capacité de combat\",\"Definition\":\"Représente votre habileté au combat au corps a corps\",\"Stats\":0,\"Type\":0},{\"StatCalculer\":null,\"Id\":2,\"Nom\":\"Capacité de tir\",\"Definition\":\"Représente votre habileté a jeter des objet et a tirer avec les arme a distance\",\"Stats\":1,\"Type\":0},{\"StatCalculer\":null,\"Id\":3,\"Nom\":\"Agilité\",\"Definition\":\"Représente les reflexes et l\\u0027équilibre , participe a votre initiative et a votre esquive\",\"Stats\":2,\"Type\":0},{\"StatCalculer\":null,\"Id\":4,\"Nom\":\"Force\",\"Definition\":\"Représente la puissance physique du personnage, votre aptitude à soulever des objets ou à les casser, participe aux dégâts et au poids transportable\",\"Stats\":3,\"Type\":0},{\"StatCalculer\":null,\"Id\":5,\"Nom\":\"Endurance\",\"Definition\":\"Représente la robustesse de votre personnage, sa capacité a résisté aux attaques physiques aux maladies, poison ... participe grandement aux réductions de dégâts\",\"Stats\":4,\"Type\":0},{\"StatCalculer\":null,\"Id\":6,\"Nom\":\"intelligence\",\"Definition\":\"représente la capacité d\\u0027apprentissage et de raisonnement de votre personnage, participe aux dégâts magiques, au pistage, réussite des sorts ...\",\"Stats\":5,\"Type\":0},{\"StatCalculer\":null,\"Id\":7,\"Nom\":\"Force mental\",\"Definition\":\"Représente la volonté, le bon sens l\\u0027acuité et l\\u0027intuition d\\u0027un personnage, participe aux dégâts de sort à la résistance à la magie et aux effets de contrôle ....\",\"Stats\":6,\"Type\":0},{\"StatCalculer\":null,\"Id\":8,\"Nom\":\"Perception\",\"Definition\":\"Représente vote acuité à percevoir le monde qui vous entoure, à remarquer les pièges et les trace, participe au pistage et à toutes les détections\",\"Stats\":7,\"Type\":0},{\"StatCalculer\":null,\"Id\":9,\"Nom\":\"Sociabilité\",\"Definition\":\"Représente la personnalité et le magnétisme personnel, la capacité à convaincre et à se faire comprendre, participe au charisme, mensonge, marchandage ... mais aussi à l\\u0027amélioration des pets\",\"Stats\":8,\"Type\":0},{\"StatCalculer\":null,\"Id\":10,\"Nom\":\"Point de vie\",\"Definition\":\"Représente le nombre de dégâts que vous pouvez encaisser avant de subir de grave blessure\",\"Stats\":9,\"Type\":0},{\"StatCalculer\":null,\"Id\":11,\"Nom\":\"PVManquand\",\"Definition\":\"Représente le nombre de points de vie qu\\u0027il vous manque\",\"Stats\":10,\"Type\":1},{\"StatCalculer\":null,\"Id\":12,\"Nom\":\"Point de vie bonus\",\"Definition\":\"point de vie en surplu , ils permette de dépasser le maximum de PV\",\"Stats\":11,\"Type\":1},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":3,\"Nom\":\"Agilité\",\"Definition\":\"Représente les reflexes et l\\u0027équilibre , participe a votre initiative et a votre esquive\",\"Stats\":2,\"Type\":0},\"Id\":1,\"Valeur\":100}],\"Id\":1},\"Id\":13,\"Nom\":\"Acrobatie\",\"Definition\":\"Représente vote habilleter au saut et votre équilibre\",\"Stats\":12,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":5,\"Nom\":\"Endurance\",\"Definition\":\"Représente la robustesse de votre personnage, sa capacité a résisté aux attaques physiques aux maladies, poison ... participe grandement aux réductions de dégâts\",\"Stats\":4,\"Type\":0},\"Id\":2,\"Valeur\":40}],\"Id\":2},\"Id\":15,\"Nom\":\"Apnee\",\"Definition\":\"Représente la capacité a retenir sont soufle du personnage\",\"Stats\":13,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":3,\"Nom\":\"Agilité\",\"Definition\":\"Représente les reflexes et l\\u0027équilibre , participe a votre initiative et a votre esquive\",\"Stats\":2,\"Type\":0},\"Id\":3,\"Valeur\":40},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":8,\"Nom\":\"Perception\",\"Definition\":\"Représente vote acuité à percevoir le monde qui vous entoure, à remarquer les pièges et les trace, participe au pistage et à toutes les détections\",\"Stats\":7,\"Type\":0},\"Id\":4,\"Valeur\":30}],\"Id\":3},\"Id\":16,\"Nom\":\"Crochetage\",\"Definition\":\"Représente votre habileté à crocheter les serrures et a désamorcé les pièges\",\"Stats\":14,\"Type\":2},{\"StatCalculer\":null,\"Id\":17,\"Nom\":\"Chance de Critique\",\"Definition\":\"Représente les chance de votre personnage d\\u0027infliger un coup critique avec ses attaque\",\"Stats\":15,\"Type\":1},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":9,\"Nom\":\"Sociabilité\",\"Definition\":\"Représente la personnalité et le magnétisme personnel, la capacité à convaincre et à se faire comprendre, participe au charisme, mensonge, marchandage ... mais aussi à l\\u0027amélioration des pets\",\"Stats\":8,\"Type\":0},\"Id\":5,\"Valeur\":60},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":6,\"Nom\":\"intelligence\",\"Definition\":\"représente la capacité d\\u0027apprentissage et de raisonnement de votre personnage, participe aux dégâts magiques, au pistage, réussite des sorts ...\",\"Stats\":5,\"Type\":0},\"Id\":6,\"Valeur\":40}],\"Id\":4},\"Id\":18,\"Nom\":\"Charisme\",\"Definition\":\"Représente votre prestence, votre capacité a captivé une foule ou a impressionné par votre seule présence\",\"Stats\":16,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":3,\"Nom\":\"Agilité\",\"Definition\":\"Représente les reflexes et l\\u0027équilibre , participe a votre initiative et a votre esquive\",\"Stats\":2,\"Type\":0},\"Id\":7,\"Valeur\":80},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":6,\"Nom\":\"intelligence\",\"Definition\":\"représente la capacité d\\u0027apprentissage et de raisonnement de votre personnage, participe aux dégâts magiques, au pistage, réussite des sorts ...\",\"Stats\":5,\"Type\":0},\"Id\":8,\"Valeur\":30}],\"Id\":5},\"Id\":19,\"Nom\":\"Déplacement silencieux\",\"Definition\":\"Représente votre aptitude à vous mouvoir sans bruit , a contourné un danger ....\",\"Stats\":17,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":3,\"Nom\":\"Agilité\",\"Definition\":\"Représente les reflexes et l\\u0027équilibre , participe a votre initiative et a votre esquive\",\"Stats\":2,\"Type\":0},\"Id\":9,\"Valeur\":80},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":6,\"Nom\":\"intelligence\",\"Definition\":\"représente la capacité d\\u0027apprentissage et de raisonnement de votre personnage, participe aux dégâts magiques, au pistage, réussite des sorts ...\",\"Stats\":5,\"Type\":0},\"Id\":10,\"Valeur\":60}],\"Id\":6},\"Id\":20,\"Nom\":\"Dissimultation\",\"Definition\":\"Représente votre habileté à vous caché.\",\"Stats\":18,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":4,\"Nom\":\"Force\",\"Definition\":\"Représente la puissance physique du personnage, votre aptitude à soulever des objets ou à les casser, participe aux dégâts et au poids transportable\",\"Stats\":3,\"Type\":0},\"Id\":11,\"Valeur\":1000}],\"Id\":7},\"Id\":21,\"Nom\":\"Poid Transportable\",\"Definition\":\"Représente la quantité d\\u0027objet que vous pouvez transporter sans étre ralenti\",\"Stats\":19,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":3,\"Nom\":\"Agilité\",\"Definition\":\"Représente les reflexes et l\\u0027équilibre , participe a votre initiative et a votre esquive\",\"Stats\":2,\"Type\":0},\"Id\":12,\"Valeur\":70},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":7,\"Nom\":\"Force mental\",\"Definition\":\"Représente la volonté, le bon sens l\\u0027acuité et l\\u0027intuition d\\u0027un personnage, participe aux dégâts de sort à la résistance à la magie et aux effets de contrôle ....\",\"Stats\":6,\"Type\":0},\"Id\":13,\"Valeur\":30}],\"Id\":8},\"Id\":22,\"Nom\":\"Equitation\",\"Definition\":\"Représente votre capacité à chevaucher ou à conduire un attelage\",\"Stats\":20,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":4,\"Nom\":\"Force\",\"Definition\":\"Représente la puissance physique du personnage, votre aptitude à soulever des objets ou à les casser, participe aux dégâts et au poids transportable\",\"Stats\":3,\"Type\":0},\"Id\":14,\"Valeur\":40},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":3,\"Nom\":\"Agilité\",\"Definition\":\"Représente les reflexes et l\\u0027équilibre , participe a votre initiative et a votre esquive\",\"Stats\":2,\"Type\":0},\"Id\":15,\"Valeur\":50}],\"Id\":9},\"Id\":23,\"Nom\":\"Escalade\",\"Definition\":\"Représente votre capacité a grimpé des obstacles.\",\"Stats\":21,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":3,\"Nom\":\"Agilité\",\"Definition\":\"Représente les reflexes et l\\u0027équilibre , participe a votre initiative et a votre esquive\",\"Stats\":2,\"Type\":0},\"Id\":16,\"Valeur\":30},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":8,\"Nom\":\"Perception\",\"Definition\":\"Représente vote acuité à percevoir le monde qui vous entoure, à remarquer les pièges et les trace, participe au pistage et à toutes les détections\",\"Stats\":7,\"Type\":0},\"Id\":17,\"Valeur\":20}],\"Id\":10},\"Id\":24,\"Nom\":\"Fuite\",\"Definition\":\"Représente votre bonus pour quitter un combat sans être frappé.\",\"Stats\":22,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":3,\"Nom\":\"Agilité\",\"Definition\":\"Représente les reflexes et l\\u0027équilibre , participe a votre initiative et a votre esquive\",\"Stats\":2,\"Type\":0},\"Id\":18,\"Valeur\":50}],\"Id\":11},\"Id\":25,\"Nom\":\"Initiative\",\"Definition\":\"Représente votre rapidité d\\u0027action , vos reflex.\",\"Stats\":23,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":9,\"Nom\":\"Sociabilité\",\"Definition\":\"Représente la personnalité et le magnétisme personnel, la capacité à convaincre et à se faire comprendre, participe au charisme, mensonge, marchandage ... mais aussi à l\\u0027amélioration des pets\",\"Stats\":8,\"Type\":0},\"Id\":19,\"Valeur\":75},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":6,\"Nom\":\"intelligence\",\"Definition\":\"représente la capacité d\\u0027apprentissage et de raisonnement de votre personnage, participe aux dégâts magiques, au pistage, réussite des sorts ...\",\"Stats\":5,\"Type\":0},\"Id\":20,\"Valeur\":20},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":7,\"Nom\":\"Force mental\",\"Definition\":\"Représente la volonté, le bon sens l\\u0027acuité et l\\u0027intuition d\\u0027un personnage, participe aux dégâts de sort à la résistance à la magie et aux effets de contrôle ....\",\"Stats\":6,\"Type\":0},\"Id\":21,\"Valeur\":20}],\"Id\":12},\"Id\":26,\"Nom\":\"Marchandage\",\"Definition\":\"Représente la réduction de prix que vous allez obtenir chez les marchant\",\"Stats\":24,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":4,\"Nom\":\"Force\",\"Definition\":\"Représente la puissance physique du personnage, votre aptitude à soulever des objets ou à les casser, participe aux dégâts et au poids transportable\",\"Stats\":3,\"Type\":0},\"Id\":22,\"Valeur\":25}],\"Id\":13},\"Id\":27,\"Nom\":\"Menace\",\"Definition\":\"Représente vos chances d\\u0027attirer l\\u0027attention des ennemies\",\"Stats\":25,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":9,\"Nom\":\"Sociabilité\",\"Definition\":\"Représente la personnalité et le magnétisme personnel, la capacité à convaincre et à se faire comprendre, participe au charisme, mensonge, marchandage ... mais aussi à l\\u0027amélioration des pets\",\"Stats\":8,\"Type\":0},\"Id\":23,\"Valeur\":75},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":6,\"Nom\":\"intelligence\",\"Definition\":\"représente la capacité d\\u0027apprentissage et de raisonnement de votre personnage, participe aux dégâts magiques, au pistage, réussite des sorts ...\",\"Stats\":5,\"Type\":0},\"Id\":24,\"Valeur\":25}],\"Id\":14},\"Id\":28,\"Nom\":\"Mensonge\",\"Definition\":\"Représente vos chances d\\u0027être crue quand vous ne dites pas la vérité\",\"Stats\":26,\"Type\":2},{\"StatCalculer\":null,\"Id\":29,\"Nom\":\"Mouvement\",\"Definition\":\"Représente votre vitesse de déplacement.\",\"Stats\":27,\"Type\":1},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":3,\"Nom\":\"Agilité\",\"Definition\":\"Représente les reflexes et l\\u0027équilibre , participe a votre initiative et a votre esquive\",\"Stats\":2,\"Type\":0},\"Id\":25,\"Valeur\":40},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":4,\"Nom\":\"Force\",\"Definition\":\"Représente la puissance physique du personnage, votre aptitude à soulever des objets ou à les casser, participe aux dégâts et au poids transportable\",\"Stats\":3,\"Type\":0},\"Id\":26,\"Valeur\":40}],\"Id\":15},\"Id\":30,\"Nom\":\"Natation\",\"Definition\":\"Représente votre habileté à vous déplacer sous l\\u0027eau\",\"Stats\":28,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":6,\"Nom\":\"intelligence\",\"Definition\":\"représente la capacité d\\u0027apprentissage et de raisonnement de votre personnage, participe aux dégâts magiques, au pistage, réussite des sorts ...\",\"Stats\":5,\"Type\":0},\"Id\":27,\"Valeur\":40},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":8,\"Nom\":\"Perception\",\"Definition\":\"Représente vote acuité à percevoir le monde qui vous entoure, à remarquer les pièges et les trace, participe au pistage et à toutes les détections\",\"Stats\":7,\"Type\":0},\"Id\":28,\"Valeur\":40}],\"Id\":16},\"Id\":31,\"Nom\":\"Navigation\",\"Definition\":\"Représente votre aptitude a dirigé toute sorte de bateau\",\"Stats\":29,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":6,\"Nom\":\"intelligence\",\"Definition\":\"représente la capacité d\\u0027apprentissage et de raisonnement de votre personnage, participe aux dégâts magiques, au pistage, réussite des sorts ...\",\"Stats\":5,\"Type\":0},\"Id\":29,\"Valeur\":60}],\"Id\":17},\"Id\":32,\"Nom\":\"Noeud\",\"Definition\":\"Représente votre aptitude à réussir des noeuds solides\",\"Stats\":30,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":8,\"Nom\":\"Perception\",\"Definition\":\"Représente vote acuité à percevoir le monde qui vous entoure, à remarquer les pièges et les trace, participe au pistage et à toutes les détections\",\"Stats\":7,\"Type\":0},\"Id\":30,\"Valeur\":75}],\"Id\":18},\"Id\":33,\"Nom\":\"Ouïe\",\"Definition\":\"Représente vos chances d\\u0027entendre les bruits les plus infimes\",\"Stats\":31,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":8,\"Nom\":\"Perception\",\"Definition\":\"Représente vote acuité à percevoir le monde qui vous entoure, à remarquer les pièges et les trace, participe au pistage et à toutes les détections\",\"Stats\":7,\"Type\":0},\"Id\":31,\"Valeur\":75}],\"Id\":19},\"Id\":34,\"Nom\":\"Odorat\",\"Definition\":\"Représente votre capacité à remarquer une odeur distante\",\"Stats\":32,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":8,\"Nom\":\"Perception\",\"Definition\":\"Représente vote acuité à percevoir le monde qui vous entoure, à remarquer les pièges et les trace, participe au pistage et à toutes les détections\",\"Stats\":7,\"Type\":0},\"Id\":32,\"Valeur\":75}],\"Id\":20},\"Id\":35,\"Nom\":\"Vue\",\"Definition\":\"Représente votre capacité a distingué de lointain objet ou de voir les mécanismes cachés\",\"Stats\":33,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":8,\"Nom\":\"Perception\",\"Definition\":\"Représente vote acuité à percevoir le monde qui vous entoure, à remarquer les pièges et les trace, participe au pistage et à toutes les détections\",\"Stats\":7,\"Type\":0},\"Id\":33,\"Valeur\":30},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":6,\"Nom\":\"intelligence\",\"Definition\":\"représente la capacité d\\u0027apprentissage et de raisonnement de votre personnage, participe aux dégâts magiques, au pistage, réussite des sorts ...\",\"Stats\":5,\"Type\":0},\"Id\":34,\"Valeur\":50}],\"Id\":21},\"Id\":36,\"Nom\":\"Perception Magique\",\"Definition\":\"Représente votre acuité à percevoir l\\u0027aura magique laissée par les sorts ou les objets enchantés\",\"Stats\":34,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":9,\"Nom\":\"Sociabilité\",\"Definition\":\"Représente la personnalité et le magnétisme personnel, la capacité à convaincre et à se faire comprendre, participe au charisme, mensonge, marchandage ... mais aussi à l\\u0027amélioration des pets\",\"Stats\":8,\"Type\":0},\"Id\":35,\"Valeur\":20},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":6,\"Nom\":\"intelligence\",\"Definition\":\"représente la capacité d\\u0027apprentissage et de raisonnement de votre personnage, participe aux dégâts magiques, au pistage, réussite des sorts ...\",\"Stats\":5,\"Type\":0},\"Id\":36,\"Valeur\":30}],\"Id\":22},\"Id\":37,\"Nom\":\"Force des pet\",\"Definition\":\"Représente la force bonus que vous donnez à vos créature et invocation\",\"Stats\":35,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":9,\"Nom\":\"Sociabilité\",\"Definition\":\"Représente la personnalité et le magnétisme personnel, la capacité à convaincre et à se faire comprendre, participe au charisme, mensonge, marchandage ... mais aussi à l\\u0027amélioration des pets\",\"Stats\":8,\"Type\":0},\"Id\":37,\"Valeur\":20},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":7,\"Nom\":\"Force mental\",\"Definition\":\"Représente la volonté, le bon sens l\\u0027acuité et l\\u0027intuition d\\u0027un personnage, participe aux dégâts de sort à la résistance à la magie et aux effets de contrôle ....\",\"Stats\":6,\"Type\":0},\"Id\":38,\"Valeur\":30}],\"Id\":23},\"Id\":38,\"Nom\":\"Endurance des pet\",\"Definition\":\"Représente l\\u0027endurance bonus que vous donnez à vos créatures ou invocation\",\"Stats\":36,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":9,\"Nom\":\"Sociabilité\",\"Definition\":\"Représente la personnalité et le magnétisme personnel, la capacité à convaincre et à se faire comprendre, participe au charisme, mensonge, marchandage ... mais aussi à l\\u0027amélioration des pets\",\"Stats\":8,\"Type\":0},\"Id\":39,\"Valeur\":50}],\"Id\":24},\"Id\":39,\"Nom\":\"Chance de crit des pet\",\"Definition\":\"Représente les chances de critique bonus que vous donnez à vos créatures et à vos invocations\",\"Stats\":37,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":9,\"Nom\":\"Sociabilité\",\"Definition\":\"Représente la personnalité et le magnétisme personnel, la capacité à convaincre et à se faire comprendre, participe au charisme, mensonge, marchandage ... mais aussi à l\\u0027amélioration des pets\",\"Stats\":8,\"Type\":0},\"Id\":40,\"Valeur\":20},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":3,\"Nom\":\"Agilité\",\"Definition\":\"Représente les reflexes et l\\u0027équilibre , participe a votre initiative et a votre esquive\",\"Stats\":2,\"Type\":0},\"Id\":41,\"Valeur\":30}],\"Id\":25},\"Id\":40,\"Nom\":\"Agilité des Pet\",\"Definition\":\"Représente l\\u0027agilité bonus que vous donnez à vos créatures et invocation\",\"Stats\":38,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":1,\"Nom\":\"Capacité de combat\",\"Definition\":\"Représente votre habileté au combat au corps a corps\",\"Stats\":0,\"Type\":0},\"Id\":42,\"Valeur\":15},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":2,\"Nom\":\"Capacité de tir\",\"Definition\":\"Représente votre habileté a jeter des objet et a tirer avec les arme a distance\",\"Stats\":1,\"Type\":0},\"Id\":43,\"Valeur\":15},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":9,\"Nom\":\"Sociabilité\",\"Definition\":\"Représente la personnalité et le magnétisme personnel, la capacité à convaincre et à se faire comprendre, participe au charisme, mensonge, marchandage ... mais aussi à l\\u0027amélioration des pets\",\"Stats\":8,\"Type\":0},\"Id\":44,\"Valeur\":20}],\"Id\":26},\"Id\":41,\"Nom\":\"Toucher des pet\",\"Definition\":\"Représente les chances de toucher bonus que vous donnez à vos créatures et vos invocations\",\"Stats\":39,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":9,\"Nom\":\"Sociabilité\",\"Definition\":\"Représente la personnalité et le magnétisme personnel, la capacité à convaincre et à se faire comprendre, participe au charisme, mensonge, marchandage ... mais aussi à l\\u0027amélioration des pets\",\"Stats\":8,\"Type\":0},\"Id\":45,\"Valeur\":20},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":7,\"Nom\":\"Force mental\",\"Definition\":\"Représente la volonté, le bon sens l\\u0027acuité et l\\u0027intuition d\\u0027un personnage, participe aux dégâts de sort à la résistance à la magie et aux effets de contrôle ....\",\"Stats\":6,\"Type\":0},\"Id\":46,\"Valeur\":30}],\"Id\":27},\"Id\":42,\"Nom\":\"Vie des pet\",\"Definition\":\"Représente la vie bonus que vous donnez à vos créatures et invocation\",\"Stats\":40,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":9,\"Nom\":\"Sociabilité\",\"Definition\":\"Représente la personnalité et le magnétisme personnel, la capacité à convaincre et à se faire comprendre, participe au charisme, mensonge, marchandage ... mais aussi à l\\u0027amélioration des pets\",\"Stats\":8,\"Type\":0},\"Id\":47,\"Valeur\":30}],\"Id\":28},\"Id\":43,\"Nom\":\"Degats critique des pet\",\"Definition\":\"Représente les dégâts critique bonus que vous donnez à vos créatures et à vos invocations\",\"Stats\":41,\"Type\":2},{\"StatCalculer\":null,\"Id\":44,\"Nom\":\"Nombre de grande creature controlable\",\"Definition\":\"Représente le nombre maximum de grande creature que vous pouvez controler\",\"Stats\":42,\"Type\":1},{\"StatCalculer\":null,\"Id\":45,\"Nom\":\"Nombre de creature moyenne controlable\",\"Definition\":\"Représente le nombre maximum de créature moyenne que vous pouvez contrôler\",\"Stats\":43,\"Type\":1},{\"StatCalculer\":null,\"Id\":46,\"Nom\":\"Nombre de petite creature controlable\",\"Definition\":\"Représente le nombre maximum de petite creature que vous pouvez controler\",\"Stats\":44,\"Type\":1},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":6,\"Nom\":\"intelligence\",\"Definition\":\"représente la capacité d\\u0027apprentissage et de raisonnement de votre personnage, participe aux dégâts magiques, au pistage, réussite des sorts ...\",\"Stats\":5,\"Type\":0},\"Id\":48,\"Valeur\":40},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":8,\"Nom\":\"Perception\",\"Definition\":\"Représente vote acuité à percevoir le monde qui vous entoure, à remarquer les pièges et les trace, participe au pistage et à toutes les détections\",\"Stats\":7,\"Type\":0},\"Id\":49,\"Valeur\":40}],\"Id\":29},\"Id\":47,\"Nom\":\"Pistage\",\"Definition\":\"Représente votre capacité à suivre la trace de quelqu\\u0027un et à avoir des informations sur ce qui a fait une trace\",\"Stats\":45,\"Type\":2},{\"StatCalculer\":null,\"Id\":48,\"Nom\":\"Point de destin\",\"Definition\":\"Représente le nombre de blessure grave que vous pouvez annuler\",\"Stats\":46,\"Type\":1},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":7,\"Nom\":\"Force mental\",\"Definition\":\"Représente la volonté, le bon sens l\\u0027acuité et l\\u0027intuition d\\u0027un personnage, participe aux dégâts de sort à la résistance à la magie et aux effets de contrôle ....\",\"Stats\":6,\"Type\":0},\"Id\":50,\"Valeur\":75},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":6,\"Nom\":\"intelligence\",\"Definition\":\"représente la capacité d\\u0027apprentissage et de raisonnement de votre personnage, participe aux dégâts magiques, au pistage, réussite des sorts ...\",\"Stats\":5,\"Type\":0},\"Id\":51,\"Valeur\":75}],\"Id\":30},\"Id\":49,\"Nom\":\"Puissance des sort\",\"Definition\":\"Représente le bonus au effet de vos sort\",\"Stats\":47,\"Type\":2},{\"StatCalculer\":null,\"Id\":50,\"Nom\":\"Ratio XP\",\"Definition\":\"Représente le pourcentage d\\u0027xp gagné par le personnage\",\"Stats\":48,\"Type\":1},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":7,\"Nom\":\"Force mental\",\"Definition\":\"Représente la volonté, le bon sens l\\u0027acuité et l\\u0027intuition d\\u0027un personnage, participe aux dégâts de sort à la résistance à la magie et aux effets de contrôle ....\",\"Stats\":6,\"Type\":0},\"Id\":52,\"Valeur\":50}],\"Id\":31},\"Id\":51,\"Nom\":\"Resistance au echo\",\"Definition\":\"Représente votre capacité a effectué des sorts sans que les démons ne vous utilisent pour agir sur le monde\",\"Stats\":49,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":5,\"Nom\":\"Endurance\",\"Definition\":\"Représente la robustesse de votre personnage, sa capacité a résisté aux attaques physiques aux maladies, poison ... participe grandement aux réductions de dégâts\",\"Stats\":4,\"Type\":0},\"Id\":53,\"Valeur\":80}],\"Id\":32},\"Id\":52,\"Nom\":\"Resistance a l\\u0027alcool\",\"Definition\":\"Représente votre aptitude à résister aux effets de l\\u0027alcool\",\"Stats\":50,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":1,\"Nom\":\"Capacité de combat\",\"Definition\":\"Représente votre habileté au combat au corps a corps\",\"Stats\":0,\"Type\":0},\"Id\":54,\"Valeur\":50},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":3,\"Nom\":\"Agilité\",\"Definition\":\"Représente les reflexes et l\\u0027équilibre , participe a votre initiative et a votre esquive\",\"Stats\":2,\"Type\":0},\"Id\":55,\"Valeur\":70}],\"Id\":33},\"Id\":53,\"Nom\":\"Esquive corps a corps\",\"Definition\":\"Représente les chances de ne pas être toucher par des attaques au corps à corps\",\"Stats\":51,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":3,\"Nom\":\"Agilité\",\"Definition\":\"Représente les reflexes et l\\u0027équilibre , participe a votre initiative et a votre esquive\",\"Stats\":2,\"Type\":0},\"Id\":56,\"Valeur\":70},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":8,\"Nom\":\"Perception\",\"Definition\":\"Représente vote acuité à percevoir le monde qui vous entoure, à remarquer les pièges et les trace, participe au pistage et à toutes les détections\",\"Stats\":7,\"Type\":0},\"Id\":57,\"Valeur\":30}],\"Id\":34},\"Id\":54,\"Nom\":\"Esquive distance\",\"Definition\":\"Représente les chances de ne pas être toucher par une attaque à distance\",\"Stats\":52,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":5,\"Nom\":\"Endurance\",\"Definition\":\"Représente la robustesse de votre personnage, sa capacité a résisté aux attaques physiques aux maladies, poison ... participe grandement aux réductions de dégâts\",\"Stats\":4,\"Type\":0},\"Id\":58,\"Valeur\":50}],\"Id\":35},\"Id\":55,\"Nom\":\"Resistance au maladie\",\"Definition\":\"Représente vos chances de ne pas contracter la maladie\",\"Stats\":53,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":6,\"Nom\":\"intelligence\",\"Definition\":\"représente la capacité d\\u0027apprentissage et de raisonnement de votre personnage, participe aux dégâts magiques, au pistage, réussite des sorts ...\",\"Stats\":5,\"Type\":0},\"Id\":59,\"Valeur\":50}],\"Id\":36},\"Id\":56,\"Nom\":\"Réussite des sort\",\"Definition\":\"Représente vos chances de réussir des sorts compliquer\",\"Stats\":54,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":6,\"Nom\":\"intelligence\",\"Definition\":\"représente la capacité d\\u0027apprentissage et de raisonnement de votre personnage, participe aux dégâts magiques, au pistage, réussite des sorts ...\",\"Stats\":5,\"Type\":0},\"Id\":60,\"Valeur\":10},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":7,\"Nom\":\"Force mental\",\"Definition\":\"Représente la volonté, le bon sens l\\u0027acuité et l\\u0027intuition d\\u0027un personnage, participe aux dégâts de sort à la résistance à la magie et aux effets de contrôle ....\",\"Stats\":6,\"Type\":0},\"Id\":61,\"Valeur\":10}],\"Id\":37},\"Id\":57,\"Nom\":\"Torture\",\"Definition\":\"Représente votre habiller à faire parler votre cible sans la tuée\",\"Stats\":55,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":3,\"Nom\":\"Agilité\",\"Definition\":\"Représente les reflexes et l\\u0027équilibre , participe a votre initiative et a votre esquive\",\"Stats\":2,\"Type\":0},\"Id\":62,\"Valeur\":60},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":6,\"Nom\":\"intelligence\",\"Definition\":\"représente la capacité d\\u0027apprentissage et de raisonnement de votre personnage, participe aux dégâts magiques, au pistage, réussite des sorts ...\",\"Stats\":5,\"Type\":0},\"Id\":63,\"Valeur\":30}],\"Id\":38},\"Id\":58,\"Nom\":\"Vol a la tir\",\"Definition\":\"Représente votre habileté à faire les poches de votre cible sans être repéré\",\"Stats\":56,\"Type\":2},{\"StatCalculer\":null,\"Id\":59,\"Nom\":\"Maitrise\",\"Definition\":\"Il s\\u0027agit d\\u0027une stat qui a un effet différent pour chaque classe\",\"Stats\":57,\"Type\":2},{\"StatCalculer\":null,\"Id\":60,\"Nom\":\"Nombre de mains\",\"Definition\":\"Bien que la majoriser des gens en possède deux il est possible suite à des accidents magiques d\\u0027en avoir plus, beaucoup plus\",\"Stats\":58,\"Type\":1},{\"StatCalculer\":null,\"Id\":61,\"Nom\":\"Vision nocturne\",\"Definition\":\"Représente vos chances de distinguer quelque chose dans la pénombre\",\"Stats\":59,\"Type\":2},{\"StatCalculer\":null,\"Id\":62,\"Nom\":\"Régénération\",\"Definition\":\"Représente le nombre de points de vie que vous régénérez par tour\",\"Stats\":60,\"Type\":1},{\"StatCalculer\":null,\"Id\":63,\"Nom\":\"Régénération par nuit\",\"Definition\":\"Représente le nombre de points de vie que vous régénérez après une bonne nuit de sommeil\",\"Stats\":61,\"Type\":1},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":6,\"Nom\":\"intelligence\",\"Definition\":\"représente la capacité d\\u0027apprentissage et de raisonnement de votre personnage, participe aux dégâts magiques, au pistage, réussite des sorts ...\",\"Stats\":5,\"Type\":0},\"Id\":64,\"Valeur\":40},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":8,\"Nom\":\"Perception\",\"Definition\":\"Représente vote acuité à percevoir le monde qui vous entoure, à remarquer les pièges et les trace, participe au pistage et à toutes les détections\",\"Stats\":7,\"Type\":0},\"Id\":65,\"Valeur\":40}],\"Id\":39},\"Id\":64,\"Nom\":\"Cartographie\",\"Definition\":\"Représente votre habileté à comprendre ou à dessiner une carte\",\"Stats\":62,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":6,\"Nom\":\"intelligence\",\"Definition\":\"représente la capacité d\\u0027apprentissage et de raisonnement de votre personnage, participe aux dégâts magiques, au pistage, réussite des sorts ...\",\"Stats\":5,\"Type\":0},\"Id\":66,\"Valeur\":50},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":8,\"Nom\":\"Perception\",\"Definition\":\"Représente vote acuité à percevoir le monde qui vous entoure, à remarquer les pièges et les trace, participe au pistage et à toutes les détections\",\"Stats\":7,\"Type\":0},\"Id\":67,\"Valeur\":50}],\"Id\":40},\"Id\":65,\"Nom\":\"Heboristerie\",\"Definition\":\"Représente votre habileté à reconnaitre les plantes et à connaitre leur effet\",\"Stats\":63,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":6,\"Nom\":\"intelligence\",\"Definition\":\"représente la capacité d\\u0027apprentissage et de raisonnement de votre personnage, participe aux dégâts magiques, au pistage, réussite des sorts ...\",\"Stats\":5,\"Type\":0},\"Id\":68,\"Valeur\":80}],\"Id\":41},\"Id\":66,\"Nom\":\"Alchimie\",\"Definition\":\"Représente votre habileté à faire des potions et à les utiliser, augmente l\\u0027effet des potions sur vous\",\"Stats\":64,\"Type\":2},{\"StatCalculer\":null,\"Id\":67,\"Nom\":\"Parade\",\"Definition\":\"Représente vos chances de bloquer une attaque\",\"Stats\":65,\"Type\":2},{\"StatCalculer\":null,\"Id\":68,\"Nom\":\"Riposte\",\"Definition\":\"Représente vos chances de contre-attaquer lorsque vous subissez une attaque au corps-à-corps\",\"Stats\":66,\"Type\":2},{\"StatCalculer\":null,\"Id\":69,\"Nom\":\"Nombre de riposte\",\"Definition\":\"Représente le nombre maximum de riposte par tour\",\"Stats\":67,\"Type\":1},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":5,\"Nom\":\"Endurance\",\"Definition\":\"Représente la robustesse de votre personnage, sa capacité a résisté aux attaques physiques aux maladies, poison ... participe grandement aux réductions de dégâts\",\"Stats\":4,\"Type\":0},\"Id\":69,\"Valeur\":60}],\"Id\":42},\"Id\":70,\"Nom\":\"Resistance au mutation\",\"Definition\":\"Représente vos chances de ne pas subir une mutation\",\"Stats\":68,\"Type\":2},{\"StatCalculer\":null,\"Id\":71,\"Nom\":\"Armure\",\"Definition\":\"Représente la réduction de dégâts physiques conférés par vos équipements\",\"Stats\":69,\"Type\":1},{\"StatCalculer\":null,\"Id\":72,\"Nom\":\"Resistance magique\",\"Definition\":\"Représente la réduction de dégâts magiques conférés par votre équipement\",\"Stats\":70,\"Type\":1},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":5,\"Nom\":\"Endurance\",\"Definition\":\"Représente la robustesse de votre personnage, sa capacité a résisté aux attaques physiques aux maladies, poison ... participe grandement aux réductions de dégâts\",\"Stats\":4,\"Type\":0},\"Id\":70,\"Valeur\":50},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":71,\"Nom\":\"Armure\",\"Definition\":\"Représente la réduction de dégâts physiques conférés par vos équipements\",\"Stats\":69,\"Type\":1},\"Id\":71,\"Valeur\":100}],\"Id\":43},\"Id\":73,\"Nom\":\"Reduction de dégâts contondant\",\"Definition\":\"Représente la réduction de dégâts appliquer face à des dégâts contondant\",\"Stats\":71,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":5,\"Nom\":\"Endurance\",\"Definition\":\"Représente la robustesse de votre personnage, sa capacité a résisté aux attaques physiques aux maladies, poison ... participe grandement aux réductions de dégâts\",\"Stats\":4,\"Type\":0},\"Id\":72,\"Valeur\":50},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":71,\"Nom\":\"Armure\",\"Definition\":\"Représente la réduction de dégâts physiques conférés par vos équipements\",\"Stats\":69,\"Type\":1},\"Id\":73,\"Valeur\":100}],\"Id\":44},\"Id\":74,\"Nom\":\"Reduction de dégâts perforant\",\"Definition\":\"Représente la réduction de dégâts appliquer face à des dégâts perforant\",\"Stats\":72,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":5,\"Nom\":\"Endurance\",\"Definition\":\"Représente la robustesse de votre personnage, sa capacité a résisté aux attaques physiques aux maladies, poison ... participe grandement aux réductions de dégâts\",\"Stats\":4,\"Type\":0},\"Id\":74,\"Valeur\":50},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":71,\"Nom\":\"Armure\",\"Definition\":\"Représente la réduction de dégâts physiques conférés par vos équipements\",\"Stats\":69,\"Type\":1},\"Id\":75,\"Valeur\":100}],\"Id\":45},\"Id\":75,\"Nom\":\"Reduction de dégâts tranchant\",\"Definition\":\"Représente la réduction de dégâts appliquer face à des dégâts tranchant\",\"Stats\":73,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":5,\"Nom\":\"Endurance\",\"Definition\":\"Représente la robustesse de votre personnage, sa capacité a résisté aux attaques physiques aux maladies, poison ... participe grandement aux réductions de dégâts\",\"Stats\":4,\"Type\":0},\"Id\":76,\"Valeur\":30},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":7,\"Nom\":\"Force mental\",\"Definition\":\"Représente la volonté, le bon sens l\\u0027acuité et l\\u0027intuition d\\u0027un personnage, participe aux dégâts de sort à la résistance à la magie et aux effets de contrôle ....\",\"Stats\":6,\"Type\":0},\"Id\":77,\"Valeur\":30},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":72,\"Nom\":\"Resistance magique\",\"Definition\":\"Représente la réduction de dégâts magiques conférés par votre équipement\",\"Stats\":70,\"Type\":1},\"Id\":78,\"Valeur\":100}],\"Id\":46},\"Id\":76,\"Nom\":\"Reduction de dégâts de feu\",\"Definition\":\"Représente la réduction de dégâts appliquer face à des dégâts de feu\",\"Stats\":74,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":5,\"Nom\":\"Endurance\",\"Definition\":\"Représente la robustesse de votre personnage, sa capacité a résisté aux attaques physiques aux maladies, poison ... participe grandement aux réductions de dégâts\",\"Stats\":4,\"Type\":0},\"Id\":79,\"Valeur\":30},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":7,\"Nom\":\"Force mental\",\"Definition\":\"Représente la volonté, le bon sens l\\u0027acuité et l\\u0027intuition d\\u0027un personnage, participe aux dégâts de sort à la résistance à la magie et aux effets de contrôle ....\",\"Stats\":6,\"Type\":0},\"Id\":80,\"Valeur\":30},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":72,\"Nom\":\"Resistance magique\",\"Definition\":\"Représente la réduction de dégâts magiques conférés par votre équipement\",\"Stats\":70,\"Type\":1},\"Id\":81,\"Valeur\":100}],\"Id\":47},\"Id\":77,\"Nom\":\"Reduction de dégâts de froid\",\"Definition\":\"Représente la réduction de dégâts appliquer face à des dégâts de froid\",\"Stats\":75,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":5,\"Nom\":\"Endurance\",\"Definition\":\"Représente la robustesse de votre personnage, sa capacité a résisté aux attaques physiques aux maladies, poison ... participe grandement aux réductions de dégâts\",\"Stats\":4,\"Type\":0},\"Id\":82,\"Valeur\":30},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":7,\"Nom\":\"Force mental\",\"Definition\":\"Représente la volonté, le bon sens l\\u0027acuité et l\\u0027intuition d\\u0027un personnage, participe aux dégâts de sort à la résistance à la magie et aux effets de contrôle ....\",\"Stats\":6,\"Type\":0},\"Id\":83,\"Valeur\":30},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":72,\"Nom\":\"Resistance magique\",\"Definition\":\"Représente la réduction de dégâts magiques conférés par votre équipement\",\"Stats\":70,\"Type\":1},\"Id\":84,\"Valeur\":100}],\"Id\":48},\"Id\":78,\"Nom\":\"Reduction de dégâts de foudre\",\"Definition\":\"Représente la réduction de dégâts appliquer face à des dégâts electrique\",\"Stats\":76,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":72,\"Nom\":\"Resistance magique\",\"Definition\":\"Représente la réduction de dégâts magiques conférés par votre équipement\",\"Stats\":70,\"Type\":1},\"Id\":85,\"Valeur\":100}],\"Id\":49},\"Id\":79,\"Nom\":\"Reduction de dégâts du chaos\",\"Definition\":\"Représente la réduction de dégâts appliquer face à des dégâts de chaos\",\"Stats\":77,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":5,\"Nom\":\"Endurance\",\"Definition\":\"Représente la robustesse de votre personnage, sa capacité a résisté aux attaques physiques aux maladies, poison ... participe grandement aux réductions de dégâts\",\"Stats\":4,\"Type\":0},\"Id\":86,\"Valeur\":30},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":7,\"Nom\":\"Force mental\",\"Definition\":\"Représente la volonté, le bon sens l\\u0027acuité et l\\u0027intuition d\\u0027un personnage, participe aux dégâts de sort à la résistance à la magie et aux effets de contrôle ....\",\"Stats\":6,\"Type\":0},\"Id\":87,\"Valeur\":30}],\"Id\":50},\"Id\":80,\"Nom\":\"Reduction de dégâts de poison\",\"Definition\":\"Représente la réduction de dégâts appliquer face à des dégâts de poison\",\"Stats\":78,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":5,\"Nom\":\"Endurance\",\"Definition\":\"Représente la robustesse de votre personnage, sa capacité a résisté aux attaques physiques aux maladies, poison ... participe grandement aux réductions de dégâts\",\"Stats\":4,\"Type\":0},\"Id\":88,\"Valeur\":40},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":7,\"Nom\":\"Force mental\",\"Definition\":\"Représente la volonté, le bon sens l\\u0027acuité et l\\u0027intuition d\\u0027un personnage, participe aux dégâts de sort à la résistance à la magie et aux effets de contrôle ....\",\"Stats\":6,\"Type\":0},\"Id\":89,\"Valeur\":30}],\"Id\":51},\"Id\":81,\"Nom\":\"Reduction de dégâts de maladie\",\"Definition\":\"Représente la réduction de dégâts appliquer face à des dégâts de maladie\",\"Stats\":79,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":5,\"Nom\":\"Endurance\",\"Definition\":\"Représente la robustesse de votre personnage, sa capacité a résisté aux attaques physiques aux maladies, poison ... participe grandement aux réductions de dégâts\",\"Stats\":4,\"Type\":0},\"Id\":90,\"Valeur\":60}],\"Id\":52},\"Id\":82,\"Nom\":\"Reduction de dégâts de saignement\",\"Definition\":\"Représente la réduction de dégâts appliquer face à des dégâts de saignement\",\"Stats\":80,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":5,\"Nom\":\"Endurance\",\"Definition\":\"Représente la robustesse de votre personnage, sa capacité a résisté aux attaques physiques aux maladies, poison ... participe grandement aux réductions de dégâts\",\"Stats\":4,\"Type\":0},\"Id\":91,\"Valeur\":30},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":7,\"Nom\":\"Force mental\",\"Definition\":\"Représente la volonté, le bon sens l\\u0027acuité et l\\u0027intuition d\\u0027un personnage, participe aux dégâts de sort à la résistance à la magie et aux effets de contrôle ....\",\"Stats\":6,\"Type\":0},\"Id\":92,\"Valeur\":30},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":72,\"Nom\":\"Resistance magique\",\"Definition\":\"Représente la réduction de dégâts magiques conférés par votre équipement\",\"Stats\":70,\"Type\":1},\"Id\":93,\"Valeur\":100}],\"Id\":53},\"Id\":83,\"Nom\":\"Reduction de dégâts de sacré\",\"Definition\":\"Représente la réduction de dégâts appliquer face à des dégâts sacré\",\"Stats\":81,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":5,\"Nom\":\"Endurance\",\"Definition\":\"Représente la robustesse de votre personnage, sa capacité a résisté aux attaques physiques aux maladies, poison ... participe grandement aux réductions de dégâts\",\"Stats\":4,\"Type\":0},\"Id\":94,\"Valeur\":30},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":7,\"Nom\":\"Force mental\",\"Definition\":\"Représente la volonté, le bon sens l\\u0027acuité et l\\u0027intuition d\\u0027un personnage, participe aux dégâts de sort à la résistance à la magie et aux effets de contrôle ....\",\"Stats\":6,\"Type\":0},\"Id\":95,\"Valeur\":30},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":72,\"Nom\":\"Resistance magique\",\"Definition\":\"Représente la réduction de dégâts magiques conférés par votre équipement\",\"Stats\":70,\"Type\":1},\"Id\":96,\"Valeur\":100}],\"Id\":54},\"Id\":84,\"Nom\":\"Reduction de dégâts d\\u0027ombre\",\"Definition\":\"Représente la réduction de dégâts appliquer face à des dégâts d\\u0027ombre\",\"Stats\":82,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":72,\"Nom\":\"Resistance magique\",\"Definition\":\"Représente la réduction de dégâts magiques conférés par votre équipement\",\"Stats\":70,\"Type\":1},\"Id\":97,\"Valeur\":100},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":7,\"Nom\":\"Force mental\",\"Definition\":\"Représente la volonté, le bon sens l\\u0027acuité et l\\u0027intuition d\\u0027un personnage, participe aux dégâts de sort à la résistance à la magie et aux effets de contrôle ....\",\"Stats\":6,\"Type\":0},\"Id\":98,\"Valeur\":50}],\"Id\":55},\"Id\":85,\"Nom\":\"Reduction de dégâts d\\u0027esprit\",\"Definition\":\"Représente la réduction de dégâts appliquer face à des dégâts d\\u0027esprit\",\"Stats\":83,\"Type\":2},{\"StatCalculer\":null,\"Id\":86,\"Nom\":\"Dégâts bonus\",\"Definition\":\"Représente le bonus de dégâts ajoute à tous les types de dégâts\",\"Stats\":84,\"Type\":1},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":4,\"Nom\":\"Force\",\"Definition\":\"Représente la puissance physique du personnage, votre aptitude à soulever des objets ou à les casser, participe aux dégâts et au poids transportable\",\"Stats\":3,\"Type\":0},\"Id\":99,\"Valeur\":100},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":86,\"Nom\":\"Dégâts bonus\",\"Definition\":\"Représente le bonus de dégâts ajoute à tous les types de dégâts\",\"Stats\":84,\"Type\":1},\"Id\":100,\"Valeur\":100}],\"Id\":56},\"Id\":87,\"Nom\":\"Dégâts Contondant\",\"Definition\":\"Représente le bonus de dégâts ajouté à vos dégâts contondant\",\"Stats\":85,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":4,\"Nom\":\"Force\",\"Definition\":\"Représente la puissance physique du personnage, votre aptitude à soulever des objets ou à les casser, participe aux dégâts et au poids transportable\",\"Stats\":3,\"Type\":0},\"Id\":101,\"Valeur\":70},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":86,\"Nom\":\"Dégâts bonus\",\"Definition\":\"Représente le bonus de dégâts ajoute à tous les types de dégâts\",\"Stats\":84,\"Type\":1},\"Id\":102,\"Valeur\":100},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":3,\"Nom\":\"Agilité\",\"Definition\":\"Représente les reflexes et l\\u0027équilibre , participe a votre initiative et a votre esquive\",\"Stats\":2,\"Type\":0},\"Id\":103,\"Valeur\":30}],\"Id\":57},\"Id\":88,\"Nom\":\"Dégâts Perforant\",\"Definition\":\"Représente le bonus de dégâts ajouté à vos dégâts perforant\",\"Stats\":86,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":4,\"Nom\":\"Force\",\"Definition\":\"Représente la puissance physique du personnage, votre aptitude à soulever des objets ou à les casser, participe aux dégâts et au poids transportable\",\"Stats\":3,\"Type\":0},\"Id\":104,\"Valeur\":70},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":1,\"Nom\":\"Capacité de combat\",\"Definition\":\"Représente votre habileté au combat au corps a corps\",\"Stats\":0,\"Type\":0},\"Id\":105,\"Valeur\":15},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":3,\"Nom\":\"Agilité\",\"Definition\":\"Représente les reflexes et l\\u0027équilibre , participe a votre initiative et a votre esquive\",\"Stats\":2,\"Type\":0},\"Id\":106,\"Valeur\":15},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":86,\"Nom\":\"Dégâts bonus\",\"Definition\":\"Représente le bonus de dégâts ajoute à tous les types de dégâts\",\"Stats\":84,\"Type\":1},\"Id\":107,\"Valeur\":100}],\"Id\":58},\"Id\":89,\"Nom\":\"Dégâts tranchant\",\"Definition\":\"Représente le bonus de dégâts ajouté à vos dégâts tranchant\",\"Stats\":87,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":7,\"Nom\":\"Force mental\",\"Definition\":\"Représente la volonté, le bon sens l\\u0027acuité et l\\u0027intuition d\\u0027un personnage, participe aux dégâts de sort à la résistance à la magie et aux effets de contrôle ....\",\"Stats\":6,\"Type\":0},\"Id\":50,\"Valeur\":75},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":6,\"Nom\":\"intelligence\",\"Definition\":\"représente la capacité d\\u0027apprentissage et de raisonnement de votre personnage, participe aux dégâts magiques, au pistage, réussite des sorts ...\",\"Stats\":5,\"Type\":0},\"Id\":51,\"Valeur\":75}],\"Id\":30},\"Id\":49,\"Nom\":\"Puissance des sort\",\"Definition\":\"Représente le bonus au effet de vos sort\",\"Stats\":47,\"Type\":2},\"Id\":108,\"Valeur\":70},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":86,\"Nom\":\"Dégâts bonus\",\"Definition\":\"Représente le bonus de dégâts ajoute à tous les types de dégâts\",\"Stats\":84,\"Type\":1},\"Id\":109,\"Valeur\":100}],\"Id\":59},\"Id\":90,\"Nom\":\"Dégâts de feu\",\"Definition\":\"Représente le bonus de dégâts ajouté à vos dégâts de feu\",\"Stats\":88,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":7,\"Nom\":\"Force mental\",\"Definition\":\"Représente la volonté, le bon sens l\\u0027acuité et l\\u0027intuition d\\u0027un personnage, participe aux dégâts de sort à la résistance à la magie et aux effets de contrôle ....\",\"Stats\":6,\"Type\":0},\"Id\":50,\"Valeur\":75},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":6,\"Nom\":\"intelligence\",\"Definition\":\"représente la capacité d\\u0027apprentissage et de raisonnement de votre personnage, participe aux dégâts magiques, au pistage, réussite des sorts ...\",\"Stats\":5,\"Type\":0},\"Id\":51,\"Valeur\":75}],\"Id\":30},\"Id\":49,\"Nom\":\"Puissance des sort\",\"Definition\":\"Représente le bonus au effet de vos sort\",\"Stats\":47,\"Type\":2},\"Id\":110,\"Valeur\":70},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":86,\"Nom\":\"Dégâts bonus\",\"Definition\":\"Représente le bonus de dégâts ajoute à tous les types de dégâts\",\"Stats\":84,\"Type\":1},\"Id\":111,\"Valeur\":100}],\"Id\":60},\"Id\":91,\"Nom\":\"Dégâts de froid\",\"Definition\":\"Représente le bonus de dégâts ajouté à vos dégâts de froid\",\"Stats\":89,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":7,\"Nom\":\"Force mental\",\"Definition\":\"Représente la volonté, le bon sens l\\u0027acuité et l\\u0027intuition d\\u0027un personnage, participe aux dégâts de sort à la résistance à la magie et aux effets de contrôle ....\",\"Stats\":6,\"Type\":0},\"Id\":50,\"Valeur\":75},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":6,\"Nom\":\"intelligence\",\"Definition\":\"représente la capacité d\\u0027apprentissage et de raisonnement de votre personnage, participe aux dégâts magiques, au pistage, réussite des sorts ...\",\"Stats\":5,\"Type\":0},\"Id\":51,\"Valeur\":75}],\"Id\":30},\"Id\":49,\"Nom\":\"Puissance des sort\",\"Definition\":\"Représente le bonus au effet de vos sort\",\"Stats\":47,\"Type\":2},\"Id\":112,\"Valeur\":70},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":86,\"Nom\":\"Dégâts bonus\",\"Definition\":\"Représente le bonus de dégâts ajoute à tous les types de dégâts\",\"Stats\":84,\"Type\":1},\"Id\":113,\"Valeur\":100}],\"Id\":61},\"Id\":92,\"Nom\":\"Dégâts de foudre\",\"Definition\":\"Représente le bonus de dégâts ajouté à vos dégâts de foudre\",\"Stats\":90,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":7,\"Nom\":\"Force mental\",\"Definition\":\"Représente la volonté, le bon sens l\\u0027acuité et l\\u0027intuition d\\u0027un personnage, participe aux dégâts de sort à la résistance à la magie et aux effets de contrôle ....\",\"Stats\":6,\"Type\":0},\"Id\":50,\"Valeur\":75},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":6,\"Nom\":\"intelligence\",\"Definition\":\"représente la capacité d\\u0027apprentissage et de raisonnement de votre personnage, participe aux dégâts magiques, au pistage, réussite des sorts ...\",\"Stats\":5,\"Type\":0},\"Id\":51,\"Valeur\":75}],\"Id\":30},\"Id\":49,\"Nom\":\"Puissance des sort\",\"Definition\":\"Représente le bonus au effet de vos sort\",\"Stats\":47,\"Type\":2},\"Id\":114,\"Valeur\":20},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":86,\"Nom\":\"Dégâts bonus\",\"Definition\":\"Représente le bonus de dégâts ajoute à tous les types de dégâts\",\"Stats\":84,\"Type\":1},\"Id\":115,\"Valeur\":100}],\"Id\":62},\"Id\":93,\"Nom\":\"Dégâts de chaos\",\"Definition\":\"Représente le bonus de dégâts ajouté à vos dégâts de chaos\",\"Stats\":91,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":7,\"Nom\":\"Force mental\",\"Definition\":\"Représente la volonté, le bon sens l\\u0027acuité et l\\u0027intuition d\\u0027un personnage, participe aux dégâts de sort à la résistance à la magie et aux effets de contrôle ....\",\"Stats\":6,\"Type\":0},\"Id\":50,\"Valeur\":75},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":6,\"Nom\":\"intelligence\",\"Definition\":\"représente la capacité d\\u0027apprentissage et de raisonnement de votre personnage, participe aux dégâts magiques, au pistage, réussite des sorts ...\",\"Stats\":5,\"Type\":0},\"Id\":51,\"Valeur\":75}],\"Id\":30},\"Id\":49,\"Nom\":\"Puissance des sort\",\"Definition\":\"Représente le bonus au effet de vos sort\",\"Stats\":47,\"Type\":2},\"Id\":116,\"Valeur\":70},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":86,\"Nom\":\"Dégâts bonus\",\"Definition\":\"Représente le bonus de dégâts ajoute à tous les types de dégâts\",\"Stats\":84,\"Type\":1},\"Id\":117,\"Valeur\":100}],\"Id\":63},\"Id\":94,\"Nom\":\"Dégâts de poison\",\"Definition\":\"Représente le bonus de dégâts ajouté à vos dégâts de poison\",\"Stats\":92,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":7,\"Nom\":\"Force mental\",\"Definition\":\"Représente la volonté, le bon sens l\\u0027acuité et l\\u0027intuition d\\u0027un personnage, participe aux dégâts de sort à la résistance à la magie et aux effets de contrôle ....\",\"Stats\":6,\"Type\":0},\"Id\":50,\"Valeur\":75},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":6,\"Nom\":\"intelligence\",\"Definition\":\"représente la capacité d\\u0027apprentissage et de raisonnement de votre personnage, participe aux dégâts magiques, au pistage, réussite des sorts ...\",\"Stats\":5,\"Type\":0},\"Id\":51,\"Valeur\":75}],\"Id\":30},\"Id\":49,\"Nom\":\"Puissance des sort\",\"Definition\":\"Représente le bonus au effet de vos sort\",\"Stats\":47,\"Type\":2},\"Id\":118,\"Valeur\":70},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":86,\"Nom\":\"Dégâts bonus\",\"Definition\":\"Représente le bonus de dégâts ajoute à tous les types de dégâts\",\"Stats\":84,\"Type\":1},\"Id\":119,\"Valeur\":100}],\"Id\":64},\"Id\":95,\"Nom\":\"Dégâts de maladie\",\"Definition\":\"Représente le bonus de dégâts ajouté à vos dégâts de maladie\",\"Stats\":93,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":3,\"Nom\":\"Agilité\",\"Definition\":\"Représente les reflexes et l\\u0027équilibre , participe a votre initiative et a votre esquive\",\"Stats\":2,\"Type\":0},\"Id\":120,\"Valeur\":70},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":86,\"Nom\":\"Dégâts bonus\",\"Definition\":\"Représente le bonus de dégâts ajoute à tous les types de dégâts\",\"Stats\":84,\"Type\":1},\"Id\":121,\"Valeur\":100}],\"Id\":65},\"Id\":96,\"Nom\":\"Dégâts de saignement\",\"Definition\":\"Représente le bonus de dégâts ajouté à vos dégâts de saignement\",\"Stats\":94,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":7,\"Nom\":\"Force mental\",\"Definition\":\"Représente la volonté, le bon sens l\\u0027acuité et l\\u0027intuition d\\u0027un personnage, participe aux dégâts de sort à la résistance à la magie et aux effets de contrôle ....\",\"Stats\":6,\"Type\":0},\"Id\":50,\"Valeur\":75},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":6,\"Nom\":\"intelligence\",\"Definition\":\"représente la capacité d\\u0027apprentissage et de raisonnement de votre personnage, participe aux dégâts magiques, au pistage, réussite des sorts ...\",\"Stats\":5,\"Type\":0},\"Id\":51,\"Valeur\":75}],\"Id\":30},\"Id\":49,\"Nom\":\"Puissance des sort\",\"Definition\":\"Représente le bonus au effet de vos sort\",\"Stats\":47,\"Type\":2},\"Id\":122,\"Valeur\":70},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":86,\"Nom\":\"Dégâts bonus\",\"Definition\":\"Représente le bonus de dégâts ajoute à tous les types de dégâts\",\"Stats\":84,\"Type\":1},\"Id\":123,\"Valeur\":100}],\"Id\":66},\"Id\":97,\"Nom\":\"Dégâts de sacré\",\"Definition\":\"Représente le bonus de dégâts ajouté à vos dégâts sacré\",\"Stats\":95,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":7,\"Nom\":\"Force mental\",\"Definition\":\"Représente la volonté, le bon sens l\\u0027acuité et l\\u0027intuition d\\u0027un personnage, participe aux dégâts de sort à la résistance à la magie et aux effets de contrôle ....\",\"Stats\":6,\"Type\":0},\"Id\":50,\"Valeur\":75},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":6,\"Nom\":\"intelligence\",\"Definition\":\"représente la capacité d\\u0027apprentissage et de raisonnement de votre personnage, participe aux dégâts magiques, au pistage, réussite des sorts ...\",\"Stats\":5,\"Type\":0},\"Id\":51,\"Valeur\":75}],\"Id\":30},\"Id\":49,\"Nom\":\"Puissance des sort\",\"Definition\":\"Représente le bonus au effet de vos sort\",\"Stats\":47,\"Type\":2},\"Id\":124,\"Valeur\":70},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":86,\"Nom\":\"Dégâts bonus\",\"Definition\":\"Représente le bonus de dégâts ajoute à tous les types de dégâts\",\"Stats\":84,\"Type\":1},\"Id\":125,\"Valeur\":100}],\"Id\":67},\"Id\":98,\"Nom\":\"Dégâts d\\u0027ombre\",\"Definition\":\"Représente le bonus de dégâts ajouté à vos dégâts d\\u0027ombre\",\"Stats\":96,\"Type\":2},{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":{\"ListStatUtils\":[{\"StatUtile\":{\"StatCalculer\":null,\"Id\":7,\"Nom\":\"Force mental\",\"Definition\":\"Représente la volonté, le bon sens l\\u0027acuité et l\\u0027intuition d\\u0027un personnage, participe aux dégâts de sort à la résistance à la magie et aux effets de contrôle ....\",\"Stats\":6,\"Type\":0},\"Id\":50,\"Valeur\":75},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":6,\"Nom\":\"intelligence\",\"Definition\":\"représente la capacité d\\u0027apprentissage et de raisonnement de votre personnage, participe aux dégâts magiques, au pistage, réussite des sorts ...\",\"Stats\":5,\"Type\":0},\"Id\":51,\"Valeur\":75}],\"Id\":30},\"Id\":49,\"Nom\":\"Puissance des sort\",\"Definition\":\"Représente le bonus au effet de vos sort\",\"Stats\":47,\"Type\":2},\"Id\":126,\"Valeur\":70},{\"StatUtile\":{\"StatCalculer\":null,\"Id\":86,\"Nom\":\"Dégâts bonus\",\"Definition\":\"Représente le bonus de dégâts ajoute à tous les types de dégâts\",\"Stats\":84,\"Type\":1},\"Id\":127,\"Valeur\":100}],\"Id\":68},\"Id\":99,\"Nom\":\"Dégâts d\\u0027esprit\",\"Definition\":\"Représente le bonus de dégâts ajouté à vos dégâts d\\u0027esprit\",\"Stats\":97,\"Type\":2},{\"StatCalculer\":null,\"Id\":590,\"Nom\":\"Dégâts critique\",\"Definition\":\"Représente le bonus de dégâts ajouté à vos dégâts critique\",\"Stats\":98,\"Type\":1}]";

            List<Stat> result = System.Text.Json.JsonSerializer.Deserialize<List<Stat>>(jsonString);


            int a = 0;
            int b = 0;
            int c = 0;
            int d = 0;
            List<Statistique.Stat> listNewStat = new List<Statistique.Stat>();
            foreach (Stat s in result)
            {
                Statistique.Stat newStat = new Statistique.Stat();
                newStat.Nom = s.Nom;
                newStat.Definition = s.Definition;
                newStat.Stats = s.Stats;
                newStat.StatUtils = new List<Statistique.StatUtil>();

                if (s.Type == Stat.Typestats.Base)
                {
                    newStat.Type = Statistique.Stat.Typestats.Base;
                }
                else
                {
                    newStat.Type = Statistique.Stat.Typestats.Secondaire;
                }
                if (s.StatCalculer != null)
                {
                    foreach (StatUtil su in s.StatCalculer.ListStatUtils)
                    {
                        Statistique.StatUtil newSu = new Statistique.StatUtil();
                        newSu.Valeur = su.Valeur;
                        newSu.StatUtile = listNewStat.Find(s => s.Nom == su.StatUtile.Nom);
                        newSu.ForStat = newStat;
                        newStat.StatUtils.Add(newSu);
                    }
                }
                listNewStat.Add(newStat);

            }
            foreach(Statistique.Stat s in listNewStat)
            {
                db.Stats.Add(s);
                db.SaveChanges();
            }


            List<Statistique.Stat> verif = db.Stats.ToList();

            */

            /*
            
            
            List<Race> result = System.Text.Json.JsonSerializer.Deserialize<List<Race>>(jsonString);
            List<Race> newRaceresult = new List<Race>();
            foreach (Race r in result)
            {
                Race newRace = new Race();
                newRace.Nom = r.Nom;
                newRace.Definition = r.Definition;
                newRace.Stat = new List<ValeurRaceStat>();
                newRace.StatDee = new List<DeeStat>();

                foreach (ValeurRaceStat vrs in r.Stat)
                {
                    ValeurRaceStat newvrs = new ValeurRaceStat();
                    newvrs.Valeur = vrs.Valeur;
                    newvrs.Stat = db.Stats.ToList().FirstOrDefault(s => s.Nom == vrs.Stat.Nom);
                    db.Entry(newvrs.Stat).State = EntityState.Modified;
                    newRace.Stat.Add(newvrs);
                }
                foreach (DeeStat dee in  r.StatDee)
                {
                    DeeStat newDee = new DeeStat();
                    newDee.NbDee = dee.NbDee;
                    newDee.TailleDee = dee.TailleDee;
                    newDee.Stat = db.Stats.ToList().FirstOrDefault(s => s.Nom == dee.Stat.Nom);
                    db.Entry(newDee.Stat).State = EntityState.Modified;
                    newRace.StatDee.Add(newDee);
                }


                db.Races.Add(newRace);
                db.SaveChanges();
            }


            List<Race> verif = db.Races.ToList();*/

        }









        public void Dispose()
        {
            db.Dispose();
        }
    }
}
