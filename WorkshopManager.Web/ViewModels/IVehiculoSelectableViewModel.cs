using Microsoft.AspNetCore.Mvc.Rendering;

namespace WorkshopManager.Web.ViewModels
{
    public interface IVehiculoSelectableViewModel
    {
        int? VehiculoId { get; set; }
        List<SelectListItem> Vehiculos { get; set; }
    }
}
