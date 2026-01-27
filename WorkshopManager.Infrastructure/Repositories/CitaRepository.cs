using Microsoft.EntityFrameworkCore;
using WorkshopManager.Domain.Entities;
using WorkshopManager.Domain.Interfaces;
using WorkshopManager.Infrastructure.Data;

namespace WorkshopManager.Infrastructure.Repositories
{
    public class CitaRepository : ICitaRepository
    {
        private readonly WorkshopDbContext _context;

        public CitaRepository(WorkshopDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Cita>> GetAllAsync()
        {
            return await _context.Citas
                        .Include(c => c.Cliente)
                        .Include(c => c.Vehiculo)
                        .AsNoTracking()
                        .ToListAsync();
      
        }
        public async Task<Cita?> GetByIdAsync(int id)
        {
            return await _context.Citas
                .Include(c => c.Cliente)
                .Include(c => c.Vehiculo)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task AddAsync(Cita cita)
        {
            _context.Citas.Add(cita);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Cita cita)
        {
            _context.Update(cita);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Cita cita)
        {
            _context.Citas.Remove(cita);
            await _context.SaveChangesAsync();
        }
    }
}
