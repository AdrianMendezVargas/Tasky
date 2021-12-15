using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Tasky.Models;

#nullable disable

namespace Tasky.Data
{
    public partial class TaskyDbContext : DbContext
    {
        public TaskyDbContext()
        {
        }

        public TaskyDbContext(DbContextOptions<TaskyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Cuota> Cuotas { get; set; }
        public virtual DbSet<Pago> Pagos { get; set; }
        public virtual DbSet<Prestamo> Prestamos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Cuota>(entity =>
            {
                entity.Property(e => e.Monto).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Prestamo)
                    .WithMany(p => p.Cuotas)
                    .HasForeignKey(d => d.PrestamoId)
                    .HasConstraintName("FK_Prestamo_Cuotas");
            });

            modelBuilder.Entity<Pago>(entity =>
            {
                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Monto).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Prestamo)
                    .WithMany(p => p.Pagos)
                    .HasForeignKey(d => d.PrestamoId)
                    .HasConstraintName("FK_Prestamo_Pagos");
            });

            modelBuilder.Entity<Prestamo>(entity =>
            {
                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Monto).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Prestamos)
                    .HasForeignKey(d => d.ClienteId)
                    .HasConstraintName("FK_Cliente_Prestamos");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
