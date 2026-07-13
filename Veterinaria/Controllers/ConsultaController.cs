using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Veterinaria.Data;
using Veterinaria.Models;
using Veterinaria.Services;
using Veterinaria.ViewModels.Consulta;

namespace Veterinaria.Controllers
{
    [Authorize]
    public class ConsultaController : Controller
    {
        private readonly VeterinariaDbContext _context;
        private readonly ICloudinaryService _cloudinaryService;

        public ConsultaController(VeterinariaDbContext context, ICloudinaryService cloudinaryService)
        {
            _context = context;
            _cloudinaryService = cloudinaryService;
        }

        [HttpGet]
        public IActionResult Create(int idMascota)
        {
            var vm = new ConsultaCreateVM
            {
                IdMascota = idMascota
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConsultaCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var consulta = new Consulta
            {
                IdMascota = model.IdMascota,
                Fecha = model.Fecha,
                Motivo = model.Motivo,
                Sintomas = model.Sintomas,
                Diagnostico = model.Diagnostico,
                Tratamiento = model.Tratamiento,
                FechaProximaCita = model.FechaProximaCita
            };

            if (model.Adjuntos != null && model.Adjuntos.Count > 0)
            {
                consulta.AdjuntosUrls = new List<string>();
                foreach (var file in model.Adjuntos)
                {
                    var uploadUrl = await _cloudinaryService.UploadFileAsync(file, "consultas");
                    if (uploadUrl != null)
                    {
                        consulta.AdjuntosUrls.Add(uploadUrl);
                    }
                }
            }

            _context.Consultas.Add(consulta);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Consulta registrada exitosamente.";
            return RedirectToAction("Details", "Mascota", new { id = model.IdMascota });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta == null) return NotFound();

            var vm = new ConsultaEditVM
            {
                IdConsulta = consulta.IdConsulta,
                IdMascota = consulta.IdMascota,
                Fecha = consulta.Fecha,
                Motivo = consulta.Motivo,
                Sintomas = consulta.Sintomas,
                Diagnostico = consulta.Diagnostico,
                Tratamiento = consulta.Tratamiento,
                FechaProximaCita = consulta.FechaProximaCita,
                AdjuntosUrls = consulta.AdjuntosUrls
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ConsultaEditVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var consulta = await _context.Consultas.FindAsync(model.IdConsulta);
            if (consulta == null) return NotFound();

            consulta.Fecha = model.Fecha;
            consulta.Motivo = model.Motivo;
            consulta.Sintomas = model.Sintomas;
            consulta.Diagnostico = model.Diagnostico;
            consulta.Tratamiento = model.Tratamiento;
            consulta.FechaProximaCita = model.FechaProximaCita;

            if (model.Adjuntos != null && model.Adjuntos.Count > 0)
            {
                if (consulta.AdjuntosUrls == null) consulta.AdjuntosUrls = new List<string>();
                
                foreach (var file in model.Adjuntos)
                {
                    var uploadUrl = await _cloudinaryService.UploadFileAsync(file, "consultas");
                    if (uploadUrl != null)
                    {
                        consulta.AdjuntosUrls.Add(uploadUrl);
                    }
                }
            }

            await _context.SaveChangesAsync();

            TempData["Success"] = "Consulta actualizada exitosamente.";
            return RedirectToAction("Details", "Mascota", new { id = model.IdMascota });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta != null)
            {
                var idMascota = consulta.IdMascota;
                if (consulta.AdjuntosUrls != null && consulta.AdjuntosUrls.Any())
                {
                    foreach (var url in consulta.AdjuntosUrls)
                    {
                        await _cloudinaryService.DeleteFileAsync(url);
                    }
                }
                _context.Consultas.Remove(consulta);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Consulta eliminada.";
                return RedirectToAction("Details", "Mascota", new { id = idMascota });
            }
            return RedirectToAction("Index", "Mascota");
        }
    }
}
