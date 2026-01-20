using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopManager.Domain.Entities;

namespace WorkshopManager.Application.Interfaces
{
    public interface IVehiculoService
    {
        Task<IEnumerable<Vehiculo>> GetAllAsync();
        Task<Vehiculo?> GetByIdAsync(int id);
        Task CreateAsync(string marca, string modelo, string matricula, int clienteId);
        Task UpdateAsync(int id, string marca, string modelo, string matricula, int clienteId);

    }
}
