namespace WorkshopManager.Domain.Entities
{
    public class Vehiculo
    {
        public int Id { get; set; }

        public string Marca { get; set; } = null!;
        public string Modelo { get; set; } = null!;
        public string Matricula { get; set; } = null!;

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;
    }
}
