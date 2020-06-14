using JDR.Model.Statistique;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDR.ViewModel
{
    public class NewStatUtil
    {
        public int Id { get; set; }
        public int StatUtile { get; set; }
        public int Valeur { get; set; }

        public NewStatUtil()
        {
        }


        public NewStatUtil(StatUtil su)
        {
            this.Id = su.Id;
            this.StatUtile = su.StatUtile.Id;
            this.Valeur = su.Valeur;
        }
    }
}
