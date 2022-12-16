﻿// <auto-generated />
using System;
using FlyingDutchmanAirlines.RepositoryLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FlyingDutchmanAirlines.Migrations
{
    [DbContext(typeof(FlyingDutchmanAirlinesContext))]
    [Migration("20221216101335_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.1");

            modelBuilder.Entity("FlyingDutchmanAirlines.RepositoryLayer.Models.Airport", b =>
                {
                    b.Property<int>("AirportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<int>("IATA")
                        .HasColumnType("INTEGER");

                    b.HasKey("AirportId");

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("FlyingDutchmanAirlines.RepositoryLayer.Models.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("FlightNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FlightNumberId")
                        .HasColumnType("INTEGER");

                    b.HasKey("BookingId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("FlightNumber");

                    b.ToTable("Bokings");
                });

            modelBuilder.Entity("FlyingDutchmanAirlines.RepositoryLayer.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("FlyingDutchmanAirlines.RepositoryLayer.Models.Flight", b =>
                {
                    b.Property<int>("FlightNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AirportDestinationAirportId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AirportOriginAirportId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Destination")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Origin")
                        .HasColumnType("INTEGER");

                    b.HasKey("FlightNumber");

                    b.HasIndex("AirportDestinationAirportId");

                    b.HasIndex("AirportOriginAirportId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("FlyingDutchmanAirlines.RepositoryLayer.Models.Booking", b =>
                {
                    b.HasOne("FlyingDutchmanAirlines.RepositoryLayer.Models.Customer", "Customer")
                        .WithMany("Booking")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlyingDutchmanAirlines.RepositoryLayer.Models.Flight", "Flight")
                        .WithMany()
                        .HasForeignKey("FlightNumber");

                    b.Navigation("Customer");

                    b.Navigation("Flight");
                });

            modelBuilder.Entity("FlyingDutchmanAirlines.RepositoryLayer.Models.Flight", b =>
                {
                    b.HasOne("FlyingDutchmanAirlines.RepositoryLayer.Models.Airport", "AirportDestination")
                        .WithMany()
                        .HasForeignKey("AirportDestinationAirportId");

                    b.HasOne("FlyingDutchmanAirlines.RepositoryLayer.Models.Airport", "AirportOrigin")
                        .WithMany()
                        .HasForeignKey("AirportOriginAirportId");

                    b.Navigation("AirportDestination");

                    b.Navigation("AirportOrigin");
                });

            modelBuilder.Entity("FlyingDutchmanAirlines.RepositoryLayer.Models.Customer", b =>
                {
                    b.Navigation("Booking");
                });
#pragma warning restore 612, 618
        }
    }
}
