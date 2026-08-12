using Application.DTOs;
using Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Authorize]
    public class DocentesController : Controller
    {
        private readonly DocentesServices _service;
        public DocentesController(DocentesServices service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string departamento)
        {
            IEnumerable<DocentesDTO> docentes;

            // Si el usuario escribió un departamento en el buscador
            if (departamento != null && !string.IsNullOrEmpty(departamento))
            {
                docentes = await _service.ListarDocentesPorDepartamento(departamento);
                ViewBag.DepartamentoBuscado = departamento; // Sugerencia: guardarlo en el ViewBag para el input
            }
            else
            {
                // Si no hay filtro, cargamos todos los docentes
                docentes = await _service.ListarDocentes();
            }

            return View("Index", docentes);
        }

        // ==========================================
        // SECCIÓN RESTRINGIDA: SOLO EMPLEADOS (ADMIN)
        // ==========================================

        [HttpGet]
        [Authorize(Roles = "Empleado")]
        public async Task<IActionResult> NuevoDocente()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Empleado")]
        public async Task<IActionResult> NuevoDocente(DocentesDTO docente)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(docente);
                }
                await _service.NuevoDocente(docente);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(docente);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Empleado")]
        public async Task<IActionResult> Editar(string departamento)
        {
            if (string.IsNullOrEmpty(departamento))
            {
                return NotFound();
            }

            var docentes = await _service.ListarDocentesPorDepartamento(departamento);

            if (docentes == null || !docentes.Any())
            {
                return NotFound();
            }

            // Toma el primer registro del departamento retornado
            var docente = docentes.FirstOrDefault();

            return View("Editar", docente);
        }

        [HttpPost]
        [Authorize(Roles = "Empleado")]
        public async Task<IActionResult> Editar(DocentesDTO docente)
        {
            try
            {
                // Pasa el objeto docente al servicio
                await _service.EditarDocente(docente);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al editar: " + ex.Message);

                // En caso de error, volvemos a mostrar la vista con los datos introducidos
                return View(docente);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Empleado")]
        public async Task<IActionResult> Eliminar(string carnet)
        {
            try
            {
                await _service.EliminarDocente(carnet);
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
