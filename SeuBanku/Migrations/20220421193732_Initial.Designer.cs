﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SeuBanku.Data;

#nullable disable

namespace SeuBanku.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220421193732_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SeuBanku.Entities.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<int>("AccountType")
                        .HasColumnType("int");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("bit");

                    b.Property<decimal>("Limit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("OpenedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PeriodToTakeMoneyInYears")
                        .HasColumnType("int");

                    b.Property<Guid?>("UserId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("SeuBanku.Entities.Extract", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("InAccountNumber")
                        .HasColumnType("int");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<int>("Operation")
                        .HasColumnType("int");

                    b.Property<DateTime>("OperationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OutAccountNumber")
                        .HasColumnType("int");

                    b.Property<decimal>("OutcomingBalance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Extracts");
                });

            modelBuilder.Entity("SeuBanku.Entities.Service", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("bit");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Services");

                    b.HasData(
                        new
                        {
                            Id = new Guid("4bbc4d9a-f327-4436-8cc1-719f872597c3"),
                            AccountNumber = 78188786,
                            Cost = 2266m,
                            CreatedDate = new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1096),
                            IsDisabled = false,
                            ServiceName = "Unitel"
                        },
                        new
                        {
                            Id = new Guid("894cffea-010e-4e6d-a791-ad6b982f5c97"),
                            AccountNumber = 27416049,
                            Cost = 5418m,
                            CreatedDate = new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1104),
                            IsDisabled = false,
                            ServiceName = "Movicel"
                        },
                        new
                        {
                            Id = new Guid("9131ea56-de7b-42be-a66d-fa556497324b"),
                            AccountNumber = 48991143,
                            Cost = 745m,
                            CreatedDate = new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1106),
                            IsDisabled = false,
                            ServiceName = "EDEL"
                        },
                        new
                        {
                            Id = new Guid("e6631f43-6266-43ea-b87b-058e0a249fa6"),
                            AccountNumber = 96693791,
                            Cost = 780m,
                            CreatedDate = new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1108),
                            IsDisabled = false,
                            ServiceName = "EPAL"
                        },
                        new
                        {
                            Id = new Guid("7ba25c43-2382-4f90-87d8-1cd584402db9"),
                            AccountNumber = 85025770,
                            Cost = 170m,
                            CreatedDate = new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1110),
                            IsDisabled = false,
                            ServiceName = "Africell"
                        },
                        new
                        {
                            Id = new Guid("448b387b-569f-49f0-86fb-3cc1b4ed326c"),
                            AccountNumber = 84171016,
                            Cost = 9437m,
                            CreatedDate = new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1124),
                            IsDisabled = false,
                            ServiceName = "Hospital"
                        },
                        new
                        {
                            Id = new Guid("9be3439e-d0b9-4e6d-9b2a-b3740238a1c0"),
                            AccountNumber = 6641069,
                            Cost = 8188m,
                            CreatedDate = new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1126),
                            IsDisabled = false,
                            ServiceName = "Governament"
                        },
                        new
                        {
                            Id = new Guid("4e1ba097-adc4-48db-85df-64c6ec223156"),
                            AccountNumber = 60544708,
                            Cost = 2597m,
                            CreatedDate = new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1128),
                            IsDisabled = false,
                            ServiceName = "Special Service"
                        },
                        new
                        {
                            Id = new Guid("e7b48021-e481-4ceb-9b1d-0b23417787b8"),
                            AccountNumber = 69527067,
                            Cost = 5362m,
                            CreatedDate = new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1130),
                            IsDisabled = false,
                            ServiceName = "School"
                        },
                        new
                        {
                            Id = new Guid("dfd70383-7f85-4456-8106-8ee4f6c8aa68"),
                            AccountNumber = 53977192,
                            Cost = 113m,
                            CreatedDate = new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1133),
                            IsDisabled = false,
                            ServiceName = "Other Service"
                        },
                        new
                        {
                            Id = new Guid("9de4c104-714c-4bc7-ac15-b14053a0d6ff"),
                            AccountNumber = 11145231,
                            Cost = 1954m,
                            CreatedDate = new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1135),
                            IsDisabled = false,
                            ServiceName = "Angola Telecom"
                        },
                        new
                        {
                            Id = new Guid("6c7c8175-c6d3-4384-b918-c4c709243d5c"),
                            AccountNumber = 28345451,
                            Cost = 1373m,
                            CreatedDate = new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1137),
                            IsDisabled = false,
                            ServiceName = "DSTV"
                        },
                        new
                        {
                            Id = new Guid("321bf96e-46e0-4e8c-8072-5b5a5ac6e69a"),
                            AccountNumber = 86003242,
                            Cost = 9621m,
                            CreatedDate = new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1138),
                            IsDisabled = false,
                            ServiceName = "TV Cabo"
                        },
                        new
                        {
                            Id = new Guid("2286804c-4edd-4d6f-92dc-59d2e4d64ca6"),
                            AccountNumber = 87249661,
                            Cost = 608m,
                            CreatedDate = new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1143),
                            IsDisabled = false,
                            ServiceName = "ZAP Satelite"
                        },
                        new
                        {
                            Id = new Guid("626ee7b6-022c-4be2-88f9-6602fb379297"),
                            AccountNumber = 98222814,
                            Cost = 1427m,
                            CreatedDate = new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1145),
                            IsDisabled = false,
                            ServiceName = "ZAP Fibra"
                        },
                        new
                        {
                            Id = new Guid("cadba806-c34a-4507-8665-46440940d73c"),
                            AccountNumber = 5509365,
                            Cost = 2718m,
                            CreatedDate = new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1147),
                            IsDisabled = false,
                            ServiceName = "NetOne"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
