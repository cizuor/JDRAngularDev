using JDR.Model.Personnage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDR.Model.Statistique.ValeurStat
{
    public class ValeurClasseStat
    {
        public int StatId { get; set; }
        public int ClasseId { get; set; }
        public virtual Stat Stat { get; set; }
        public int Valeur { get; set; }
        public Classe Classe { get; set; }
    }
}
