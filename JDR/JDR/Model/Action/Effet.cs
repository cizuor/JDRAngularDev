using JDR.Model.Outil;
using JDR.Model.Personnage;
using JDR.Model.Statistique;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDR.Model.Action
{
    public class Effet
    {
        public int Id { get; set; }
        public String Nom { get; set; }
        public int ChanceEffetBonus { get; set; }
        public int TempsAvantEffetBonus { get; set; }
        public virtual List<Effet> ListEffetBonus { get; set; }
        public int Penetration { get; set; }
        public int MinHit { get; set; }
        public int MaxHit { get; set; }
        public int DureMin { get; set; }
        public int DureMax { get; set; }
        public virtual Stat StatUtil { get; set; }
        public int CoefStatUtil { get; set; }
        public virtual Stat StatReduc { get; set; }
        public int CoefStatReduc { get; set; }
        public int CumulMax { get; set; }
        public int ChanceResist { get; set; }
        public virtual Stat StatResist { get; set; }
        public int ValeurBase { get; set; }
        public virtual Stat StatAffecter { get; set; }
        public int Drain { get; set; }
        public int TailleDee { get; set; }
        public int NbDee { get; set; }

        public Effet()
        {
        }

        public void Application(Perso perso)
        {
            List<EffetAppliquer> cumul = (from effetActif in perso.ListEffets where effetActif.IdEffet == Id select effetActif).ToList();
            if (cumul.Count < CumulMax)
            {

                int valueResist = ChanceResist + perso.GetStat(StatResist);
                valueResist = Util.GetValeurOn100(valueResist);
                int result;
                Boolean resist;
                Roll.Jet100(valueResist, out result, out resist);
                if (!resist)
                {
                    EffetAppliquer effetAppliquer = new EffetAppliquer(this);
                    if (effetAppliquer.TourRestant > 0)
                    {
                        perso.ListEffets.Add(effetAppliquer);
                    }
                }
            }
        }



    }
}
