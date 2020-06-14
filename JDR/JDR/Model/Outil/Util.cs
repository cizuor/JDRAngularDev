using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDR.Model.Outil
{
    public class Util
    {
        private static int reduc_puissance = 2;
        private static int reduc_valeur = 200;
        public static int GetValeur(float valeur)
        {
            return (int)valeur;
        }

        public static int GetValeurOn100(float valeur)
        {
            return (int)(100 - (100 / (Math.Pow(2, (valeur / 50)))));
        }

        public static int ReductionDegats(int subit, int resist)
        {
            double val = Math.Pow((double)reduc_puissance, ((double)resist / (double)reduc_valeur));
            val = 100 - (100 / val);
            return (int)(subit - ((subit * val) / 100));
        }

        public enum EnumCible
        {
            Enemie = 0,
            Allier = 1,
            AllierZone = 2,
            EnemieZone = 3,
            AllAllier = 4,
            AllEnemie = 5,
        }
    }
}
