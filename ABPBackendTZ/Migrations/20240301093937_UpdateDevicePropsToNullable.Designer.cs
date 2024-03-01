﻿// <auto-generated />
using System;
using ABPBackendTZ;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ABPBackendTZ.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240301093937_UpdateDevicePropsToNullable")]
    partial class UpdateDevicePropsToNullable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ABPBackendTZ.Models.ButtonColor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("HEX")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ButtonColors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            HEX = "#FF0000"
                        },
                        new
                        {
                            Id = 2,
                            HEX = "#00FF00"
                        },
                        new
                        {
                            Id = 3,
                            HEX = "#0000FF"
                        });
                });

            modelBuilder.Entity("ABPBackendTZ.Models.Device", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("ButtonColorId")
                        .HasColumnType("int");

                    b.Property<int?>("PriceToShowId")
                        .HasColumnType("int");

                    b.HasKey("Token");

                    b.HasIndex("ButtonColorId");

                    b.HasIndex("PriceToShowId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("ABPBackendTZ.Models.PriceToShow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("Percentage")
                        .HasColumnType("real");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("PricesToShow");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Percentage = 0.75f,
                            Value = 10m
                        },
                        new
                        {
                            Id = 2,
                            Percentage = 0.1f,
                            Value = 20m
                        },
                        new
                        {
                            Id = 3,
                            Percentage = 0.05f,
                            Value = 50m
                        },
                        new
                        {
                            Id = 4,
                            Percentage = 0.1f,
                            Value = 5m
                        });
                });

            modelBuilder.Entity("ABPBackendTZ.Models.Device", b =>
                {
                    b.HasOne("ABPBackendTZ.Models.ButtonColor", "ButtonColor")
                        .WithMany()
                        .HasForeignKey("ButtonColorId");

                    b.HasOne("ABPBackendTZ.Models.PriceToShow", "PriceToShow")
                        .WithMany()
                        .HasForeignKey("PriceToShowId");

                    b.Navigation("ButtonColor");

                    b.Navigation("PriceToShow");
                });
#pragma warning restore 612, 618
        }
    }
}
