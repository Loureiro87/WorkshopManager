using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorkshopManager.Domain.Enums;

namespace WorkshopManager.Web.ViewModels
{
    public class CitaEstadoViewModel
    {
        public int Id { get; set; }
        public CitaEstado? EstadoActual { get; set; }

        [Required(ErrorMessage = "Debes seleccionar un estado")]
        [Display(Name = "Nuevo estado")]
        public CitaEstado? NuevoEstado { get; set; }

        public List<SelectListItem> Estados { get; set; } = new();
    }
}
