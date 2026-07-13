using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veterinaria.Data;

namespace Veterinaria.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private readonly VeterinariaDbContext _context;

        public SearchController(VeterinariaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Buscar(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
            {
                return Json(new List<object>());
            }

            var query = q.ToLower().Trim();

            // Buscar Clientes
            var clientes = await _context.Clientes
                .Where(c => c.Nombre.ToLower().Contains(query) || 
                            c.Apellido.ToLower().Contains(query) || 
                            (c.Correo != null && c.Correo.ToLower().Contains(query)) || 
                            c.Telefono.Contains(query))
                .Take(5)
                .Select(c => new
                {
                    id = c.IdCliente,
                    type = "cliente",
                    title = $"{c.Nombre} {c.Apellido}",
                    subtitle = c.Correo,
                    url = Url.Action("Details", "Cliente", new { id = c.IdCliente }),
                    icon = "person"
                })
                .ToListAsync();

            // Buscar Mascotas
            var mascotas = await _context.Mascotas
                .Include(m => m.Cliente)
                .Where(m => m.Nombre.ToLower().Contains(query) || 
                            m.Raza.ToLower().Contains(query) || 
                            m.Especie.ToLower().Contains(query))
                .Take(5)
                .Select(m => new
                {
                    id = m.IdMascota,
                    type = "mascota",
                    title = m.Nombre,
                    subtitle = $"{m.Especie} ({m.Raza}) - Dueño: {m.Cliente.Nombre}",
                    url = Url.Action("Details", "Mascota", new { id = m.IdMascota }),
                    icon = "pets"
                })
                .ToListAsync();

            var resultados = clientes.Cast<object>().Concat(mascotas.Cast<object>()).ToList();

            return Json(resultados);
        }
    }
}
