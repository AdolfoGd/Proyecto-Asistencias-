using Application.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IEstudianteRepository _estudianteRepo;
        private readonly IDocentesRepository _docenteRepo;
        private readonly IEmpleadoRepository _empleadoRepo;

        public AccountController(
            IEstudianteRepository estudianteRepo,
            IDocentesRepository docenteRepo,
            IEmpleadoRepository empleadoRepo)
        {
            _estudianteRepo = estudianteRepo;
            _docenteRepo = docenteRepo;
            _empleadoRepo = empleadoRepo;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string usuario, string password)
        {
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Debe ingresar su usuario y contraseña.";
                return View();
            }

            string rolDelUsuario = "";
            string idUsuario = "";
            string nombreUsuario = "";

            // 1. Intentar validar como Estudiante
            var estudiante = await _estudianteRepo.ValidarCredencialesAsync(usuario, password);
            if (estudiante != null)
            {
                rolDelUsuario = "Estudiante";
                idUsuario = estudiante.Carnet;
                nombreUsuario = $"{estudiante.PrimerNombre} {estudiante.PrimerApellido}";
            }
            else
            {
                // 2. Intentar validar como Docente
                var docente = await _docenteRepo.ValidarCredencialesAsync(usuario, password);
                if (docente != null)
                {
                    rolDelUsuario = "Docente";
                    idUsuario = docente.Carnet;
                    nombreUsuario = $"{docente.PrimerNombre} {docente.PrimerApellido}";
                }
                else
                {
                    // 3. Intentar validar como Empleado
                    var empleado = await _empleadoRepo.ValidarCredencialesAsync(usuario, password);
                    if (empleado != null)
                    {
                        rolDelUsuario = "Empleado";
                        idUsuario = empleado.CodigoEmpleado;
                        nombreUsuario = $"{empleado.PrimerNombre} {empleado.PrimerApellido}";
                    }
                }
            }

            // Si no coincidió con ninguna tabla
            if (string.IsNullOrEmpty(rolDelUsuario))
            {
                ViewBag.Error = "Credenciales incorrectas. Verifique sus datos.";
                return View();
            }

            // Crear la Cookie de Sesión con los Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, nombreUsuario),
                new Claim(ClaimTypes.NameIdentifier, idUsuario), // Clave para filtrar datos del usuario
                new Claim(ClaimTypes.Role, rolDelUsuario)         // Clave para los [Authorize(Roles="...")]
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult AccesoDenegado()
        {
            return View();
        }
    }
}
