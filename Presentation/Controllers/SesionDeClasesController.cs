using Application.DTOs;
using Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Presentation.Controllers
{
    [Authorize]
    public class SesionDeClaseController : Controller
    {
        private readonly SesionDeClaseService _service;
        public SesionDeClaseController(SesionDeClaseService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? idGrupo)
        {
            IEnumerable<Sesion_de_ClaseDTO> sesiones;

            if (!string.IsNullOrWhiteSpace(idGrupo))
            {
                sesiones = await _service.ListarSesionDeClasePorIdGrupoAsync(idGrupo);
                ViewBag.IdGrupoBuscado = idGrupo;
            }
            else
            {
                sesiones = await _service.ListarSesionDeClaseAsync();
            }

            return View(sesiones);
        }

        // ==========================================
        // SECCIÓN RESTRINGIDA: EMPLEADOS Y DOCENTES
        // ==========================================

        [HttpGet]
        [Authorize(Roles = "Empleado,Docente")]
        public IActionResult NuevaSesion()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Empleado,Docente")]
        public async Task<IActionResult> NuevaSesion(Sesion_de_ClaseDTO sesion)
        {
            try
            {
                if (!ModelState.IsValid) return View(sesion);
                await _service.NuevaSesion(sesion);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(sesion);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Empleado,Docente")]
        public async Task<IActionResult> Editar(int id)
        {
            var lista = await _service.ListarSesionDeClaseAsync();
            var sesion = lista.FirstOrDefault(s => s.IdSesion == id);

            if (sesion == null) return NotFound();

            return View(sesion);
        }

        [HttpPost]
        [Authorize(Roles = "Empleado,Docente")]
        public async Task<IActionResult> Editar(Sesion_de_ClaseDTO sesion)
        {
            try
            {
                if (!ModelState.IsValid) return View(sesion);
                await _service.EditarSesion(sesion);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al editar: " + ex.Message);
                return View(sesion);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Empleado,Docente")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _service.EliminarSesion(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("foreign key constraint") || ex.Message.Contains("a parent row"))
                    TempData["Error"] = "No se puede eliminar esta sesión porque ya tiene asistencias registradas.";
                else
                    TempData["Error"] = "Error al eliminar: " + ex.Message;

                return RedirectToAction(nameof(Index));
            }
        }
    }
}