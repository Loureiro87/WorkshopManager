using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopManager.Domain.Entities
{
    public class Pieza
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Referencia { get; set; } = null!;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public bool Activa { get; set; }
        public ICollection<CitaPieza> CitaPiezas { get; set;} = new List<CitaPieza>();
    }
}
