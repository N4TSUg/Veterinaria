using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veterinaria.Data;

namespace Veterinaria.Controllers
{
    [Authorize]
    public class CalendarioController : Controller
    {
        private readonly VeterinariaDbContext _context;

        public CalendarioController(VeterinariaDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCitas(DateTime start, DateTime end)
        {
            // FullCalendar envia 'start' y 'end' como parametros ISO string en GET
            var consultas = await _context.Consultas
                .Include(c => c.Mascota)
                .ThenInclude(m => m.Cliente)
                .Where(c => c.FechaProximaCita != null && c.FechaProximaCita >= start && c.FechaProximaCita <= end)
                .ToListAsync();

            var eventos = consultas.Select(c => new
            {
                id = c.IdConsulta,
                title = $"{c.Mascota.Nombre} - {c.Motivo}",
                start = c.FechaProximaCita?.ToString("yyyy-MM-ddTHH:mm:ss"),
                url = Url.Action("Details", "Mascota", new { id = c.IdMascota }),
                backgroundColor = "#003c90", // primary color
                borderColor = "#003c90",
                textColor = "#ffffff",
                extendedProps = new {
                    dueño = $"{c.Mascota.Cliente.Nombre} {c.Mascota.Cliente.Apellido}",
                    motivo = c.Motivo
                }
            });

            return Json(eventos);
        }
    }
}
