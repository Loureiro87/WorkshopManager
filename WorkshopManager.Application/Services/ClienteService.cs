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
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            return await _clienteRepository.GetAllAsync();
        }
        public async Task CreateAsync(string nombre, string? telefono, string? email)
        {
            var cliente = new Cliente
            {
                Nombre = nombre,
                Telefono = telefono,
                Email = email
            };

            await _clienteRepository.AddAsync(cliente);
        }
    }
}