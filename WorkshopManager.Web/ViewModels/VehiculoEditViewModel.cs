using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WorkshopManager.Web.ViewModels;

public class VehiculoEditViewModel : IClienteSelectableViewModel
{
    public int Id { get; set; }

    [Required]
    public string Marca { get; set; }
    [Required]
    public string Modelo { get; set; }

    [Required]
    public string Matricula { get; set; }

    [Required]
    public int? ClienteId { get; set; }

    public  List<SelectListItem> Clientes { get; set; }

}