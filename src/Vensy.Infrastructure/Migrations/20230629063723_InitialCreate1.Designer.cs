﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Vensy.Infrastructure.Persistence;

#nullable disable

namespace Vensy.Infrastructure.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20230629063723_InitialCreate1")]
    partial class InitialCreate1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Vensy.Domain.Models.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CustomerEmail")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("customer_email");

                    b.Property<string>("CustomerPhone")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("customer_phone");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_time");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_time");

                    b.Property<int>("VenueId")
                        .HasColumnType("integer")
                        .HasColumnName("venue_id");

                    b.HasKey("Id")
                        .HasName("pk_appointments");

                    b.HasIndex("VenueId")
                        .HasDatabaseName("ix_appointments_venue_id");

                    b.ToTable("appointments", (string)null);
                });

            modelBuilder.Entity("Vensy.Domain.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_companies");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_companies_user_id");

                    b.ToTable("companies", (string)null);
                });

            modelBuilder.Entity("Vensy.Domain.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("firstname");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("lastname");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("UserId")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Vensy.Domain.Models.Venue", b =>
                {
                    b.Property<int>("VenueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("venue_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("VenueId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<int>("Capacity")
                        .HasColumnType("integer")
                        .HasColumnName("capacity");

                    b.Property<int>("CompanyId")
                        .HasColumnType("integer")
                        .HasColumnName("company_id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone");

                    b.HasKey("VenueId")
                        .HasName("pk_venues");

                    b.HasIndex("CompanyId")
                        .HasDatabaseName("ix_venues_company_id");

                    b.ToTable("venues", (string)null);
                });

            modelBuilder.Entity("Vensy.Domain.Models.Appointment", b =>
                {
                    b.HasOne("Vensy.Domain.Models.Venue", "Venue")
                        .WithMany("Appointments")
                        .HasForeignKey("VenueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_appointments_venues_venue_id");

                    b.Navigation("Venue");
                });

            modelBuilder.Entity("Vensy.Domain.Models.Company", b =>
                {
                    b.HasOne("Vensy.Domain.Models.User", "User")
                        .WithMany("Companies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_companies_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Vensy.Domain.Models.Venue", b =>
                {
                    b.HasOne("Vensy.Domain.Models.Company", "Company")
                        .WithMany("Venues")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_venues_companies_company_id");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Vensy.Domain.Models.Company", b =>
                {
                    b.Navigation("Venues");
                });

            modelBuilder.Entity("Vensy.Domain.Models.User", b =>
                {
                    b.Navigation("Companies");
                });

            modelBuilder.Entity("Vensy.Domain.Models.Venue", b =>
                {
                    b.Navigation("Appointments");
                });
#pragma warning restore 612, 618
        }
    }
}
