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

                entity.HasOne(c =>c.Cliente)
                      .WithMany()
                      .HasForeignKey(c => c.ClienteId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Vehiculo)
                      .WithMany()
                      .HasForeignKey(c=> c.VehiculoId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(c => c.Observaciones)
                      .HasMaxLength(1000);

                entity.Property(c => c.Estado)
                      .IsRequired();
            });
        }
    }
}
