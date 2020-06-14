using JDR.Model.Statistique.ValeurStat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDR.ViewModel
{
    public class NewValeurStat
    {
        public int StatId { get; set; }
        public int Id { get; set; }
        public String NomStat { get; set; }
        public int Valeur { get; set; }

        public NewValeurStat()
        {
        }

        public NewValeurStat(ValeurRaceStat valeurRace)
        {
            this.StatId = valeurRace.Stat.Id;
            this.Id = valeurRace.RaceId;
            this.NomStat = valeurRace.Stat.Nom;
            this.Valeur = valeurRace.Valeur;
        }

        public NewValeurStat(ValeurClasseStat valeurClasse)
        {
            this.StatId = valeurClasse.Stat.Id;
            this.Id = valeurClasse.ClasseId;
            this.NomStat = valeurClasse.Stat.Nom;
            this.Valeur = valeurClasse.Valeur;
        }


        public NewValeurStat(ValeurSousRaceStat valeurSousRace)
        {
            this.StatId = valeurSousRace.Stat.Id;
            this.Id = valeurSousRace.SousRaceId;
            this.NomStat = valeurSousRace.Stat.Nom;
            this.Valeur = valeurSousRace.Valeur;
        }
        public NewValeurStat(ValeurPersoStat valeurPerso)
        {
            this.StatId = valeurPerso.Stat.Id;
            this.Id = valeurPerso.PersoId;
            this.NomStat = valeurPerso.Stat.Nom;
            this.Valeur = valeurPerso.Valeur;
        }
        public NewValeurStat(ValeurBuffStat ValeurBuff)
        {
            this.StatId = ValeurBuff.Stat.Id;
            this.Id = ValeurBuff.PersoId;
            this.NomStat = ValeurBuff.Stat.Nom;
            this.Valeur = ValeurBuff.Valeur;
        }
    }
}
