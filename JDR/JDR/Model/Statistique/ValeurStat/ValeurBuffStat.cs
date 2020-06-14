using JDR.Model.Personnage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDR.Model.Statistique.ValeurStat
{
    public class ValeurBuffStat
    {
        public int StatId { get; set; }
        public int PersoId { get; set; }
        public virtual Stat Stat { get; set; }
        public int Valeur { get; set; }
        public Perso Perso { get; set; }
    }
}
