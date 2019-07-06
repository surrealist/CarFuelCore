﻿// <auto-generated />
using System;
using CarFuel.Services.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarFuel.Services.Migrations
{
    [DbContext(typeof(AppDb))]
    [Migration("20190622065756_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CarFuel.Models.Car", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Color");

                    b.Property<bool>("IsStarted");

                    b.Property<string>("Make");

                    b.HasKey("Id");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("CarFuel.Models.FillUp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid?>("CarId");

                    b.Property<bool>("IsFull");

                    b.Property<double>("Liters");

                    b.Property<int?>("NextFillUpId");

                    b.Property<int>("Odometer");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("NextFillUpId");

                    b.ToTable("FillUp");
                });

            modelBuilder.Entity("CarFuel.Models.FillUp", b =>
                {
                    b.HasOne("CarFuel.Models.Car")
                        .WithMany("FillUps")
                        .HasForeignKey("CarId");

                    b.HasOne("CarFuel.Models.FillUp", "NextFillUp")
                        .WithMany()
                        .HasForeignKey("NextFillUpId");
                });
#pragma warning restore 612, 618
        }
    }
}
