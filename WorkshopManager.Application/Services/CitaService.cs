using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopManager.Application.Interfaces;
using WorkshopManager.Domain.Entities;
using WorkshopManager.Domain.Enums;
using WorkshopManager.Domain.Interfaces;

namespace WorkshopManager.Application.Services
{
    public class CitaService : ICitaService
    {
        private readonly ICitaRepository _citaRepository;

        public CitaService(ICitaRepository citaService)
        {
            _citaRepository = citaService;
        }

        public async Task<IEnumerable<Cita>> GetAllAsync()
        {
            return await _citaRepository.GetAllAsync();
        }
        public async Task CreateAsync(int clienteId, int vehiculoId, DateTime fechaEntrega, DateTime? fechaEstimadaFin, string? observacion )
        {
            if (fechaEntrega < DateTime.Now.Date)
            {
                throw new  InvalidOperationException("No se puede crear una cita en el pasado.");
            }

            var cita = new Cita
            {
                ClienteId = clienteId,
                VehiculoId = vehiculoId,
                FechaEntrega = fechaEntrega,
                FechaEstimadaFin = fechaEstimadaFin,
                Observaciones = observacion,
                Estado = CitaEstado.PendienteEntrega
            };
            await _citaRepository.AddAsync(cita);
        }
        public async Task ChangeEstadoAsync(int citaId, CitaEstado nuevoEstado)
        {
            var cita = await _citaRepository.GetByIdAsync(citaId);

            if (cita == null)
            {
                throw new InvalidOperationException("La cita no existe");
            
            }
            if (cita.Estado == CitaEstado.Finalizada || cita.Estado == CitaEstado.Cancelada) 
            {
                throw new InvalidOperationException("No se puede modificar una cita cerrada");
            }
            if (nuevoEstado == CitaEstado.Finalizada && cita.Estado != CitaEstado.EnProceso)
            {
                throw new InvalidOperationException("Solo se puede finalizar una cita que este en proceso");
            }

            cita.Estado = nuevoEstado;

            await _citaRepository.UpdateAsync(cita);
        }
        public async Task DeleteAsync(int id)
        {
            var cita = await _citaRepository.GetByIdAsync(id);
            if(cita == null)
            {
                throw new InvalidOperationException("La cita no existe.");
            }
            if(cita.Estado == CitaEstado.Finalizada)
            {
                throw new InvalidOperationException("No se puede borrar una cita Finzalizada.");
            }

            await _citaRepository.DeleteAsync(cita);
        }

        public async Task UpdateAsync(int id, int clienteId, int vehiculoId, DateTime fechaEntrega
            , DateTime? fechaEstimadaFin, string? observaciones)
        {
            var cita = await _citaRepository.GetByIdAsync(id);

            if (cita == null)
            {
                throw new InvalidOperationException("La cita no existe.");
            }
            cita.ClienteId = clienteId;
            cita.VehiculoId = vehiculoId;
            cita.FechaEntrega = fechaEntrega;
            cita.FechaEstimadaFin = fechaEstimadaFin;
            cita.Observaciones = observaciones;

            await _citaRepository.UpdateAsync(cita);
        }
    }
}
