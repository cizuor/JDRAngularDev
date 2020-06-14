using JDR.Model.Statistique.ValeurStat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDR.Model.Personnage
{
    public class Classe
    {

        public int Id { get; set; }
        public String Nom { get; set; }
        public String Definition { get; set; }
        public virtual List<ValeurClasseStat> Stat { get; set; }
        public List<Perso> ListPerso { get; set; }


        private Dal dal = new Dal();


        public void InitNewClasse()
        {
            if (Stat == null || Stat.Count == 0)
            {
                Nom = "Nom";
                Definition = "Definition";
                Stat = new List<ValeurClasseStat>();

                Stat.Add(new ValeurClasseStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.CC), Valeur = 2 });
                Stat.Add(new ValeurClasseStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.CT), Valeur = 2 });
                Stat.Add(new ValeurClasseStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.Ag), Valeur = 2 });
                Stat.Add(new ValeurClasseStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.F), Valeur = 2 });
                Stat.Add(new ValeurClasseStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.E), Valeur = 2 });
                Stat.Add(new ValeurClasseStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.Int), Valeur = 2 });
                Stat.Add(new ValeurClasseStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.Fm), Valeur = 2 });
                Stat.Add(new ValeurClasseStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.P), Valeur = 2 });
                Stat.Add(new ValeurClasseStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.Soc), Valeur = 2 });
                Stat.Add(new ValeurClasseStat { Stat = dal.GetStatByStat(Statistique.Stat.stats.PV), Valeur = 5 });
            }

        }
    }
}
