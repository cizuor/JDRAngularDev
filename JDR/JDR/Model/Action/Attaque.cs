using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static JDR.Model.Outil.Util;

namespace JDR.Model.Action
{
    public class Attaque
    {
        public int Id { get; set; }
        public List<Effet> Effets { get; set; }
        public int Porter { get; set; }
        public Boolean Basic { get; set; }
        public Boolean Contact { get; set; }
        public EnumCible Cible { get; set; }
        public Boolean Passif { get; set; }

    }
}
