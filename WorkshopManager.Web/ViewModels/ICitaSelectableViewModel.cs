using Microsoft.AspNetCore.Mvc.Rendering;

namespace WorkshopManager.Web.ViewModels
{
    public interface ICitaSelectableViewModel
    {
        int? ClienteId { get; set; }
        List<SelectListItem> Clientes { get; set; }

        int? VehiculoId { get; set; }
        List<SelectListItem> Vehiculos { get; set; }
    }
}
