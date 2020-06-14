using JDR.Model.Statistique.ValeurStat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDR.ViewModel
{
    public class NewDeeStat
    {
        public int StatId { get; set; }
        public String NomStat { get; set; }
        public int RaceId { get; set; }
        public int NbDee { get; set; }
        public int TailleDee { get; set; }

        public NewDeeStat()
        {
        }
        public NewDeeStat(DeeStat ds)
        {
            this.StatId = ds.Stat.Id;
            this.NomStat = ds.Stat.Nom;
            this.RaceId = ds.RaceId;
            this.NbDee = ds.NbDee;
            this.TailleDee = ds.TailleDee;
        }
    }
}
