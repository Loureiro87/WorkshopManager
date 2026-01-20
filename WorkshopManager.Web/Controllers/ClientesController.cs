using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WorkshopManager.Application.Interfaces;

namespace WorkshopManager.Web.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

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
        public async Task<IActionResult> Create(string nombre, string? telefono, string? email) 
        {
            await _clienteService.CreateAsync(nombre, telefono, email);

            return RedirectToAction(nameof(Index));
        }
    }
}
  