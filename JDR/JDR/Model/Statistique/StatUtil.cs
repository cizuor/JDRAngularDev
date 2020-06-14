using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDR.Model.Statistique
{
    public class StatUtil
    {


        public int Id { get; set; }
        public virtual Stat ForStat { get; set; }
        public virtual Stat StatUtile { get; set; }
        public int Valeur { get; set; }



    }
}
