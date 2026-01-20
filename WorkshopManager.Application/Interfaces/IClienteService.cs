using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopManager.Domain.Entities;

namespace WorkshopManager.Application.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task CreateAsync(string nombre, string? telefono, string? email);
    }
}
