using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WorkshopManager.Web.ViewModels
{
    public class CitaEditViewModel : ICitaSelectableViewModel
    {
        public int Id {  get; set; }

        [Required(ErrorMessage ="Debe seleccionar un cliente.")]
        [Display(Name = "Cliente")]
        public int? ClienteId { get; set; }

        [Required(ErrorMessage = "Debes seleccionar un vehiculo.")]
        [Display(Name = "Vehículo")]
        public int? VehiculoId { get; set; }

        [Required(ErrorMessage = "La fecha de entregar es obligatoria.")]
        [Display(Name = "Fecha de Entrega")]
        public DateTime FechaEntrega { get; set; }

        [Display(Name = "Fecha estimada de finalización")]
        public DateTime? FechaEstimadaFin { get; set; }

        [Display(Name = "Observaciones")]
        public string? Observaciones { get; set; }

        //Dropdowns
        public List<SelectListItem> Clientes { get; set; } = new();
        public List<SelectListItem> Vehiculos { get; set; } = new();
    }
}
