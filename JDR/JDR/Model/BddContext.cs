using JDR.Model.Personnage;
using JDR.Model.Statistique;
using JDR.Model.Statistique.ValeurStat;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDR.Model
{
    public class BddContext : DbContext
    {

        public DbSet<Stat> Stats { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Classe> Classes { get; set; }
        public DbSet<SousRace> SousRaces { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Perso> Persos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            optionsBuilder
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=JDR;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stat>().ToTable("Stat");
            modelBuilder.Entity<StatUtil>().ToTable("StatUtil");
            modelBuilder.Entity<Stat>()
                .HasMany(s => s.StatUtils)
                .WithOne(su => su.ForStat);
            modelBuilder.Entity<StatUtil>()
                .HasOne(su => su.StatUtile)
                .WithMany(s => s.UsedFor);
            modelBuilder.Entity<Race>().ToTable("Race");
            modelBuilder.Entity<ValeurRaceStat>().ToTable("ValeurRaceStat");
            modelBuilder.Entity<ValeurRaceStat>().HasKey(vr => new { vr.StatId, vr.RaceId });
            modelBuilder.Entity<ValeurRaceStat>()
               .HasOne(vr => vr.Race)
               .WithMany(r => r.Stat)
               .HasForeignKey(vr => vr.RaceId);
            modelBuilder.Entity<ValeurRaceStat>()
                .HasOne(vr => vr.Stat)
                .WithMany(s => s.RaceStats)
                .HasForeignKey(vr => vr.StatId);
            modelBuilder.Entity<SousRace>().ToTable("SousRace");
            modelBuilder.Entity<ValeurSousRaceStat>().ToTable("ValeurSousRaceStat");
            modelBuilder.Entity<ValeurSousRaceStat>().HasKey(vsr => new { vsr.StatId, vsr.SousRaceId });
            modelBuilder.Entity<ValeurSousRaceStat>()
                .HasOne(vsr => vsr.SousRace)
                .WithMany(sr => sr.Stat)
                .HasForeignKey(vsr => vsr.SousRaceId);
            modelBuilder.Entity<ValeurSousRaceStat>()
                .HasOne(vsr => vsr.Stat)
                .WithMany(s => s.SousRaceStats)
                .HasForeignKey(vsr => vsr.StatId);
            modelBuilder.Entity<Race>()
                .HasMany(r => r.ListSousRace)
                .WithOne(sr => sr.Race);
            modelBuilder.Entity<Classe>().ToTable("Classe");
            modelBuilder.Entity<ValeurClasseStat>().ToTable("ValeurClasseStat");
            modelBuilder.Entity<ValeurClasseStat>().HasKey(vc => new { vc.StatId,vc.ClasseId});
            modelBuilder.Entity<ValeurClasseStat>()
                .HasOne(vcs => vcs.Stat)
                .WithMany(s => s.ClasseStats);
            modelBuilder.Entity<ValeurClasseStat>()
                .HasOne(vcs => vcs.Classe)
                .WithMany(c => c.Stat);
            modelBuilder.Entity<DeeStat>().ToTable("DeeStat");
            modelBuilder.Entity<DeeStat>().HasKey(ds => new { ds.StatId,ds.RaceId});
            modelBuilder.Entity<DeeStat>()
                .HasOne(ds => ds.Stat)
                .WithMany(s => s.DeeStats);
            modelBuilder.Entity<DeeStat>()
                .HasOne(ds => ds.Race)
                .WithMany(s => s.StatDee);
            modelBuilder.Entity<Perso>().ToTable("Perso");
            modelBuilder.Entity<Perso>()
                .HasOne(p => p.Race)
                .WithMany(r => r.ListPerso);
            modelBuilder.Entity<Perso>()
                .HasOne(p => p.Classe)
                .WithMany(c => c.ListPerso);
            modelBuilder.Entity<Perso>()
                .HasOne(p => p.SousRace)
                .WithMany(sr => sr.ListPerso);
            modelBuilder.Entity<ValeurPersoStat>().ToTable("ValeurPersoStat");
            modelBuilder.Entity<ValeurPersoStat>().HasKey(vp => new { vp.StatId, vp.PersoId });
            modelBuilder.Entity<ValeurPersoStat>()
                .HasOne(vp => vp.Stat)
                .WithMany(s => s.PersoStats);
            modelBuilder.Entity<ValeurPersoStat>()
                .HasOne(vp => vp.Perso)
                .WithMany(p => p.Stats);
            modelBuilder.Entity<ValeurBuffStat>().ToTable("ValeurBuffStat");
            modelBuilder.Entity<ValeurBuffStat>().HasKey(vb => new { vb.StatId,vb.PersoId });
            modelBuilder.Entity<ValeurBuffStat>()
                .HasOne(vb => vb.Perso)
                .WithMany(p => p.Buff);
            modelBuilder.Entity<ValeurBuffStat>()
                .HasOne(vb => vb.Stat)
                .WithMany(s => s.BuffStats);
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Perso>()
                .HasOne(p => p.User)
                .WithMany(u => u.Persos);







        }

        public static BddContext Create()
        {
            return new BddContext();
        }
    }
}
