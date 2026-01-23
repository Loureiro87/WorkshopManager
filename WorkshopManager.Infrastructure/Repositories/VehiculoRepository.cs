using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopManager.Domain.Entities;
using WorkshopManager.Domain.Interfaces;
using WorkshopManager.Infrastructure.Data;

namespace WorkshopManager.Infrastructure.Repositories
{
    public class VehiculoRepository : IVehiculoRepository
    {
        private readonly WorkshopDbContext _context;

        public VehiculoRepository(WorkshopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vehiculo>> GetAllAsync()
        {
            return await _context.Vehiculos
                .Include(v => v.Cliente)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Vehiculo?> GetByIdAsync(int id)
        {            
            return await _context.Vehiculos
                        .Include(v => v.Cliente)
                        .FirstOrDefaultAsync(v => v.Id == id);
        }
        public async Task AddAsync(Vehiculo vehiculo)
        {
            _context.Vehiculos.Add(vehiculo);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistsByMatriculaAsync(string matricula)
        {
            return await _context.Vehiculos
                    .AnyAsync(v => v.Matricula == matricula);
        }
        public async Task UpdateAsync(Vehiculo vehiculo)
        {
            _context.Vehiculos.Update(vehiculo);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Vehiculo vehiculo)
        {
            _context.Vehiculos.Remove(vehiculo);
            await _context.SaveChangesAsync();
        }
    }
}
