﻿// <auto-generated />
using System;
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
    [Migration("20221221094216_InitialMigration")]
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

                    b.Property<string>("AccountName")
                        .HasColumnType("text")
                        .HasColumnName("accountname");

                    b.Property<string>("AccountNumber")
                        .HasColumnType("text")
                        .HasColumnName("accountnumber");

                    b.Property<string>("BankName")
                        .HasColumnType("text")
                        .HasColumnName("bankname");

                    b.Property<string>("Branch")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("branch");

                    b.Property<string>("City")
                        .HasColumnType("text")
                        .HasColumnName("city");

                    b.Property<string>("ContactNumber")
                        .HasColumnType("text")
                        .HasColumnName("contactnumber");

                    b.Property<DateTime>("DateHired")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("datehired");

                    b.Property<string>("Department")
                        .HasColumnType("text")
                        .HasColumnName("division");

                    b.Property<string>("Designation")
                        .HasColumnType("text")
                        .HasColumnName("designation");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("PAGIBIG")
                        .HasColumnType("text")
                        .HasColumnName("pagibig");

                    b.Property<string>("PHIC")
                        .HasColumnType("text")
                        .HasColumnName("phic");

                    b.Property<string>("Position")
                        .HasColumnType("text")
                        .HasColumnName("position");

                    b.Property<string>("Province")
                        .HasColumnType("text")
                        .HasColumnName("province");

                    b.Property<string>("Region")
                        .HasColumnType("text")
                        .HasColumnName("region");

                    b.Property<string>("SSS")
                        .HasColumnType("text")
                        .HasColumnName("sss");

                    b.Property<string>("TIN")
                        .HasColumnType("text")
                        .HasColumnName("tin");

                    b.Property<string>("Title")
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("userid");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("employees", (string)null);
                });

            modelBuilder.Entity("Employee.API.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("city");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("country");

                    b.Property<string>("Designation")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("designation");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("firstname");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("lastname");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("mobilenumber");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string[]>("Role")
                        .IsRequired()
                        .HasColumnType("text[]")
                        .HasColumnName("role");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("state");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
