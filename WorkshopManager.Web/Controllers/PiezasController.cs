using Microsoft.AspNetCore.Mvc;
using WorkshopManager.Application.Interfaces;
using WorkshopManager.Domain.Entities;
using WorkshopManager.Web.ViewModels;

namespace WorkshopManager.Web.Controllers
{
    public class PiezasController : Controller
    {
        private readonly IPiezaService _piezasService;
        public PiezasController(IPiezaService piezasService)
        {
            _piezasService = piezasService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var piezas = await _piezasService.GetAllAsync();
            return View(piezas);
        }
        [HttpPost]
        public async Task<IActionResult> DeshabilitarAsync(int id)
        {
            await _piezasService.DeshabilitarAsync(id);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Activar(int id)
        {
            await _piezasService.ActivarAsync(id);
            return RedirectToAction(nameof(Index));
        }
        // ---------------------
        // CREACION PIEZA 
        // ---------------------
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PiezaCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            try
            {
                await _piezasService.CreateAsync(
                     vm.Nombre,
                     vm.Referencia ?? string.Empty,
                     vm.Precio,
                     vm.Stock
                    );
            }
            catch(InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id) 
        {
            var pieza   = await _piezasService.GetByIdAsync(id);

            var vm = new PiezaEditViewModel
            {
                Id = pieza.Id,
                Nombre = pieza.Nombre,
                Referencia = pieza.Referencia,
                Precio = pieza.Precio,
                Stock = pieza.Stock
            };
            return View(vm);
        }
        public async Task<IActionResult> Edit(PiezaEditViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            try
            {
                await _piezasService.UpdateAsync(
                        vm.Id,
                        vm.Nombre,
                        vm.Referencia,
                        vm.Precio,
                        vm.Stock
                    );
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
