using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorkshopManager.Application.Interfaces;
using WorkshopManager.Domain.Enums;
using WorkshopManager.Web.ViewModels;

namespace WorkshopManager.Web.Controllers
{
    public class CitasController : Controller
    {
        private readonly ICitaService _citaService;
        private readonly IClienteService _clienteService;
        private readonly IVehiculoService _vehiculoService;

        public CitasController(
            ICitaService citaService,
            IClienteService clienteService,
            IVehiculoService vehiculoService)
        {
            _citaService = citaService;
            _clienteService = clienteService;
            _vehiculoService = vehiculoService;
        }

        // -------------------------
        // LISTADO
        // -------------------------

        public async Task<IActionResult> Index()
        {
            var citas = await _citaService.GetAllAsync();
            return View(citas);
        }

        // -------------------------
        // CREAR CITA
        // -------------------------

        [HttpGet]
        public async Task<IActionResult> Create()
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
                await LoadDropdownsAsync(vm);
                return View(vm);
            }

            TempData["Success"] = "Cita creada correctamente";
            return RedirectToAction(nameof(Index));
        }

        // -------------------------
        // DETALLE
        // -------------------------

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var cita = await _citaService.GetByIdAsync(id);
            if (cita == null)
                return NotFound();

            var vm = new CitaDetalleViewModel
            {
                Id = cita.Id,

                ClienteNombre = cita.Cliente.Nombre,
                ClienteTelefono = cita.Cliente.Telefono!,
                ClienteEmail = cita.Cliente.Email!,

                VehiculoDescripcion = $"{cita.Vehiculo.Marca} {cita.Vehiculo.Modelo}",
                VehiculoMatricula = cita.Vehiculo.Matricula,

                FechaEntrega = cita.FechaEntrega,
                FechaEstimadaFin = cita.FechaEstimadaFin,
                Observaciones = cita.Observaciones,
                Estado = cita.Estado
            };

            return View(vm);
        }

        // -------------------------
        // CAMBIO DE ESTADO
        // -------------------------

        [HttpGet]
        public async Task<IActionResult> ChangeEstado(int id)
        {
            var cita = await _citaService.GetByIdAsync(id);
            if (cita == null)
                return NotFound();

            var vm = new CitaEstadoViewModel
            {
                CitaId = cita.Id,
                NuevoEstado = cita.Estado,
                Estados = Enum.GetValues(typeof(CitaEstado))
                              .Cast<CitaEstado>()
                              .Select(e => new SelectListItem
                              {
                                  Value = e.ToString(),
                                  Text = e.ToString()
                              }).ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeEstado(CitaEstadoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Estados = Enum.GetValues(typeof(CitaEstado))
                                 .Cast<CitaEstado>()
                                 .Select(e => new SelectListItem
                                 {
                                     Value = e.ToString(),
                                     Text = e.ToString()
                                 }).ToList();

                return View(vm);
            }

            try
            {
                await _citaService.ChangeEstadoAsync(
                    vm.CitaId,
                    vm.NuevoEstado!.Value
                );
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                vm.Estados = Enum.GetValues(typeof(CitaEstado))
                                 .Cast<CitaEstado>()
                                 .Select(e => new SelectListItem
                                 {
                                     Value = e.ToString(),
                                     Text = e.ToString()
                                 }).ToList();

                return View(vm);
            }

            TempData["Success"] = "Estado de la cita actualizado correctamente";
            return RedirectToAction(nameof(Index));
        }

        // -------------------------
        // MÉTODOS PRIVADOS
        // -------------------------

        private async Task LoadDropdownsAsync(CitaCreateViewModel vm)
        {
            var clientes = await _clienteService.GetAllAsync();
            var vehiculos = await _vehiculoService.GetAllAsync();

            vm.Clientes = clientes.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Nombre
            }).ToList();

            vm.Vehiculos = vehiculos.Select(v => new SelectListItem
            {
                Value = v.Id.ToString(),
                Text = $"{v.Marca} {v.Modelo} ({v.Matricula})"
            }).ToList();
        }
    }
}
