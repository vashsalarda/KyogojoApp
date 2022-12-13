﻿// <auto-generated />
using Employee.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Employee.API.Migrations
{
    [DbContext(typeof(EmployeeContext))]
    [Migration("20221212142545_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Employee.API.Entities.EmployeeModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("Designation")
                        .HasColumnType("text")
                        .HasColumnName("designation");

                    b.Property<string>("Division")
                        .HasColumnType("text")
                        .HasColumnName("division");

                    b.Property<int>("Level")
                        .HasColumnType("integer")
                        .HasColumnName("level");

                    b.Property<string>("Position")
                        .HasColumnType("text")
                        .HasColumnName("position");

                    b.Property<string>("Region")
                        .HasColumnType("text")
                        .HasColumnName("region");

                    b.Property<string>("Title")
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<string>("UserId")
                        .HasColumnType("text")
                        .HasColumnName("userid");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("employees", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
