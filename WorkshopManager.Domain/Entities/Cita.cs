using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopManager.Domain.Enums;

namespace WorkshopManager.Domain.Entities
{
    public class Cita
    {
        public int Id { get; set; }
        public int ClienteId { get; set; } 
        public Cliente Cliente { get; set; }
        public int VehiculoId { get; set; } 
        public Vehiculo Vehiculo { get; set; }
        public DateTime FechaEntrega { get; set; }
        public DateTime? FechaEstimadaFin { get; set; }
        public string? Observaciones { get; set; }
        public CitaEstado Estado { get; set; }

    }
}
