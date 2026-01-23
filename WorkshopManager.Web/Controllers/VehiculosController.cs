using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WorkshopManager.Application.Interfaces;
using WorkshopManager.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorkshopManager.Domain.Entities;

namespace WorkshopManager.Web.Controllers
{
    public class VehiculosController : Controller
    {
        private readonly IVehiculoService _vehiculoService;
        private readonly IClienteService _clienteService;

        public VehiculosController(IVehiculoService vehiculoService, IClienteService clienteService)
        {
            _vehiculoService = vehiculoService;
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new VehiculoCreateViewModel();
            await LoadClientesAsync(vm);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VehiculoCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                await LoadClientesAsync(vm);
                return View(vm);
            }

            try
            {
                await _vehiculoService.CreateAsync(
                    vm.Marca,
                    vm.Modelo,
                    vm.Matricula,
                    vm.ClienteId!.Value);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(nameof(vm.Matricula), ex.Message);
                await LoadClientesAsync(vm);
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vehiculos = await _vehiculoService.GetAllAsync();
            return View(vehiculos);
        }
        private async Task LoadClientesAsync(IClienteSelectableViewModel vm)
        {
            var clientes = await _clienteService.GetAllAsync();

            vm.Clientes = clientes.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Nombre,
                Selected = vm.ClienteId.HasValue && c.Id == vm.ClienteId.Value
            }).ToList();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var vehiculo = await _vehiculoService.GetByIdAsync(id);

            if (vehiculo == null)
                return NotFound();

            var vm = new VehiculoEditViewModel
            {
                Id = vehiculo.Id,
                Marca = vehiculo.Marca,
                Modelo = vehiculo.Modelo,
                Matricula = vehiculo.Matricula,
                ClienteId = vehiculo.ClienteId
            };

            await LoadClientesAsync(vm);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(VehiculoEditViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                await LoadClientesAsync(vm);
                return View(vm);
            }

            await _vehiculoService.UpdateAsync(
                vm.Id,
                vm.Marca,
                vm.Modelo,
                vm.Matricula,
                vm.ClienteId!.Value
            );
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var vehiculo = await _vehiculoService.GetByIdAsync(id);
            if (vehiculo == null)
            {
                return NotFound();
            }
            return View(vehiculo);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _vehiculoService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
