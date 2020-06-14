using JDR.Model.Statistique;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDR.ViewModel
{
    public class NewStat
    {
        public int Id { get; set; }
        public String Nom { get; set; }
        public String Definition { get; set; }
        public Stat.Typestats Type { get; set; }
        public Stat.stats Stats { get; set; }
        public List<NewStatUtil> StatUtils { get; set; }

        public NewStat()
        {
        }

        public NewStat(Stat s)
        {
            this.Id = s.Id;
            this.Nom = s.Nom;
            this.Definition = s.Definition;
            this.Type = s.Type;
            this.Stats = s.Stats;
            StatUtils = new List<NewStatUtil>();
            if (s.StatUtils != null) {
                foreach (StatUtil su in s.StatUtils)
                {
                    this.StatUtils.Add(new NewStatUtil(su));
                }
            }
        }

    }
}
