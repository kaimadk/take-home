﻿// <auto-generated />
using System;
using Energycom.Ingestion.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Energycom.Ingestion.Migrations
{
    [DbContext(typeof(ECOMDbContext))]
    [Migration("20250515090526_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Energycom.Ingestion.Entities.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_groups");

                    b.ToTable("groups", (string)null);
                });

            modelBuilder.Entity("Energycom.Ingestion.Entities.Meter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ConfigurationId")
                        .HasColumnType("integer")
                        .HasColumnName("configuration_id");

                    b.Property<int>("GroupId")
                        .HasColumnType("integer")
                        .HasColumnName("group_id");

                    b.Property<string>("MeterNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("meter_number");

                    b.HasKey("Id")
                        .HasName("pk_meters");

                    b.HasIndex("ConfigurationId")
                        .HasDatabaseName("ix_meters_configuration_id");

                    b.HasIndex("GroupId")
                        .HasDatabaseName("ix_meters_group_id");

                    b.ToTable("meters", (string)null);
                });

            modelBuilder.Entity("Energycom.Ingestion.Entities.MeterConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("BaseValue")
                        .HasColumnType("numeric")
                        .HasColumnName("base_value");

                    b.Property<bool>("CanDuplicateReadings")
                        .HasColumnType("boolean")
                        .HasColumnName("can_duplicate_readings");

                    b.Property<bool>("CanHaveUnparsedReadings")
                        .HasColumnType("boolean")
                        .HasColumnName("can_have_unparsed_readings");

                    b.Property<bool>("CanSkipReadings")
                        .HasColumnType("boolean")
                        .HasColumnName("can_skip_readings");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_meter_configuration");

                    b.ToTable("meter_configuration", (string)null);
                });

            modelBuilder.Entity("Energycom.Ingestion.Entities.Reading", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("MeterId")
                        .HasColumnType("integer")
                        .HasColumnName("meter_id");

                    b.Property<bool>("Parsed")
                        .HasColumnType("boolean")
                        .HasColumnName("parsed");

                    b.Property<string>("RawJson")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("raw_json");

                    b.Property<DateTime>("ReadingDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("reading_date");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("unit");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric")
                        .HasColumnName("value");

                    b.HasKey("Id")
                        .HasName("pk_readings");

                    b.HasIndex("MeterId")
                        .HasDatabaseName("ix_readings_meter_id");

                    b.ToTable("readings", (string)null);
                });

            modelBuilder.Entity("Energycom.Ingestion.Entities.Meter", b =>
                {
                    b.HasOne("Energycom.Ingestion.Entities.MeterConfiguration", "Configuration")
                        .WithMany()
                        .HasForeignKey("ConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_meters_meter_configuration_configuration_id");

                    b.HasOne("Energycom.Ingestion.Entities.Group", "Group")
                        .WithMany("Meters")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_meters_groups_group_id");

                    b.Navigation("Configuration");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Energycom.Ingestion.Entities.Reading", b =>
                {
                    b.HasOne("Energycom.Ingestion.Entities.Meter", "Meter")
                        .WithMany("Readings")
                        .HasForeignKey("MeterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_readings_meters_meter_id");

                    b.Navigation("Meter");
                });

            modelBuilder.Entity("Energycom.Ingestion.Entities.Group", b =>
                {
                    b.Navigation("Meters");
                });

            modelBuilder.Entity("Energycom.Ingestion.Entities.Meter", b =>
                {
                    b.Navigation("Readings");
                });
#pragma warning restore 612, 618
        }
    }
}
