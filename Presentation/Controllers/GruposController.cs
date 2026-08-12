using Application.DTOs;
using Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Authorize]
    public class GruposController : Controller
    {
        private readonly GrupoService _service;
        public GruposController(GrupoService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string codigoGrupo)
        {
            IEnumerable<GruposDTO> grupos;

            // Si el usuario escribió un código de grupo en el buscador
            if (!string.IsNullOrEmpty(codigoGrupo))
            {
                grupos = await _service.ListarGruposPorCodigo(codigoGrupo);
                // Guardamos el código buscado para mantenerlo visible en el input
                ViewBag.CodigoGrupoBuscado = codigoGrupo;
            }
            else
            {
                // Si no hay filtro, cargamos todos los grupos
                grupos = await _service.ListarGrupoAsync();
            }

            // Los estudiantes, docentes y empleados pueden ver esto
            return View(grupos);
        }

        // ==========================================
        // SECCIÓN RESTRINGIDA: EMPLEADOS Y DOCENTES
        // ==========================================

        [HttpGet]
        [Authorize(Roles = "Empleado,Docente")]
        public async Task<IActionResult> NuevoGrupo()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Empleado,Docente")]
        public async Task<IActionResult> NuevoGrupo(GruposDTO grupo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(grupo);
                }
                await _service.NuevoGrupo(grupo);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(grupo);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Empleado,Docente")]
        public async Task<IActionResult> Editar(string codigoGrupo)
        {
            if (string.IsNullOrEmpty(codigoGrupo))
            {
                return NotFound();
            }

            var grupos = await _service.ListarGruposPorCodigo(codigoGrupo);

            if (grupos == null || !grupos.Any())
            {
                return NotFound();
            }

            var grupo = grupos.FirstOrDefault();

            return View(grupo); // Retornamos la vista con el modelo del grupo
        }

        [HttpPost]
        [Authorize(Roles = "Empleado,Docente")]
        public async Task<IActionResult> Editar(GruposDTO grupo)
        {
            try
            {
                await _service.EditarGrupo(grupo);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al editar: " + ex.Message);
                return View(grupo);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Empleado,Docente")]
        public async Task<IActionResult> Eliminar(string codigoGrupo)
        {
            try
            {
                await _service.EliminarGrupo(codigoGrupo);
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
