using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopManager.Application.Interfaces;
using WorkshopManager.Domain.Entities;
using WorkshopManager.Domain.Interfaces;

namespace WorkshopManager.Application.Services
{
    public class PiezaService : IPiezaService
    {
        private readonly IPiezaRepository _piezaRepository;

        public PiezaService(IPiezaRepository piezaRepository)
        {
            _piezaRepository = piezaRepository;
        }
        // -----------------------------
        // LECTURA
        // -----------------------------

        public async Task<IEnumerable<Pieza>> GetAllAsync()
        {
            return await _piezaRepository.GetAllAsync();
        }
        public async Task<IEnumerable<Pieza>> GetActivasAsync()
        {
            return await _piezaRepository.GetByActivaAsync(true);
        }

        public async Task<Pieza> GetByIdAsync(int id)
        {
            var pieza = await _piezaRepository.GetByIdAsync(id);
            if (pieza == null)
            {
                throw new InvalidOperationException("La pieza no existe"); 
            }
            return pieza;
        }
        // -------------------------
        // CREACIÓN
        // -------------------------
        public async Task CreateAsync(string nombre, string referencia, decimal precio, int stock)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new InvalidOperationException("El nombre es obligatorio.");

            if (precio < 0)
                throw new InvalidOperationException("El precio no puede ser negativo.");

            if (stock < 0)
                throw new InvalidOperationException("El stock no puede ser negativo.");

            if (await _piezaRepository.ExistsByNombreAsync(nombre))
                throw new InvalidOperationException("Ya existe una pieza con ese nombre.");

            if (string.IsNullOrWhiteSpace(referencia))
                throw new InvalidOperationException("La referencia es obligatoria.");


            var pieza = new Pieza
            {
                Nombre = nombre,
                Referencia = referencia,
                Precio = precio,
                Stock = stock,
                Activa = true
            };

            await _piezaRepository.AddAsync(pieza);
        }

        // -----------------------
        // EDICIÓN GENERAL
        // -----------------------
        public async Task UpdateAsync(int id, string nombre, string referencia, decimal precio, int stock)
        {
            var pieza = await _piezaRepository.GetByIdAsync(id);
            if(pieza == null)
            {
                throw new InvalidOperationException("La pieza no existe");
            }
            if (string.IsNullOrWhiteSpace(nombre))
                throw new InvalidOperationException("El nombre no puede estar en blanco");
            if (precio < 0)
                throw new InvalidOperationException("El precio no puede ser negativo.");

            if (stock < 0)
                throw new InvalidOperationException("El stock no puede ser negativo.");

            if (await _piezaRepository.ExistsByReferenciaAsync(referencia))
                throw new InvalidOperationException("Ya existe una pieza con esa referencia.");


            if (nombre != pieza.Nombre)
            {
                if (await _piezaRepository.ExistsByNombreAsync(nombre))
                {
                    throw new InvalidOperationException($"{nombre} no puede estar repetido en otra pieza");
                }
            }
            pieza.Nombre = nombre;
            pieza.Referencia = referencia;
            pieza.Precio = precio;
            pieza.Stock = stock;

            await _piezaRepository.UpdateAsync(pieza);

        }
        // ----------------------
        // DESHABILITAR PIEZA
        // ----------------------

        public async Task DeshabilitarAsync(int id)
        {
            var pieza = await _piezaRepository.GetByIdAsync(id);

            if (pieza == null)
                throw new InvalidOperationException("La pieza no existe");
            if (!pieza.Activa)
                return;// Ya esta deshabilitada.

            pieza.Activa = false;

           await _piezaRepository.UpdateAsync(pieza);
        }
        public async Task ActivarAsync(int id)
        {
            var pieza = await _piezaRepository.GetByIdAsync(id);

            if (pieza == null)
                throw new InvalidOperationException("La pieza no existe.");
            if (pieza.Activa)
            {
                return; // La pieza ya esta activa, no hacemos cambios y volvemos.
            }
            pieza.Activa = true;
            await _piezaRepository.UpdateAsync(pieza);
        }
        // -----------------
        // COMPROVACIÓN DE STOCK
        // -----------------
        public async Task<bool> HayStockSuficienteAsync(int piezaId, int cantidad)
        {
            var pieza = await _piezaRepository.GetByIdAsync(piezaId);

            if (pieza == null)
                throw new InvalidOperationException("La pieza no existe.");

            return pieza.Stock >= cantidad;
        }

    }
}
