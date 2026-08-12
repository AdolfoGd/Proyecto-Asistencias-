using Application.DTOs;
using Application.Service;
using Google.Protobuf.Collections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Authorize]
    public class AsistenciasController : Controller
    {
        private readonly AsistenciaService _service;
        public AsistenciasController(AsistenciaService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int? idSesion)
        {
            // Capturamos la identidad del usuario actual
            var rol = User.FindFirst(ClaimTypes.Role)?.Value;
            var idUsuario = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            IEnumerable<AsistenciaDTO> asistencias;

            // 1. Obtenemos las asistencias base (con o sin filtro de sesión)
            if (idSesion.HasValue && idSesion.Value > 0)
            {
                asistencias = await _service.ListarAsistenciasPorSesion(idSesion.Value);
                ViewBag.IdSesionBuscada = idSesion.Value;
            }
            else
            {
                asistencias = await _service.ListarAsistencia();
            }

            // 2. Filtro estricto para Estudiantes: interceptamos la lista y dejamos solo las suyas
            if (rol == "Estudiante")
            {
                // Convertimos el Carnet (int) a texto agregando .ToString() para compararlo con idUsuario (string)
                asistencias = asistencias.Where(a => a.Carnet.ToString() == idUsuario).ToList();
            }

            return View("Index", asistencias);
        }

        // ==========================================
        // SECCIÓN RESTRINGIDA: EMPLEADOS Y DOCENTES
        // ==========================================

        [HttpGet]
        [Authorize(Roles = "Empleado,Docente")]
        public async Task<IActionResult> NuevoAsistencia()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Empleado,Docente")]
        public async Task<IActionResult> NuevoAsistencia(AsistenciaDTO asistencia)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(asistencia);
                }
                await _service.NuevaAsistencia(asistencia);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(asistencia);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Empleado,Docente")]
        public async Task<IActionResult> Editar(int id)
        {
            var asistencia = await _service.ListarAsistenciasPorSesion(id);
            var lista = asistencia.FirstOrDefault();

            if (asistencia == null || !asistencia.Any())
            {
                return NotFound();
            }
            return View("Editar", lista);
        }

        [HttpPost]
        [Authorize(Roles = "Empleado,Docente")]
        public async Task<IActionResult> Editar(int idAsistencia, string estado)
        {
            try
            {
                await _service.EditarAsistencia(idAsistencia, estado);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al editar: " + ex.Message);
                var asistencia = await _service.ListarAsistenciasPorSesion(idAsistencia);
                var lista = asistencia.FirstOrDefault();
                return View(lista);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Empleado,Docente")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _service.EliminarAsistencia(id);
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
