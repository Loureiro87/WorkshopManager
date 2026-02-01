using WorkshopManager.Domain.Enums;

namespace WorkshopManager.Web.ViewModels
{ 
public class CitaDetalleViewModel
{
    public int Id { get; set; }

    public string ClienteNombre { get; set; } = null!;
    public string ClienteTelefono { get; set; } = null!;
    public string ClienteEmail { get; set; } = null!;

    public string VehiculoDescripcion { get; set; } = null!;
    public string VehiculoMatricula { get; set; } = null!;

    public DateTime FechaEntrega { get; set; }
    public DateTime? FechaEstimadaFin { get; set; }
    public string? Observaciones { get; set; }

    public CitaEstado Estado { get; set; }
}
}