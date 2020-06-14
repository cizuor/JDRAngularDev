using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDR.Model.Outil
{
    public class Roll
    {
        private static Random rand = new Random();


        public static int JetDée(int taille, int nb)
        {
            int retour = 0;
            int i = 0;
            for (i = 0; i < nb; i++)
            {
                retour += rand.Next(taille) + 1;
            }
            return retour;
        }


        public static Boolean JetToucher(int toucher, int esquive)
        {
            bool retour = false;
            double tmp = Convert.ToDouble(toucher);
            tmp = (tmp + 50) * 50;
            tmp = tmp / esquive;
            tmp = Math.Pow(2, (tmp / 50));
            tmp = 100 / tmp;
            int chanceToucher = (int)(100 - tmp);
            int resultat = JetDée(100, 1);
            if (chanceToucher > resultat)
            {
                retour = true;
            }
            else
            {
                retour = false;
            }
            return retour;
        }

        public static int minmax(int min, int max)
        {
            int taille = max - min;
            taille++;

            int retour = JetDée(taille, 1);

            retour--;
            retour += min;
            return retour;
        }


        public static void Jet100(int objectif, out int resultat, out Boolean réussite)
        {
            resultat = JetDée(100, 1);
            if (objectif > resultat)
            {
                réussite = true;
            }
            else
            {
                réussite = false;
            }

        }
    }
}
