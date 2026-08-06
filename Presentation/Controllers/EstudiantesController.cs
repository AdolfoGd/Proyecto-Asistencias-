using Application.DTOs;
using Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
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
            IEnumerable<EstudianteDTO> estudiantes;

            // Si el usuario escribió un ID de sesión en el buscador
            if (!string.IsNullOrEmpty(carnet))
            {
                estudiantes = await _service.ListarEstudiantesPorCarnet(carnet);
                // Guardamos el carnet buscado para mantenerlo visible en el input
                ViewBag.CarnetBuscado = carnet;
            }
            else
            {
                // Si no hay filtro, cargamos todos los estudiantes
                estudiantes = await _service.ListarEstudiantes();
            }

            return View(estudiantes);
        }
        //metodo para insertar
        [HttpGet]
        public async Task<IActionResult> NuevoEstudiante()
        {
            return View();
        }
        [HttpPost]
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
        //metodo para editar
        [HttpGet]
        public async Task<IActionResult> Editar(string carnet)
        {
            if (string.IsNullOrEmpty(carnet))
            {
                return NotFound();
            }

            var estudiantes = await _service.ListarEstudiantesPorCarnet(carnet);

            if (estudiantes == null || !estudiantes.Any())
            {
                return NotFound();
            }

            // Toma el primer registro del departamento retornado
            var estudiante = estudiantes.FirstOrDefault();

            return View("EditarEstudiante", estudiante);
        }


        [HttpPost]
        public async Task<IActionResult> Editar(EstudianteDTO estudiante)
        {
            try
            {
                // Pasa el objeto estudiante al servicio
                await _service.EditarEstudiante(estudiante);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al editar: " + ex.Message);

                // En caso de error, volvemos a mostrar la vista con los datos introducidos
                return View(estudiante);
            }
        }

        //metodo para eliminar
        [HttpPost]
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
