using JDR.Model.Personnage;
using JDR.Model.Statistique.ValeurStat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDR.ViewModel
{
    public class NewRace
    {
        public int Id { get; set; }
        public String Nom { get; set; }
        public String Definition { get; set; }
        public List<NewValeurStat> Stat { get; set; }
        public List<NewDeeStat> StatDee { get; set; }

        public NewRace()
        {
        }

        public NewRace(Race race)
        {
            this.Id = race.Id;
            this.Nom = race.Nom;
            this.Definition = race.Definition;
            Stat = new List<NewValeurStat>();
            foreach (ValeurRaceStat vr in race.Stat)
            {
                Stat.Add(new NewValeurStat(vr));
            }
            StatDee = new List<NewDeeStat>();
            foreach (DeeStat ds in race.StatDee)
            {
                StatDee.Add(new NewDeeStat(ds));
            }
        }
    }
}
