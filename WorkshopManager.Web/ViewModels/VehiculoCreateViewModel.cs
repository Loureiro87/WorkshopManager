using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkshopManager.Web.ViewModels
{
    public class VehiculoCreateViewModel : IClienteSelectableViewModel
    {
        [Required(ErrorMessage ="La marca es obligatoria")]
        [StringLength(50, ErrorMessage ="La marca no puede superar los 50 caracteres")]
        public string Marca { get; set; } = null!;

        [Required(ErrorMessage ="El modelo es obligatorio")]
        [StringLength(50, ErrorMessage ="El modelo no puede superar los 50 caracteres")]
        public string Modelo { get; set; } = null!;

        [Required(ErrorMessage ="La Matricula es obligatoria")]
        [StringLength(10, ErrorMessage = "La matrícula no puede superar los 10 caracteres")]
        public string Matricula { get; set; } = null!;

        [Required(ErrorMessage = "Debes seleccionar un cliente")]
        public int? ClienteId { get; set; }

        // Para el dropdown
        public List<SelectListItem> Clientes { get; set; } = new();
    }
}
