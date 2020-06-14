using JDR.Model.Personnage;
using JDR.Model.Statistique;
using JDR.Model.Statistique.ValeurStat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDR.ViewModel
{
    public class NewPerso
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public String Nom { get; set; }
        public String Prenom { get; set; }
        public String Definition { get; set; }
        public Boolean Vivant { get; set; }
        public int IdRace { get; set; }
        public String NomRace { get; set; }
        public int IdSousRace { get; set; }
        public String NomSousRace { get; set; }
        public int IdClasse { get; set; }
        public String NomClasse { get; set; }
        public int Lvl { get; set; }
        public List<NewValeurStat> Stats { get; set; }
        public int posX { get; set; }
        public int posY { get; set; }
        public virtual List<NewValeurStat> Buff { get; set; }

        public NewPerso()
        {
        }

        public NewPerso(Perso perso)
        {
            this.Id = perso.Id;
            if (perso.User != null)
            {
                this.IdUser = perso.User.Id;
            }
            this.Nom = perso.Nom;
            this.Prenom = perso.Prenom;
            this.Definition = perso.Definition;
            this.Vivant = perso.Vivant;
            this.IdRace = perso.Race.Id;
            this.NomRace = perso.Race.Nom;
            this.IdSousRace = perso.SousRace.Id;
            this.NomSousRace = perso.SousRace.Nom;
            this.IdClasse = perso.Classe.Id;
            this.NomClasse = perso.Classe.Nom;
            this.Lvl = perso.Lvl;
            this.Stats = new List<NewValeurStat>();
            foreach(ValeurPersoStat vp in perso.Stats)
            {
                this.Stats.Add(new NewValeurStat(vp));
            }
            this.posX = perso.posX;
            this.posY = perso.posY;
            this.Buff = new List<NewValeurStat>();
            foreach (ValeurBuffStat vb in perso.Buff)
            {
                this.Buff.Add(new NewValeurStat(vb));
            }


        }

    }
}
