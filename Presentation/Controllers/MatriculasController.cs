using Application.DTOs;
using Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Authorize]
    public class MatriculasController : Controller
    {
        private readonly MatriculasService _service;
        public MatriculasController(MatriculasService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? idEstudiante)
        {
            // Capturamos la identidad del usuario actual
            var rol = User.FindFirst(ClaimTypes.Role)?.Value;
            var idUsuario = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            IEnumerable<MatriculasDTO> matriculas;

            if (idEstudiante.HasValue && idEstudiante.Value > 0)
            {
                matriculas = await _service.ListarMatriculasporIdEstudianteAsync(idEstudiante.Value);
                ViewBag.IdEstudianteBuscado = idEstudiante.Value;
            }
            else
            {
                matriculas = await _service.ListarMatriculasAsync();
            }

            // Filtro estricto para Estudiantes: interceptamos la lista y dejamos solo las suyas
            if (rol == "Estudiante")
            {
                // Asumo que tu DTO tiene una propiedad "IdEstudiante" (o "Carnet").
                // Lo convertimos a string para compararlo con el ID de la sesión.
                matriculas = matriculas.Where(m => m.IdEstudiante.ToString() == idUsuario).ToList();
            }

            return View("Index", matriculas);
        }

        // ==========================================
        // SECCIÓN RESTRINGIDA: SOLO EMPLEADOS (ADMIN)
        // ==========================================

        [HttpGet]
        [Authorize(Roles = "Empleado")]
        public IActionResult NuevaMatricula()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Empleado")]
        public async Task<IActionResult> NuevaMatricula(MatriculasDTO matricula)
        {
            try
            {
                if (!ModelState.IsValid) return View(matricula);
                await _service.NuevaMatricula(matricula);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(matricula);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Empleado")]
        public async Task<IActionResult> Editar(int id)
        {
            var lista = await _service.ListarMatriculasAsync();
            var matricula = lista.FirstOrDefault(m => m.IdMatricula == id);

            if (matricula == null) return NotFound();

            return View(matricula);
        }

        [HttpPost]
        [Authorize(Roles = "Empleado")]
        public async Task<IActionResult> Editar(MatriculasDTO matricula)
        {
            try
            {
                if (!ModelState.IsValid) return View(matricula);
                await _service.EditarMatricula(matricula);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al editar: " + ex.Message);
                return View(matricula);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Empleado")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _service.EliminarMatricula(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al eliminar: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}

