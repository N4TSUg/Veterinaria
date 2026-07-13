using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veterinaria.Data;
using Veterinaria.Models;
using Veterinaria.ViewModels.Cliente;

namespace Veterinaria.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        private readonly VeterinariaDbContext _context;

        public ClienteController(VeterinariaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var clientes = await _context.Clientes.ToListAsync();
            return View(clientes);
        }

        public async Task<IActionResult> Details(int id)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Mascotas)
                .FirstOrDefaultAsync(c => c.IdCliente == id);

            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        public IActionResult Create()
        {
            return View(new ClienteCreateVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClienteCreateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var cliente = new Cliente
            {
                Nombre = model.Nombre,
                Apellido = model.Apellido,
                DNI = model.DNI,
                Telefono = model.Telefono,
                Direccion = model.Direccion,
                Correo = model.Correo
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Cliente registrado exitosamente.";
            return RedirectToAction(nameof(Details), new { id = cliente.IdCliente });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return NotFound();

            var vm = new ClienteEditVM
            {
                IdCliente = cliente.IdCliente,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                DNI = cliente.DNI,
                Telefono = cliente.Telefono,
                Direccion = cliente.Direccion,
                Correo = cliente.Correo
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ClienteEditVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var cliente = await _context.Clientes.FindAsync(model.IdCliente);
            if (cliente == null) return NotFound();

            cliente.Nombre = model.Nombre;
            cliente.Apellido = model.Apellido;
            cliente.DNI = model.DNI;
            cliente.Telefono = model.Telefono;
            cliente.Direccion = model.Direccion;
            cliente.Correo = model.Correo;

            await _context.SaveChangesAsync();

            TempData["Success"] = "Cliente actualizado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Cliente eliminado.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
