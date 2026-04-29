using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopManager.Domain.Entities;

namespace WorkshopManager.Domain.Interfaces
{
    public interface IPiezaRepository
    {
        Task<IEnumerable<Pieza>> GetAllAsync();
        Task<IEnumerable<Pieza>> GetByActivaAsync(bool activa);
        Task<Pieza?> GetByIdAsync(int id);
        Task AddAsync(Pieza pieza);
        Task UpdateAsync(Pieza pieza);
        Task DeleteAsync(Pieza pieza);
        Task<bool> ExistsByNombreAsync(string nombre);
        Task<bool> ExistsByReferenciaAsync(string referencia);
    }
}
