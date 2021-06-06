using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
#nullable disable

namespace GatewaysAPI
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Gateway> Gateways { get; set; }
        public virtual DbSet<Peripheral> Peripherals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server =(localdb)\\MSSQLLocalDB;Database=GateWaysDBSava;Trusted_Connection=True;MultipleActiveResultSets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Gateway>(entity =>
            {
                entity.HasKey(e => e.SerialNumber);

                entity.Property(e => e.Hrname).HasColumnName("HRName");

                entity.Property(e => e.Ipv4).HasColumnName("IPv4");
            });

            modelBuilder.Entity<Peripheral>(entity =>
            {
                entity.HasKey(e => e.Uid);

                entity.HasIndex(e => e.GatewaySerialNumber, "IX_Peripherals_GatewaySerialNumber");

                entity.Property(e => e.Uid)
                    .ValueGeneratedNever()
                    .HasColumnName("UID");

                entity.HasOne(d => d.GatewaySerialNumberNavigation)
                    .WithMany(p => p.Peripherals)
                    .HasForeignKey(d => d.GatewaySerialNumber);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<Gateway> Gateway { get; set; }
    }
}
