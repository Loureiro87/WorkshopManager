using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopManager.Domain.Entities;
using WorkshopManager.Domain.Enums;

namespace WorkshopManager.Application.Interfaces
{
    public interface ICitaService
    {
        Task<IEnumerable<Cita>> GetAllAsync();
        Task CreateAsync(int clienteId, int vehiculoId
            ,DateTime fechaEntrega
            ,DateTime? fechaEstimadaFin
            ,string? observacion);
        Task ChangeEstadoAsync(int citaId, CitaEstado nuevoEstado);
        Task UpdateAsync(int id, int clienteId, int vehiculoId, DateTime fechaEntrega
            ,DateTime? fechaEstimadaFin, string? observaciones);
        Task DeleteAsync(int id);
    }
}
