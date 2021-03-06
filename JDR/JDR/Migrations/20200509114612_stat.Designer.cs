﻿// <auto-generated />
using System;
using JDR.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JDR.Migrations
{
    [DbContext(typeof(BddContext))]
    [Migration("20200509114612_stat")]
    partial class stat
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("JDR.Model.Statistique.Stat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Definition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Stats")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Stat");
                });

            modelBuilder.Entity("JDR.Model.Statistique.StatUtil", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ForStatId")
                        .HasColumnType("int");

                    b.Property<int?>("StatUtileId")
                        .HasColumnType("int");

                    b.Property<int>("Valeur")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ForStatId");

                    b.HasIndex("StatUtileId");

                    b.ToTable("StatUtil");
                });

            modelBuilder.Entity("JDR.Model.Statistique.StatUtil", b =>
                {
                    b.HasOne("JDR.Model.Statistique.Stat", "ForStat")
                        .WithMany("StatUtils")
                        .HasForeignKey("ForStatId");

                    b.HasOne("JDR.Model.Statistique.Stat", "StatUtile")
                        .WithMany("UsedFor")
                        .HasForeignKey("StatUtileId");
                });
#pragma warning restore 612, 618
        }
    }
}
