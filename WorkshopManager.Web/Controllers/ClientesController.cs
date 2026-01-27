using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WorkshopManager.Application.Interfaces;
using WorkshopManager.Web.ViewModels;

namespace WorkshopManager.Web.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var clientes = await _clienteService.GetAllAsync();
            return View(clientes);
        }
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ClienteCreateViewModel vm) 
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                await _clienteService.CreateAsync(
                    vm.Nombre,
                    vm.Telefono,
                    vm.Email
                    );
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty,ex.Message);
                return View(vm);
            }
            TempData["Success"] = "Cliente creado correctamente";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var cliente = await _clienteService.GetByIdAsync(id);

            if (cliente == null ) { return NotFound(); }
            var vm = new ClienteEditViewModel
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Telefono = cliente.Telefono,
                Email = cliente.Email,

            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ClienteEditViewModel vm) 
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }
            await _clienteService.UpdateAsync(
                vm.Id,
                vm.Nombre,
                vm.Telefono,
                vm.Email
                );
            TempData["Success"] = "Cliente actualizado correctamente";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _clienteService.GetByIdAsync(id);

            if(cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
             await _clienteService.DeleteAsync(id);
            TempData["Success"] = "Cliente eliminado correctamente";
            return RedirectToAction(nameof(Index));
        }
    }
}
  