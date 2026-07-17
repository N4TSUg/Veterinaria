using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veterinaria.Data;
using Veterinaria.ViewModels.Dashboard;

namespace Veterinaria.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly VeterinariaDbContext _context;

        public HomeController(VeterinariaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var ahora = DateTime.UtcNow.AddHours(-5);
            var hoy = ahora.Date;
            var inicioMes = new DateTime(hoy.Year, hoy.Month, 1);
            var finMes = inicioMes.AddMonths(1).AddDays(-1);

            var proximasCitasList = await _context.Consultas
                .Include(c => c.Mascota)
                .ThenInclude(m => m.Cliente)
                .Where(c => c.FechaProximaCita != null && c.FechaProximaCita >= ahora)
                .OrderBy(c => c.FechaProximaCita)
                .Take(5)
                .ToListAsync();

            var vm = new DashboardVM
            {
                TotalClientes = await _context.Clientes.CountAsync(),
                TotalMascotas = await _context.Mascotas.CountAsync(),
                ConsultasMes = await _context.Consultas.CountAsync(c => c.Fecha >= inicioMes && c.Fecha <= finMes),
                ProximasCitas = await _context.Consultas.CountAsync(c => c.FechaProximaCita != null && c.FechaProximaCita >= ahora),
                VacunasPorVencer = await _context.VacunasAplicadas.CountAsync(v => v.FechaProximaDosis >= hoy && v.FechaProximaDosis <= hoy.AddDays(7)),
                ListaProximasCitas = proximasCitasList
            };

            return View(vm);
        }
    }
}
