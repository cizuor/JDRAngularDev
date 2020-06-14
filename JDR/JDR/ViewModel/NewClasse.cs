using JDR.Model.Personnage;
using JDR.Model.Statistique.ValeurStat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDR.ViewModel
{
    public class NewClasse
    {
        public int Id { get; set; }
        public String Nom { get; set; }
        public String Definition { get; set; }
        public List<NewValeurStat> Stat { get; set; }

        public NewClasse()
        {
        }

        public NewClasse(Classe classe)
        {
            this.Id = classe.Id;
            this.Nom = classe.Nom;
            this.Definition = classe.Definition;
            Stat = new List<NewValeurStat>();
            foreach (ValeurClasseStat vc in classe.Stat)
            {
                Stat.Add(new NewValeurStat(vc));
            }
        }
    }
}
