using JDR.Model.Statistique.ValeurStat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDR.Model.Personnage
{
    public class SousRace
    {

        public int Id { get; set; }
        public String Nom { get; set; }
        public String Definition { get; set; }
        public virtual List<ValeurSousRaceStat> Stat { get; set; }
        public Race Race { get; set; }
        public List<Perso> ListPerso { get; set; }

        private Dal dal = new Dal();
        public void InitNewSousRace()
        {
            if (Stat == null || Stat.Count == 0)
            {
                Nom = "Nom";
                Definition = "Definition";
                Stat = new List<ValeurSousRaceStat>();

            }





        }



    }
}
