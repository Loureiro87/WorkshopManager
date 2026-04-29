using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopManager.Domain.Entities
{
    public class CitaPieza
    {
        public int CitaId { get; set; }
        public Cita Cita { get; set; } = null!;

        public int PiezaId { get; set; }
        public Pieza Pieza { get; set; } = null!;

        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
