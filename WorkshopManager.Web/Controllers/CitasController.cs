using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WorkshopManager.Application.Interfaces;
using WorkshopManager.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WorkshopManager.Web.Controllers
{
    public class CitasController : Controller
    {
        private readonly ICitaService _citaService;
        private readonly IClienteService _clienteService;
        private readonly IVehiculoService _vehiculoService;
        public CitasController(ICitaService citaService, IClienteService clienteService, IVehiculoService vehiculoService) 
        {
            _citaService = citaService;
            _clienteService = clienteService;
            _vehiculoService = vehiculoService;
        }
        public async Task<IActionResult> Index()
        {
           var cita = await _citaService.GetAllAsync();
            return View(cita);
        }
        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            var vm = new CitaCreateViewModel
            {
                FechaEntrega = DateTime.Today
            };
            await LoadDropdownsAsync(vm);
                return View(vm);
            
        }
        [HttpPost]
        public async Task<IActionResult> Create(CitaCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync(vm);
                return View(vm);
            }

            try
            {
                await _citaService.CreateAsync(
                    vm.ClienteId!.Value,
                    vm.VehiculoId!.Value,
                    vm.FechaEntrega,
                    vm.FechaEstimadaFin,
                    vm.Observaciones
                );
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }

            TempData["Success"] = "Cita creada correctamente";
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadDropdownsAsync(CitaCreateViewModel vm)
        {
            var clientes = await _clienteService.GetAllAsync();
            var vehiculos = await _vehiculoService.GetAllAsync();

            vm.Clientes = clientes.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Nombre,
            }).ToList();
            vm.Vehiculos = vehiculos.Select(v => new SelectListItem
            {
                Value = v.Id.ToString(),
                Text = $"{v.Marca} {v.Modelo} ({v.Matricula})"
            }).ToList();
        }
    }
}
