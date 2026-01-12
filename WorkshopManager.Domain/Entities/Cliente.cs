using System.Collections.Generic;

namespace WorkshopManager.Domain.Entities
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? Email { get; set; }

        public ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
    }
}
