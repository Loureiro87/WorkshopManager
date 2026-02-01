using WorkshopManager.Application.Interfaces;
using WorkshopManager.Domain.Entities;
using WorkshopManager.Domain.Enums;
using WorkshopManager.Domain.Interfaces;

namespace WorkshopManager.Application.Services
{
    public class CitaService : ICitaService
    {
        private readonly ICitaRepository _citaRepository;

        // Transiciones de estado permitidas
        private static readonly Dictionary<CitaEstado, CitaEstado[]> _transicionesPermitidas =
            new()
            {
                { CitaEstado.PendienteEntrega, new[] { CitaEstado.EnTaller, CitaEstado.Cancelada } },
                { CitaEstado.EnTaller, new[] { CitaEstado.EnProceso, CitaEstado.Cancelada } },
                { CitaEstado.EnProceso, new[] { CitaEstado.EnEsperaRecogida } },
                { CitaEstado.EnEsperaRecogida, new[] { CitaEstado.Finalizada } }
            };

        public CitaService(ICitaRepository citaRepository)
        {
            _citaRepository = citaRepository;
        }

        // -------------------------
        // LECTURA
        // -------------------------

        public async Task<IEnumerable<Cita>> GetAllAsync()
        {
            return await _citaRepository.GetAllAsync();
        }

        public async Task<Cita?> GetByIdAsync(int id)
        {
            return await _citaRepository.GetByIdAsync(id);
        }

        // -------------------------
        // CREACIÓN
        // -------------------------

        public async Task CreateAsync(
            int clienteId,
            int vehiculoId,
            DateTime fechaEntrega,
            DateTime? fechaEstimadaFin,
            string? observaciones)
        {
            if (fechaEntrega.Date < DateTime.Today)
                throw new InvalidOperationException("No se puede crear una cita en el pasado");

            var cita = new Cita
            {
                ClienteId = clienteId,
                VehiculoId = vehiculoId,
                FechaEntrega = fechaEntrega,
                FechaEstimadaFin = fechaEstimadaFin,
                Observaciones = observaciones,
                Estado = CitaEstado.PendienteEntrega
            };

            await _citaRepository.AddAsync(cita);
        }

        // -------------------------
        // EDICIÓN GENERAL
        // -------------------------

        public async Task UpdateAsync(
            int id,
            int clienteId,
            int vehiculoId,
            DateTime fechaEntrega,
            DateTime? fechaEstimadaFin,
            string? observaciones)
        {
            var cita = await _citaRepository.GetByIdAsync(id);
            if (cita == null)
                throw new InvalidOperationException("La cita no existe");

            if (cita.Estado == CitaEstado.Finalizada || cita.Estado == CitaEstado.Cancelada)
                throw new InvalidOperationException("No se puede editar una cita cerrada");

            if (fechaEntrega.Date < DateTime.Today)
                throw new InvalidOperationException("La fecha de entrega no puede ser anterior a hoy");

            cita.ClienteId = clienteId;
            cita.VehiculoId = vehiculoId;
            cita.FechaEntrega = fechaEntrega;
            cita.FechaEstimadaFin = fechaEstimadaFin;
            cita.Observaciones = observaciones;

            await _citaRepository.UpdateAsync(cita);
        }

        // -------------------------
        // CAMBIO DE ESTADO
        // -------------------------

        public async Task ChangeEstadoAsync(int citaId, CitaEstado nuevoEstado)
        {
            var cita = await _citaRepository.GetByIdAsync(citaId);
            if (cita == null)
                throw new InvalidOperationException("La cita no existe");

            if (cita.Estado == nuevoEstado)
                throw new InvalidOperationException("La cita ya se encuentra en ese estado");

            if (cita.Estado == CitaEstado.Finalizada || cita.Estado == CitaEstado.Cancelada)
                throw new InvalidOperationException("No se puede modificar una cita cerrada");

            if (!_transicionesPermitidas.TryGetValue(cita.Estado, out var estadosPermitidos) ||
                !estadosPermitidos.Contains(nuevoEstado))
                throw new InvalidOperationException("Transición de estado no permitida");

            cita.Estado = nuevoEstado;
            await _citaRepository.UpdateAsync(cita);
        }

        // -------------------------
        // ELIMINACIÓN
        // -------------------------

        public async Task DeleteAsync(int id)
        {
            var cita = await _citaRepository.GetByIdAsync(id);
            if (cita == null)
                throw new InvalidOperationException("La cita no existe");

            if (cita.Estado == CitaEstado.Finalizada)
                throw new InvalidOperationException("No se puede eliminar una cita finalizada");

            await _citaRepository.DeleteAsync(cita);
        }
    }
}
