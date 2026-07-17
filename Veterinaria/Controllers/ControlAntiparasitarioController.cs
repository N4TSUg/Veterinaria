using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Veterinaria.Data;
using Veterinaria.Models;
using Veterinaria.ViewModels.ControlAntiparasitario;

namespace Veterinaria.Controllers
{
    [Authorize]
    public class ControlAntiparasitarioController : Controller
    {
        private readonly VeterinariaDbContext _context;

        public ControlAntiparasitarioController(VeterinariaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create(int idMascota)
        {
            var vm = new ControlAntiparasitarioCreateVM
            {
                IdMascota = idMascota
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ControlAntiparasitarioCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var control = new ControlAntiparasitario
            {
                IdMascota = model.IdMascota,
                Fecha = model.Fecha,
                Peso = model.Peso,
                Medicamento = model.Medicamento,
                Dosis = model.Dosis
            };

            _context.ControlesAntiparasitarios.Add(control);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Control antiparasitario registrado exitosamente.";
            return RedirectToAction("Details", "Mascota", new { id = model.IdMascota });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var control = await _context.ControlesAntiparasitarios.FindAsync(id);
            if (control == null) return NotFound();

            var vm = new ControlAntiparasitarioEditVM
            {
                IdControlAntiparasitario = control.IdControlAntiparasitario,
                IdMascota = control.IdMascota,
                Fecha = control.Fecha,
                Peso = control.Peso,
                Medicamento = control.Medicamento,
                Dosis = control.Dosis
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ControlAntiparasitarioEditVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var control = await _context.ControlesAntiparasitarios.FindAsync(model.IdControlAntiparasitario);
            if (control == null) return NotFound();

            control.Fecha = model.Fecha;
            control.Peso = model.Peso;
            control.Medicamento = model.Medicamento;
            control.Dosis = model.Dosis;

            await _context.SaveChangesAsync();

            TempData["Success"] = "Control antiparasitario actualizado exitosamente.";
            return RedirectToAction("Details", "Mascota", new { id = model.IdMascota });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var control = await _context.ControlesAntiparasitarios.FindAsync(id);
            if (control != null)
            {
                var idMascota = control.IdMascota;
                _context.ControlesAntiparasitarios.Remove(control);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Control antiparasitario eliminado.";
                return RedirectToAction("Details", "Mascota", new { id = idMascota });
            }
            return RedirectToAction("Index", "Mascota");
        }
    }
}
