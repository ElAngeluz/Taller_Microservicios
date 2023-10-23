﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using microscore.infrastructure.data.context;

#nullable disable

namespace microscore.infrastructure.Migrations
{
    [DbContext(typeof(MicrosContext))]
    partial class MicrosContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("microscore.domain.entities.Accounts.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(10,5)");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("State")
                        .HasColumnType("bit");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("microscore.domain.entities.Accounts.Movement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("MovementType")
                        .HasColumnType("int");

                    b.Property<bool>("State")
                        .HasColumnType("bit");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(10,5)");

                    b.Property<decimal>("ValueBalance")
                        .HasColumnType("decimal(10,5)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Movement");
                });

            modelBuilder.Entity("microscore.domain.entities.People.Client", b =>
                {
                    b.Property<Guid>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("State")
                        .HasColumnType("bit");

                    b.HasKey("ClientId");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("cliente");
                });

            modelBuilder.Entity("microscore.domain.entities.People.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Identification")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<bool>("State")
                        .HasColumnType("bit");

                    b.Property<long>("YearsOld")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("person");
                });

            modelBuilder.Entity("microscore.domain.entities.Accounts.Account", b =>
                {
                    b.HasOne("microscore.domain.entities.People.Client", "ClientNav")
                        .WithMany("AccountsNav")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClientNav");
                });

            modelBuilder.Entity("microscore.domain.entities.Accounts.Movement", b =>
                {
                    b.HasOne("microscore.domain.entities.Accounts.Account", "AccountNav")
                        .WithMany("MovementsNav")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountNav");
                });

            modelBuilder.Entity("microscore.domain.entities.People.Client", b =>
                {
                    b.HasOne("microscore.domain.entities.People.Person", "PersonNav")
                        .WithOne("ClientNav")
                        .HasForeignKey("microscore.domain.entities.People.Client", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PersonNav");
                });

            modelBuilder.Entity("microscore.domain.entities.Accounts.Account", b =>
                {
                    b.Navigation("MovementsNav");
                });

            modelBuilder.Entity("microscore.domain.entities.People.Client", b =>
                {
                    b.Navigation("AccountsNav");
                });

            modelBuilder.Entity("microscore.domain.entities.People.Person", b =>
                {
                    b.Navigation("ClientNav");
                });
#pragma warning restore 612, 618
        }
    }
}
