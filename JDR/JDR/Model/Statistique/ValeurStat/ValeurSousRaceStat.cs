using JDR.Model.Personnage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDR.Model.Statistique.ValeurStat
{
    public class ValeurSousRaceStat
    {
        public int StatId { get; set; }
        public int SousRaceId { get; set; }
        public virtual Stat Stat { get; set; }
        public SousRace SousRace { get; set; }
        public int Valeur { get; set; }
    }
}
