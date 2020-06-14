using JDR.Model.Statistique.ValeurStat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDR.Model.Personnage
{
    public class Race
    {
        public int Id { get; set; }
        public String Nom { get; set; }
        public String Definition { get; set; }
        public virtual List<ValeurRaceStat> Stat { get; set; }
        public virtual List<DeeStat> StatDee { get; set; }
        public virtual List<SousRace> ListSousRace { get; set; }
        public List<Perso> ListPerso { get; set; }

        private Dal dal = new Dal();

        public void InitNewRace()
        {
            if (Stat == null || Stat.Count == 0)
            {
                Nom = "Nom";
                Definition = "Definition";
                Stat = new List<ValeurRaceStat>();
                StatDee = new List<DeeStat>();

                Stat.Add(new ValeurRaceStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.CC), Valeur = 15 });
                Stat.Add(new ValeurRaceStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.CT), Valeur = 15 });
                Stat.Add(new ValeurRaceStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.Ag), Valeur = 15 });
                Stat.Add(new ValeurRaceStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.F), Valeur = 15 });
                Stat.Add(new ValeurRaceStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.E), Valeur = 15 });
                Stat.Add(new ValeurRaceStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.Int), Valeur = 15 });
                Stat.Add(new ValeurRaceStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.Fm), Valeur = 15 });
                Stat.Add(new ValeurRaceStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.P), Valeur = 15 });
                Stat.Add(new ValeurRaceStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.Soc), Valeur = 15 });
                Stat.Add(new ValeurRaceStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.PV), Valeur = 70 });
                Stat.Add(new ValeurRaceStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.ChanceCrit), Valeur = 5 });
                Stat.Add(new ValeurRaceStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.Initiative), Valeur = 50 });
                Stat.Add(new ValeurRaceStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.Mouvement), Valeur = 4 });
                Stat.Add(new ValeurRaceStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.RatioXp), Valeur = 100 });
                Stat.Add(new ValeurRaceStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.NBMains), Valeur = 2 });
                Stat.Add(new ValeurRaceStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.NbMaxRiposte), Valeur = 1 });

                StatDee.Add(new DeeStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.CC), NbDee = 2, TailleDee = 6 });
                StatDee.Add(new DeeStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.CT), NbDee = 2, TailleDee = 6 });
                StatDee.Add(new DeeStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.Ag), NbDee = 2, TailleDee = 6 });
                StatDee.Add(new DeeStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.F), NbDee = 2, TailleDee = 6 });
                StatDee.Add(new DeeStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.E), NbDee = 2, TailleDee = 6 });
                StatDee.Add(new DeeStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.Int), NbDee = 2, TailleDee = 6 });
                StatDee.Add(new DeeStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.Fm), NbDee = 2, TailleDee = 6 });
                StatDee.Add(new DeeStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.P), NbDee = 2, TailleDee = 6 });
                StatDee.Add(new DeeStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.Soc), NbDee = 2, TailleDee = 6 });
                StatDee.Add(new DeeStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.PV), NbDee = 3, TailleDee = 12 });
                StatDee.Add(new DeeStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.PointDestin), NbDee = 1, TailleDee = 3 });


            }





        }

    }
}
