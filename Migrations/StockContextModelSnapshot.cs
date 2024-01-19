﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WarehouseSystem.Data;

#nullable disable

namespace WarehouseSystem.Migrations
{
    [DbContext(typeof(StockContext))]
    partial class StockContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("WarehouseSystem.Models.Check", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Actual")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CashierID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Time")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CashierID");

                    b.ToTable("Check", (string)null);
                });

            modelBuilder.Entity("WarehouseSystem.Models.CheckEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CheckId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CheckId");

                    b.HasIndex("ProductID");

                    b.ToTable("CheckEntry", (string)null);
                });

            modelBuilder.Entity("WarehouseSystem.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("Actual")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PositionID")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PositionID");

                    b.ToTable("Employee", (string)null);
                });

            modelBuilder.Entity("WarehouseSystem.Models.Import", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("InvoiceID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceID");

                    b.HasIndex("ProductID");

                    b.ToTable("Import", (string)null);
                });

            modelBuilder.Entity("WarehouseSystem.Models.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Actual")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ResponsibleID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Time")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ResponsibleID");

                    b.ToTable("Invoice", (string)null);
                });

            modelBuilder.Entity("WarehouseSystem.Models.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Position", (string)null);
                });

            modelBuilder.Entity("WarehouseSystem.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Actual")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("WarehouseSystem.Models.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Stock", (string)null);
                });

            modelBuilder.Entity("WarehouseSystem.Models.Check", b =>
                {
                    b.HasOne("WarehouseSystem.Models.Employee", "Cashier")
                        .WithMany()
                        .HasForeignKey("CashierID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cashier");
                });

            modelBuilder.Entity("WarehouseSystem.Models.CheckEntry", b =>
                {
                    b.HasOne("WarehouseSystem.Models.Check", null)
                        .WithMany("CheckEntries")
                        .HasForeignKey("CheckId");

                    b.HasOne("WarehouseSystem.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WarehouseSystem.Models.Employee", b =>
                {
                    b.HasOne("WarehouseSystem.Models.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Position");
                });

            modelBuilder.Entity("WarehouseSystem.Models.Import", b =>
                {
                    b.HasOne("WarehouseSystem.Models.Invoice", null)
                        .WithMany("Imports")
                        .HasForeignKey("InvoiceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WarehouseSystem.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WarehouseSystem.Models.Invoice", b =>
                {
                    b.HasOne("WarehouseSystem.Models.Employee", "Responsible")
                        .WithMany()
                        .HasForeignKey("ResponsibleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Responsible");
                });

            modelBuilder.Entity("WarehouseSystem.Models.Stock", b =>
                {
                    b.HasOne("WarehouseSystem.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WarehouseSystem.Models.Check", b =>
                {
                    b.Navigation("CheckEntries");
                });

            modelBuilder.Entity("WarehouseSystem.Models.Invoice", b =>
                {
                    b.Navigation("Imports");
                });
#pragma warning restore 612, 618
        }
    }
}
