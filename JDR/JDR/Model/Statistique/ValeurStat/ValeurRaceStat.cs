using JDR.Model.Personnage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JDR.Model.Statistique.ValeurStat
{
    public class ValeurRaceStat
    {
        public int StatId { get; set; }
        public int RaceId { get; set; }
        public virtual Stat Stat { get; set; }
        public Race Race { get; set; }
        public int Valeur { get; set; }
    }
}
