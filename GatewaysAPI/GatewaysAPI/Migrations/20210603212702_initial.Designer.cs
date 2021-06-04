﻿// <auto-generated />
using System;
using GatewaysAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GatewaysAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210603212702_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GatewaysAPI.Gateway", b =>
                {
                    b.Property<string>("SerialNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Hrname")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("HRName");

                    b.Property<string>("Ipv4")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("IPv4");

                    b.HasKey("SerialNumber");

                    b.ToTable("Gateway");
                });

            modelBuilder.Entity("GatewaysAPI.Peripheral", b =>
                {
                    b.Property<Guid>("Uid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("GatewaySerialNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Vendor")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Uid");

                    b.HasIndex(new[] { "GatewaySerialNumber" }, "IX_Peripherals_GatewaySerialNumber");

                    b.ToTable("Peripherals");
                });

            modelBuilder.Entity("GatewaysAPI.Peripheral", b =>
                {
                    b.HasOne("GatewaysAPI.Gateway", "GatewaySerialNumberNavigation")
                        .WithMany("Peripherals")
                        .HasForeignKey("GatewaySerialNumber");

                    b.Navigation("GatewaySerialNumberNavigation");
                });

            modelBuilder.Entity("GatewaysAPI.Gateway", b =>
                {
                    b.Navigation("Peripherals");
                });
#pragma warning restore 612, 618
        }
    }
}
