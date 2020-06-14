using JDR.Model.Action;
using JDR.Model.Outil;
using JDR.Model.Statistique;
using JDR.Model.Statistique.ValeurStat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDR.Model.Personnage
{
    public class Perso
    {

        private Dal dal = new Dal();
        public User User { get; set; }
        public int Id { get; set; }
        public String Nom { get; set; }
        public String Prenom { get; set; }
        public String Definition { get; set; }
        public Boolean Vivant { get; set; }
        public virtual Race Race { get; set; }
        public virtual SousRace SousRace { get; set; }
        public virtual Classe Classe { get; set; }
        public int Lvl { get; set; }
        public virtual List<ValeurPersoStat> Stats { get; set; }
        public int posX { get; set; }
        public int posY { get; set; }
        public virtual List<ValeurBuffStat> Buff { get; set; }
        public virtual List<EffetAppliquer> ListEffets { get; set; }



        public int GetStat(Stat stat)
        {
            return Util.GetValeur(GetValueStat(stat));
        }
        public int GetStatTest(Stat stat)
        {
            return Util.GetValeurOn100(GetValueStat(stat));
        }
        

        /// <summary>
        /// retourne la valeur d'une stat pour le personnage
        /// </summary>
        /// <param name="stat"></param>
        /// <returns></returns>
        private int GetValueStat(Stat stat)
        {
            int retour = 0;


            ValeurRaceStat valeurStatRace = Race.Stat.FirstOrDefault(s => s.Stat == stat);
            ValeurSousRaceStat valeurStatSousRace = SousRace.Stat.FirstOrDefault(s => s.Stat == stat);
            ValeurClasseStat valeurStatClasse = Classe.Stat.FirstOrDefault(s => s.Stat == stat);
            ValeurPersoStat valeurStatPerso = Stats.FirstOrDefault(s => s.Stat == stat);
            ValeurBuffStat valeurStatBuf = Buff.FirstOrDefault(s => s.Stat == stat);


            int statRace = 0;
            int statSousRace = 0;
            int statClasse = 0;
            int statPerso = 0;
            int statBuf = 0;


            if (valeurStatRace != null)
            {
                statRace = valeurStatRace.Valeur;
            }
            if (valeurStatSousRace != null)
            {
                statSousRace = valeurStatSousRace.Valeur;
            }
            if (valeurStatClasse != null)
            {
                statClasse = valeurStatClasse.Valeur;
            }
            if (valeurStatPerso != null)
            {
                statPerso = valeurStatPerso.Valeur;
            }
            if (valeurStatBuf != null)
            {
                statBuf = valeurStatBuf.Valeur;
            }






            int bonus = 0;
            if (stat.StatUtils != null)
            {
                foreach (StatUtil util in stat.StatUtils)
                {
                    int tmp = (GetValueStat(util.StatUtile) * util.Valeur) / 100;
                    bonus += tmp;
                }
            }
            if (stat.Type == Stat.Typestats.Base)
            {
                return ((statRace + statSousRace + statPerso) * (100 + statClasse * Lvl)) / 100 + statBuf + bonus;
            }
            else
            {
                return statRace + statSousRace + statPerso + (statClasse * Lvl) + statBuf + bonus;
            }
        }











        public void InitNewPerso()
        {
            Nom = "Nom";
            Prenom = "Prenom";
            Definition = "Description";
            Vivant = true;
            Stats = new List<ValeurPersoStat>();
            Buff = new List<ValeurBuffStat>();
            ListEffets = new List<EffetAppliquer>();
            Race = dal.GetRaceById(2);
            Classe = dal.GetClasseById(1);
            SousRace = dal.GetSousRaceById(1);
    }

    }
}
