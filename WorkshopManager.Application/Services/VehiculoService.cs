using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopManager.Application.Interfaces;
using WorkshopManager.Domain.Entities;
using WorkshopManager.Domain.Interfaces;

namespace WorkshopManager.Application.Services
{
    public class VehiculoService : IVehiculoService
    {
        private readonly IVehiculoRepository _vehiculoRepository;
        
        public VehiculoService(IVehiculoRepository vehiculoRepository)
        {
            _vehiculoRepository = vehiculoRepository;
        }
        public async Task<IEnumerable<Vehiculo>> GetAllAsync()
        {
            return await _vehiculoRepository.GetAllAsync();
        }
        public async Task<Vehiculo?> GetByIdAsync(int id)
        {
            return await _vehiculoRepository.GetByIdAsync(id);
        }
        public async Task CreateAsync(string marca, string modelo, string matricula, int clienteId)
        {
            var exists = await _vehiculoRepository.ExistsByMatriculaAsync(matricula);
            if (exists)
            {
                throw new InvalidOperationException("Ya existe un vehículo con esa matrícula");
            }
            Vehiculo vehiculo = new Vehiculo()
            {
                Marca = marca,
                Modelo = modelo,
                Matricula = matricula,
                ClienteId = clienteId
            };
            await _vehiculoRepository.AddAsync(vehiculo);
        }
        public async Task UpdateAsync(int id, string marca, string modelo, string matricula, int clienteId)
        {
            var vehiculo = await _vehiculoRepository.GetByIdAsync(id);

            if(vehiculo == null)
            {
                throw new InvalidOperationException("Vehicvulo no encontrado");
            }
            vehiculo.Marca = marca;
            vehiculo.Modelo = modelo;
            vehiculo.Matricula  = matricula;
            vehiculo.ClienteId = clienteId;

            await _vehiculoRepository.UpdateAsync(vehiculo);
        }
        public async Task DeleteAsync(int id)
        {
            var vehiculo = await _vehiculoRepository.GetByIdAsync(id);
            if (vehiculo == null)
            {
                throw new InvalidOperationException("Vehiculo no encontrado");
            }
            await _vehiculoRepository.DeleteAsync(vehiculo);
        }
    }
}
