﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParkingAPI.Context;

namespace ParkingAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231114023212_create_new_column_in_table_parking")]
    partial class create_new_column_in_table_parking
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("ParkingAPI.Entities.AddressEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("city");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("country");

                    b.Property<DateTime?>("CreateAT")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("start_date");

                    b.Property<int>("Number")
                        .HasColumnType("int")
                        .HasColumnName("number");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("state");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("street");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("modify_date");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("ParkingAPI.Entities.CompanyEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AddressEntityId")
                        .HasColumnType("char(36)")
                        .HasColumnName("address_Id");

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("varchar(14)")
                        .HasColumnName("cnpj");

                    b.Property<DateTime?>("CreateAT")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("start_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.Property<int>("NumberCars")
                        .HasColumnType("int")
                        .HasColumnName("number_cars");

                    b.Property<int>("NumberMotorcycies")
                        .HasColumnType("int")
                        .HasColumnName("number_motorcycies");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("phone");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("modify_date");

                    b.HasKey("Id");

                    b.HasIndex("AddressEntityId");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("ParkingAPI.Entities.ParkingEntiy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("char(36)")
                        .HasColumnName("company_id");

                    b.Property<DateTime?>("CreateAT")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("start_date");

                    b.Property<bool>("IsParked")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_parked");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("modify_date");

                    b.Property<Guid>("VehicleId")
                        .HasColumnType("char(36)")
                        .HasColumnName("vehicle_id");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Parking");
                });

            modelBuilder.Entity("ParkingAPI.Entities.VehicleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("brand");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("color");

                    b.Property<DateTime?>("CreateAT")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("start_date");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("model");

                    b.Property<string>("Plate")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("plate");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("modify_date");

                    b.Property<int>("typeVehicle")
                        .HasColumnType("int")
                        .HasColumnName("type_vehicle");

                    b.HasKey("Id");

                    b.ToTable("Vehicle");
                });

            modelBuilder.Entity("ParkingAPI.Entities.CompanyEntity", b =>
                {
                    b.HasOne("ParkingAPI.Entities.AddressEntity", "Address")
                        .WithMany()
                        .HasForeignKey("AddressEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("ParkingAPI.Entities.ParkingEntiy", b =>
                {
                    b.HasOne("ParkingAPI.Entities.CompanyEntity", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ParkingAPI.Entities.VehicleEntity", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Vehicle");
                });
#pragma warning restore 612, 618
        }
    }
}