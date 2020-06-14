using JDR.Model.Outil;
using JDR.Model.Personnage;
using JDR.Model.Statistique;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDR.Model.Action
{
    public class EffetAppliquer
    {

        public int Id { get; set; }

        public int IdEffet { get; set; }
        public virtual List<Effet> ListEffetBonus { get; set; }
        public int Penetration { get; set; }
        public int MinHit { get; set; }
        public int MaxHit { get; set; }
        public virtual Stat StatReduc { get; set; }
        public int CoefStatReduc { get; set; }
        public int ValeurBase { get; set; }
        public virtual Stat StatAffecter { get; set; }
        public int Drain { get; set; }
        public int TailleDee { get; set; }
        public int NbDee { get; set; }


        public Perso Lanceur { get; set; }
        public int Bonus { get; set; }
        public int Reduction { get; set; }
        public int TourRestant { get; set; }
        public Boolean EffetBonus { get; set; }
        public Boolean Actif { get; set; }
        public int BuffDrain { get; set; }
        public int BuffTotal { get; set; }

        public EffetAppliquer()
        {
        }

        public EffetAppliquer(Effet effet)
        {
            IdEffet = effet.Id;
            ListEffetBonus = effet.ListEffetBonus;
            Penetration = effet.Penetration;
            MinHit = effet.MinHit;
            MaxHit = effet.MaxHit;
            StatReduc = effet.StatReduc;
            CoefStatReduc = effet.CoefStatReduc;
            ValeurBase = effet.ValeurBase;
            StatAffecter = effet.StatAffecter;
            Drain = effet.Drain;
            TailleDee = effet.TailleDee;
            NbDee = effet.NbDee;

            TourRestant = Roll.minmax(effet.DureMin, effet.DureMax);
            Boolean isEffetBonus;
            int result;
            Roll.Jet100(effet.ChanceEffetBonus, out result, out isEffetBonus);
            EffetBonus = isEffetBonus;

        }

    }
}
