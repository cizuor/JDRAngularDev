using JDR.Model.Personnage;
using JDR.Model.Statistique.ValeurStat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDR.ViewModel
{
    public class NewSousRace
    {
        public int Id { get; set; }
        public String Nom { get; set; }
        public String Definition { get; set; }
        public List<NewValeurStat> Stat  { get; set; }
        public String NomRace { get; set; }
        public int IdRace { get; set; }

        public NewSousRace()
        {
        }


        public NewSousRace(SousRace sousRace)
        {
            this.Id = sousRace.Id;
            this.Nom = sousRace.Nom;
            this.Definition = sousRace.Definition;
            if (sousRace.Race != null) {
                this.IdRace = sousRace.Race.Id;
                this.NomRace = sousRace.Race.Nom;
            }
            else
            {
                this.IdRace = 0;
                this.NomRace = "sans";
            }
            this.Stat = new List<NewValeurStat>();
            foreach(ValeurSousRaceStat vsr in sousRace.Stat)
            {
                this.Stat.Add(new NewValeurStat(vsr));
            }
        }

    }
}
