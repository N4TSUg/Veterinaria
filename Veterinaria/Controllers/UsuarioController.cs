using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Veterinaria.Data;
using Veterinaria.Models;
using Veterinaria.ViewModels.Usuario;

namespace Veterinaria.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UsuarioController : Controller
    {
        private readonly VeterinariaDbContext _context;

        public UsuarioController(VeterinariaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = await _context.Usuarios.Include(u => u.Rol).ToListAsync();
            return View(usuarios);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(_context.Roles, "IdRol", "NameRol");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = new SelectList(_context.Roles, "IdRol", "NameRol", model.IdRol);
                return View(model);
            }

            if (await _context.Usuarios.AnyAsync(u => u.EmailUser == model.EmailUser))
            {
                ModelState.AddModelError("EmailUser", "El correo ya está registrado.");
                ViewBag.Roles = new SelectList(_context.Roles, "IdRol", "NameRol", model.IdRol);
                return View(model);
            }

            var hasher = new PasswordHasher<Usuario>();
            var usuario = new Usuario
            {
                NameUser = model.NameUser,
                EmailUser = model.EmailUser,
                IdRol = model.IdRol
            };
            usuario.PasswordUser = hasher.HashPassword(usuario, model.PasswordUser);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Usuario creado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();

            var vm = new UsuarioEditVM
            {
                IdUser = usuario.IdUser,
                NameUser = usuario.NameUser,
                EmailUser = usuario.EmailUser,
                IdRol = usuario.IdRol
            };

            ViewBag.Roles = new SelectList(_context.Roles, "IdRol", "NameRol", usuario.IdRol);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UsuarioEditVM model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = new SelectList(_context.Roles, "IdRol", "NameRol", model.IdRol);
                return View(model);
            }

            var usuario = await _context.Usuarios.FindAsync(model.IdUser);
            if (usuario == null) return NotFound();

            if (await _context.Usuarios.AnyAsync(u => u.EmailUser == model.EmailUser && u.IdUser != model.IdUser))
            {
                ModelState.AddModelError("EmailUser", "El correo ya está registrado por otro usuario.");
                ViewBag.Roles = new SelectList(_context.Roles, "IdRol", "NameRol", model.IdRol);
                return View(model);
            }

            usuario.NameUser = model.NameUser;
            usuario.EmailUser = model.EmailUser;
            usuario.IdRol = model.IdRol;

            if (!string.IsNullOrEmpty(model.PasswordUser))
            {
                var hasher = new PasswordHasher<Usuario>();
                usuario.PasswordUser = hasher.HashPassword(usuario, model.PasswordUser);
            }

            await _context.SaveChangesAsync();

            TempData["Success"] = "Usuario actualizado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                // Evitar que el administrador se elimine a sí mismo (opcional, pero buena práctica)
                var currentUserId = User.FindFirst("IdUser")?.Value;
                if (currentUserId == id.ToString())
                {
                    TempData["Error"] = "No puedes eliminar tu propia cuenta.";
                    return RedirectToAction(nameof(Index));
                }

                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Usuario eliminado.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
