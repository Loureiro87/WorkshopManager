using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WorkshopManager.Web.ViewModels
{
    public interface IClienteSelectableViewModel 
    {
        int? ClienteId { get; set; }
        List<SelectListItem> Clientes { get; set; }
    }
}
