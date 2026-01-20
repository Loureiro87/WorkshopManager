using System.Collections.Generic;
using System.Threading.Tasks;
using WorkshopManager.Domain.Entities;

namespace WorkshopManager.Domain.Interfaces
{
    public interface IVehiculoRepository
    {
        Task<IEnumerable<Vehiculo>> GetAllAsync();

        Task<Vehiculo?> GetByIdAsync(int id);
        Task AddAsync(Vehiculo vehiculo);
        Task<bool> ExistsByMatriculaAsync(string matricula);
        Task UpdateAsync(Vehiculo vehiculo);

    }

}
 