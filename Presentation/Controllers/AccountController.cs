using Application.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System.Security.Claims;

namespace Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IEstudianteRepository _estudianteRepository;
        public AccountController(IEstudianteRepository estudianteRepository)
        {
            _estudianteRepository = estudianteRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Carnet == "steven" && model.Contrasena == "admin123")
            {
                var claimsAdmin = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, "ADMIN01"),
                    new Claim(ClaimTypes.Name, "Administrador del Sistema"),
                    new Claim(ClaimTypes.Role, "Administrador"),
                    new Claim("Carnet", "ADMIN01")
                };

                var identityAdmin = new ClaimsIdentity(claimsAdmin, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identityAdmin));

                return RedirectToAction("Index", "Home");
            }

            var estudiantes = await _estudianteRepository.ListarEstudiantesPorCarnetAsync(model.Carnet);
            var estudiante = estudiantes.FirstOrDefault();

            if (estudiante != null && estudiante.Contraseña == model.Contrasena)
            {
                string nombreCompleto = $"{estudiante.PrimerNombre} {estudiante.PrimerApellido}".Trim();

                var claimsEstudiante = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, estudiante.Carnet),
                    new Claim(ClaimTypes.Name, nombreCompleto),
                    new Claim(ClaimTypes.Role, "Estudiante"),
                    new Claim("Carnet", estudiante.Carnet)
                };

                var identityEstudiante = new ClaimsIdentity(claimsEstudiante, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identityEstudiante));

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Carnet/Usuario o contraseña incorrectos.";
            return View(model);
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}