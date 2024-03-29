﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using bbit_2_uzd.Models;

#nullable disable

namespace bbit_2_uzd.Migrations
{
    [DbContext(typeof(AppDatabaseConfig))]
    [Migration("20230421082724_extra_indexes")]
    partial class extra_indexes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true);

            modelBuilder.Entity("ApartmentTenant", b =>
                {
                    b.Property<Guid>("TenantApartmentsId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("TEXT");

                    b.HasKey("TenantApartmentsId", "TenantId");

                    b.HasIndex("TenantId");

                    b.ToTable("ApartmentTenant");
                });

            modelBuilder.Entity("bbit_2_uzd.Models.Apartment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Floor")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("FullArea")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("HouseId")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("LivingArea")
                        .HasColumnType("TEXT");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumberOfRooms")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumberOfTenants")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("HouseId");

                    b.HasIndex("Number", "HouseId")
                        .IsUnique();

                    b.ToTable("Apartments");

                    b.HasData(
                        new
                        {
                            Id = new Guid("4b82f99b-bf6c-4973-b171-7107546c12ca"),
                            Floor = 1,
                            FullArea = 70m,
                            HouseId = new Guid("c925843e-c424-4676-8690-360310f1261f"),
                            LivingArea = 64.5m,
                            Number = 1,
                            NumberOfRooms = 3,
                            NumberOfTenants = 4
                        },
                        new
                        {
                            Id = new Guid("21e718f3-f2e5-4f79-8dd6-b19e5501fd69"),
                            Floor = 1,
                            FullArea = 70m,
                            HouseId = new Guid("c925843e-c424-4676-8690-360310f1261f"),
                            LivingArea = 64.5m,
                            Number = 2,
                            NumberOfRooms = 1,
                            NumberOfTenants = 0
                        },
                        new
                        {
                            Id = new Guid("5372ea96-77d5-4125-85e3-3505f6167037"),
                            Floor = 2,
                            FullArea = 70m,
                            HouseId = new Guid("c925843e-c424-4676-8690-360310f1261f"),
                            LivingArea = 64.5m,
                            Number = 3,
                            NumberOfRooms = 1,
                            NumberOfTenants = 0
                        },
                        new
                        {
                            Id = new Guid("3271d406-97dc-4939-8876-265fc82f4beb"),
                            Floor = 2,
                            FullArea = 70m,
                            HouseId = new Guid("c925843e-c424-4676-8690-360310f1261f"),
                            LivingArea = 64.5m,
                            Number = 4,
                            NumberOfRooms = 1,
                            NumberOfTenants = 0
                        },
                        new
                        {
                            Id = new Guid("5d619b95-b25c-43e0-bec2-86808d199e6d"),
                            Floor = 3,
                            FullArea = 70m,
                            HouseId = new Guid("c925843e-c424-4676-8690-360310f1261f"),
                            LivingArea = 64.5m,
                            Number = 5,
                            NumberOfRooms = 1,
                            NumberOfTenants = 0
                        });
                });

            modelBuilder.Entity("bbit_2_uzd.Models.House", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Number", "Street", "City", "Country")
                        .IsUnique();

                    b.ToTable("Houses");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c925843e-c424-4676-8690-360310f1261f"),
                            City = "Test",
                            Country = "TestaValsts",
                            Number = 1,
                            PostalCode = "TV-0001",
                            Street = "Default"
                        },
                        new
                        {
                            Id = new Guid("b10cbf89-817d-427e-bb28-282f8d6ad9d9"),
                            City = "Jelgava",
                            Country = "Lavia",
                            Number = 40,
                            PostalCode = "LV-3001",
                            Street = "Liela"
                        });
                });

            modelBuilder.Entity("bbit_2_uzd.Models.Tenant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsOwner")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PersonalCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("Email IS NOT NULL");

                    b.HasIndex("PersonalCode")
                        .IsUnique();

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.ToTable("Tenants");

                    b.HasData(
                        new
                        {
                            Id = new Guid("cdbd0d5e-9bc9-4944-9a35-c33da875b119"),
                            DateOfBirth = new DateTime(1972, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "janis.tests@fake.com",
                            IsOwner = true,
                            Name = "Janis",
                            PersonalCode = "000000-000001",
                            PhoneNumber = "+371 00000001",
                            Surname = "Tests"
                        },
                        new
                        {
                            Id = new Guid("ebcaeab9-b5f4-48a0-81ab-59531a6cb11a"),
                            DateOfBirth = new DateTime(1974, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "Anna.Tests@fake.com",
                            IsOwner = false,
                            Name = "Anna",
                            PersonalCode = "000000-000002",
                            PhoneNumber = "+371 00000002",
                            Surname = "Tests"
                        },
                        new
                        {
                            Id = new Guid("83bf7350-2d6c-4f30-b206-3251b2b3d790"),
                            DateOfBirth = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "Maris.tests@fake.com",
                            IsOwner = false,
                            Name = "Māris",
                            PersonalCode = "000000-000003",
                            PhoneNumber = "+371 00000003",
                            Surname = "Tests"
                        },
                        new
                        {
                            Id = new Guid("edb4a419-b589-4bd7-986e-9dd94eac8c75"),
                            DateOfBirth = new DateTime(1998, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "",
                            IsOwner = false,
                            Name = "Zane",
                            PersonalCode = "000000-000004",
                            PhoneNumber = "+371 00000004",
                            Surname = "Tests"
                        });
                });

            modelBuilder.Entity("ApartmentTenant", b =>
                {
                    b.HasOne("bbit_2_uzd.Models.Apartment", null)
                        .WithMany()
                        .HasForeignKey("TenantApartmentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("bbit_2_uzd.Models.Tenant", null)
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("bbit_2_uzd.Models.Apartment", b =>
                {
                    b.HasOne("bbit_2_uzd.Models.House", "House")
                        .WithMany()
                        .HasForeignKey("HouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("House");
                });
#pragma warning restore 612, 618
        }
    }
}
