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
        }
    }
}
