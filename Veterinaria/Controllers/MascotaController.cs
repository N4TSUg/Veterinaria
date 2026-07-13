using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Veterinaria.Data;
using Veterinaria.Models;
using Veterinaria.Services;
using Veterinaria.ViewModels.Mascota;

namespace Veterinaria.Controllers
{
    [Authorize]
    public class MascotaController : Controller
    {
        private readonly VeterinariaDbContext _context;
        private readonly ICloudinaryService _cloudinaryService;

        public MascotaController(VeterinariaDbContext context, ICloudinaryService cloudinaryService)
        {
            _context = context;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<IActionResult> Index()
        {
            var mascotas = await _context.Mascotas
                .Include(m => m.Cliente)
                .AsNoTracking()
                .ToListAsync();
            return View(mascotas);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var mascota = await _context.Mascotas
                .Include(m => m.Cliente)
                .Include(m => m.Consultas)
                .Include(m => m.VacunasAplicadas)
                    .ThenInclude(v => v.Vacuna)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.IdMascota == id);

            if (mascota == null) return NotFound();

            return View(mascota); // The View should be able to receive the entity itself for details, or we can make a VM later.
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? idCliente = null)
        {
            var vm = new MascotaCreateVM
            {
                IdCliente = idCliente ?? 0,
                Clientes = await _context.Clientes
                    .Select(c => new SelectListItem { Value = c.IdCliente.ToString(), Text = $"{c.Nombre} {c.Apellido}" })
                    .ToListAsync()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MascotaCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Clientes = await _context.Clientes
                    .Select(c => new SelectListItem { Value = c.IdCliente.ToString(), Text = $"{c.Nombre} {c.Apellido}" })
                    .ToListAsync();
                return View(model);
            }

            var mascota = new Mascota
            {
                IdCliente = model.IdCliente,
                Nombre = model.Nombre,
                Especie = model.Especie,
                Raza = model.Raza,
                Sexo = model.Sexo,
                Edad = model.Edad,
                Peso = model.Peso
            };

            if (model.Foto != null && model.Foto.Length > 0)
            {
                var uploadUrl = await _cloudinaryService.UploadFileAsync(model.Foto, "mascotas");
                if (uploadUrl != null)
                {
                    mascota.FotoUrl = uploadUrl;
                }
            }

            _context.Mascotas.Add(mascota);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Mascota registrada exitosamente.";
            return RedirectToAction(nameof(Details), new { id = mascota.IdMascota });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var mascota = await _context.Mascotas.FindAsync(id);
            if (mascota == null) return NotFound();

            var vm = new MascotaEditVM
            {
                IdMascota = mascota.IdMascota,
                IdCliente = mascota.IdCliente,
                Nombre = mascota.Nombre,
                Especie = mascota.Especie,
                Raza = mascota.Raza,
                Sexo = mascota.Sexo,
                Edad = mascota.Edad,
                Peso = mascota.Peso,
                FotoUrl = mascota.FotoUrl,
                Clientes = await _context.Clientes
                    .Select(c => new SelectListItem { Value = c.IdCliente.ToString(), Text = $"{c.Nombre} {c.Apellido}" })
                    .ToListAsync()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MascotaEditVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Clientes = await _context.Clientes
                    .Select(c => new SelectListItem { Value = c.IdCliente.ToString(), Text = $"{c.Nombre} {c.Apellido}" })
                    .ToListAsync();
                return View(model);
            }

            var mascota = await _context.Mascotas.FindAsync(model.IdMascota);
            if (mascota == null) return NotFound();

            mascota.IdCliente = model.IdCliente;
            mascota.Nombre = model.Nombre;
            mascota.Especie = model.Especie;
            mascota.Raza = model.Raza;
            mascota.Sexo = model.Sexo;
            mascota.Edad = model.Edad;
            mascota.Peso = model.Peso;

            if (model.Foto != null && model.Foto.Length > 0)
            {
                var uploadUrl = await _cloudinaryService.UploadFileAsync(model.Foto, "mascotas");
                if (uploadUrl != null)
                {
                    mascota.FotoUrl = uploadUrl;
                }
            }

            await _context.SaveChangesAsync();

            TempData["Success"] = "Mascota actualizada exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var mascota = await _context.Mascotas.FindAsync(id);
            if (mascota != null)
            {
                if (!string.IsNullOrEmpty(mascota.FotoUrl))
                {
                    await _cloudinaryService.DeleteFileAsync(mascota.FotoUrl);
                }
                _context.Mascotas.Remove(mascota);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Mascota eliminada.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
