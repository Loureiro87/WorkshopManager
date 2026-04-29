using Microsoft.EntityFrameworkCore;
using WorkshopManager.Domain.Entities;

namespace WorkshopManager.Infrastructure.Data
{
    public class WorkshopDbContext : DbContext
    {
        public WorkshopDbContext(DbContextOptions<WorkshopDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Vehiculo> Vehiculos => Set<Vehiculo>();
        public DbSet<Cita> Citas => Set<Cita>();
        public DbSet<Pieza> Piezas => Set<Pieza>();
        public DbSet<CitaPieza> CitaPiezas => Set<CitaPieza>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Nombre)
                      .IsRequired()
                      .HasMaxLength(100);
            });

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.HasKey(v => v.Id);

                entity.Property(v => v.Marca)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(v => v.Modelo)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(v => v.Matricula)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.HasOne(v => v.Cliente)
                      .WithMany(c => c.Vehiculos)
                      .HasForeignKey(v => v.ClienteId);
            });
            modelBuilder.Entity<Cita>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.HasOne(c => c.Cliente)
                      .WithMany()
                      .HasForeignKey(c => c.ClienteId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Vehiculo)
                      .WithMany()
                      .HasForeignKey(c => c.VehiculoId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(c => c.Observaciones)
                      .HasMaxLength(1000);

                entity.Property(c => c.Estado)
                      .IsRequired();
            });

            modelBuilder.Entity<CitaPieza>(entity =>
            {
                entity.HasKey(cp => new { cp.CitaId, cp.PiezaId });

                entity.Property(cp => cp.PrecioUnitario)
                      .HasPrecision(18, 2);

                entity.HasOne(cp => cp.Cita)
                      .WithMany(c => c.CitaPiezas)
                      .HasForeignKey(cp => cp.CitaId);

                entity.HasOne(cp => cp.Pieza)
                      .WithMany(p => p.CitaPiezas)
                      .HasForeignKey(cp => cp.PiezaId);
            });
            modelBuilder.Entity<Pieza>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Nombre)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(p => p.Referencia)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasIndex(p => p.Referencia)
                      .IsUnique();   // 🔥 Índice único

                entity.Property(p => p.Precio)
                      .HasPrecision(18, 2);

                entity.Property(p => p.Stock)
                      .IsRequired();

                entity.Property(p => p.Activa)
                      .IsRequired();
            });
            modelBuilder.Entity<CitaPieza>(entity =>
            {
                entity.HasKey(cp => new { cp.CitaId, cp.PiezaId });

                entity.Property(cp => cp.PrecioUnitario)
                .HasPrecision(18, 2);

                entity.Property(cp => cp.Cantidad)
                .IsRequired();

                entity.HasOne(cp => cp.Cita)
                      .WithMany(c => c.CitaPiezas)
                      .HasForeignKey(cp => cp.CitaId )
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(cp => cp.Pieza)
                      .WithMany(c => c.CitaPiezas)
                      .HasForeignKey(cp => cp.PiezaId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
