using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Veterinaria.Data;
using Veterinaria.Models;
using Veterinaria.ViewModels.Access;

namespace Veterinaria.Controllers
{
    public class AccessController : Controller
    {
        private readonly VeterinariaDbContext _context;

        public AccessController(VeterinariaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.EmailUser == model.Correo);

            if (usuario == null)
            {
                ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos.");
                return View(model);
            }

            var hasher = new PasswordHasher<Usuario>();
            var result = hasher.VerifyHashedPassword(usuario, usuario.PasswordUser, model.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos.");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.NameUser),
                new Claim(ClaimTypes.Email, usuario.EmailUser),
                new Claim(ClaimTypes.Role, usuario.Rol.NameRol),
                new Claim("IdUser", usuario.IdUser.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }
    }
}
