using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopManager.Domain.Entities;

namespace WorkshopManager.Application.Interfaces
{
    public interface IPiezaService
    {
        Task<IEnumerable<Pieza>> GetAllAsync();
        Task<IEnumerable<Pieza>> GetActivasAsync();
        Task<Pieza> GetByIdAsync(int id);
        Task CreateAsync(string nombre, string referencia, decimal precio, int stock);
        Task UpdateAsync(int id, string nombre, string referencia, decimal precio, int stock);

        Task DeshabilitarAsync(int id);
        Task ActivarAsync(int id);
        Task<bool> HayStockSuficienteAsync(int piezaId, int cantidad);

    }
}
