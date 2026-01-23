using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WorkshopManager.Web.ViewModels;

namespace WorkshopManager.Web.ViewModels
{
    public class VehiculoEditViewModel : IClienteSelectableViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Marca { get; set; }
        [Required]
        public string Modelo { get; set; }

        [Required]
        public string Matricula { get; set; }

        [Required(ErrorMessage = "Debes seleccionar un cliente")]
        public int? ClienteId { get; set; }

        [ValidateNever]
        public List<SelectListItem> Clientes { get; set; }

    }
}