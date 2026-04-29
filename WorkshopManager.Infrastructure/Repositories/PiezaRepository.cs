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
    public class PiezaRepository : IPiezaRepository
    {
        private readonly WorkshopDbContext _context;
        public PiezaRepository(WorkshopDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Pieza>> GetAllAsync()
        {
            return await _context.Piezas
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IEnumerable<Pieza>> GetByActivaAsync(bool activa)
        {
            return await _context.Piezas
                .Where(p => p.Activa == activa)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Pieza?> GetByIdAsync(int id)
        {
           return await _context.Piezas
                  .Include(p => p.CitaPiezas)
                  .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddAsync(Pieza pieza)
        {
             _context.Piezas.Add(pieza);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Pieza pieza)
        {
            _context.Piezas.Update(pieza);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Pieza pieza)
        {
            _context.Piezas.Remove(pieza);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistsByNombreAsync(string nombre)
        {
            return await _context.Piezas.AnyAsync(p => p.Nombre == nombre);
        }
        public async Task<bool> ExistsByReferenciaAsync(string refenrencia)
        {
            return await _context.Piezas.AllAsync(p => p.Referencia == refenrencia);
        }
    }
}
