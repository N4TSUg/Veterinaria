using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Veterinaria.Data;
using Veterinaria.Models;
using Veterinaria.ViewModels.Vacuna;

namespace Veterinaria.Controllers
{
    [Authorize]
    public class VacunaController : Controller
    {
        private readonly VeterinariaDbContext _context;

        public VacunaController(VeterinariaDbContext context)
        {
            _context = context;
        }

        // --- GESTIÓN DE CATÁLOGO DE VACUNAS ---

        public async Task<IActionResult> Index()
        {
            var vacunas = await _context.Vacunas.ToListAsync();
            return View(vacunas);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new VacunaCreateVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VacunaCreateVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var vacuna = new Vacuna
            {
                Nombre = model.Nombre,
                Tipo = model.Tipo
            };

            _context.Vacunas.Add(vacuna);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Vacuna creada exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var vacuna = await _context.Vacunas.FindAsync(id);
            if (vacuna == null) return NotFound();

            var vm = new VacunaEditVM
            {
                IdVacuna = vacuna.IdVacuna,
                Nombre = vacuna.Nombre,
                Tipo = vacuna.Tipo
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VacunaEditVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var vacuna = await _context.Vacunas.FindAsync(model.IdVacuna);
            if (vacuna == null) return NotFound();

            vacuna.Nombre = model.Nombre;
            vacuna.Tipo = model.Tipo;

            await _context.SaveChangesAsync();

            TempData["Success"] = "Vacuna actualizada exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var vacuna = await _context.Vacunas.FindAsync(id);
            if (vacuna != null)
            {
                _context.Vacunas.Remove(vacuna);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Vacuna eliminada exitosamente.";
            }
            return RedirectToAction(nameof(Index));
        }

        // --- MONITOREO DE APLICACIONES ---

        public async Task<IActionResult> Monitoreo()
        {
            var aplicaciones = await _context.VacunasAplicadas
                .Include(va => va.Mascota)
                .ThenInclude(m => m.Cliente)
                .Include(va => va.Vacuna)
                .OrderBy(va => va.FechaProximaDosis)
                .ToListAsync();

            return View(aplicaciones);
        }

        // --- APLICAR A UNA MASCOTA ---

        [HttpGet]
        public async Task<IActionResult> Aplicar(int idMascota)
        {
            var vacunas = await _context.Vacunas.ToListAsync();

            var vm = new VacunaAplicadaCreateVM
            {
                IdMascota = idMascota,
                VacunasDisponibles = vacunas.Select(v => new SelectListItem
                {
                    Value = v.IdVacuna.ToString(),
                    Text = $"{v.Nombre} ({v.Tipo})"
                })
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Aplicar(VacunaAplicadaCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                var vacunas = await _context.Vacunas.ToListAsync();
                model.VacunasDisponibles = vacunas.Select(v => new SelectListItem
                {
                    Value = v.IdVacuna.ToString(),
                    Text = $"{v.Nombre} ({v.Tipo})"
                });
                return View(model);
            }

            var vacunaAplicada = new VacunaAplicada
            {
                IdMascota = model.IdMascota,
                IdVacuna = model.IdVacuna,
                FechaAplicacion = model.FechaAplicacion,
                FechaProximaDosis = model.FechaProximaDosis
            };

            _context.VacunasAplicadas.Add(vacunaAplicada);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Vacuna registrada exitosamente.";
            return RedirectToAction("Details", "Mascota", new { id = model.IdMascota });
        }
    }
}
