using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WorkshopManager.Web.ViewModels
{
    public class CitaCreateViewModel 
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Debes seleccionar un cliente")]
        [Display(Name = "Cliente")]
        public int? ClienteId { get; set; }

        [Required(ErrorMessage = "Debes seleccionar un vehículo")]
        [Display(Name = "Vehiculo")]
        public int? VehiculoId { get; set; }

        [Required]
        [Display(Name = "Fecha de entrega")]
        public DateTime FechaEntrega { get; set; }
        [Display(Name = "Fecha estimada de finalización")]
        public DateTime? FechaEstimadaFin { get; set; }
        public string? Observaciones { get; set; }

        [ValidateNever]
        public List<SelectListItem> Clientes { get; set; } = new();
        [ValidateNever]
        public List<SelectListItem> Vehiculos { get; set; } = new();
    }
}
