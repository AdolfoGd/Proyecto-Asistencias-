using Application.DTOs;
using Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers
{

    [Authorize] // Bloquea todo el controlador si no hay sesión iniciada
    public class EstudiantesController : Controller
    {
        private readonly EstudianteService _service;

        public EstudiantesController(EstudianteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string carnet)
        {
            // 1. Identificamos quién es el usuario actual
            var rol = User.FindFirst(ClaimTypes.Role)?.Value;
            var idUsuario = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            IEnumerable<EstudianteDTO> estudiantes;

            // 2. Si es Estudiante, ignoramos la barra de búsqueda y mostramos solo el suyo
            if (rol == "Estudiante")
            {
                estudiantes = await _service.ListarEstudiantesPorCarnet(idUsuario);
                ViewBag.CarnetBuscado = idUsuario;
            }
            else
            {
                // 3. Lógica normal para Docentes y Empleados (búsqueda o lista completa)
                if (!string.IsNullOrEmpty(carnet))
                {
                    estudiantes = await _service.ListarEstudiantesPorCarnet(carnet);
                    ViewBag.CarnetBuscado = carnet;
                }
                else
                {
                    estudiantes = await _service.ListarEstudiantes();
                }
            }

            return View(estudiantes);
        }

        // ==========================================
        // SECCIÓN RESTRINGIDA: SOLO EMPLEADOS (ADMIN)
        // ==========================================

        [HttpGet]
        [Authorize(Roles = "Empleado")] // Bloquea el acceso a Estudiantes y Docentes
        public async Task<IActionResult> NuevoEstudiante()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Empleado")]
        public async Task<IActionResult> NuevoEstudiante(EstudianteDTO estudiante)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(estudiante);
                }
                await _service.NuevoEstudiante(estudiante);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(estudiante);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Empleado")]
        public async Task<IActionResult> Editar(int carnet)
        {
            if (carnet <= 0)
            {
                return NotFound();
            }

            var estudiantes = await _service.ListarEstudiantesPorCarnet(carnet.ToString());

            if (estudiantes == null || !estudiantes.Any())
            {
                return NotFound();
            }

            var estudiante = estudiantes.FirstOrDefault();
            return View("EditarEstudiante", estudiante);
        }

        [HttpPost]
        [Authorize(Roles = "Empleado")]
        public async Task<IActionResult> Editar(EstudianteDTO estudiante)
        {
            try
            {
                await _service.EditarEstudiante(estudiante);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al editar: " + ex.Message);
                return View("EditarEstudiante", estudiante);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Empleado")]
        public async Task<IActionResult> Eliminar(int carnet)
        {
            try
            {
                await _service.EliminarEstudiante(carnet);
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
