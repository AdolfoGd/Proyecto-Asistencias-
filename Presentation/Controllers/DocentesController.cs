using Application.DTOs;
using Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
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

            // Si el usuario escribió un ID de sesión en el buscador
            if (departamento != null && !string.IsNullOrEmpty(departamento))
            {
                docentes = await _service.ListarDocentesPorDepartamento(departamento);
            }
            else
            {
                // Si no hay filtro, cargamos todos los docentes
                docentes = await _service.ListarDocentes();
            }

            return View(docentes);
        }
        //metodo para insertar
        [HttpGet]
        public async Task<IActionResult> NuevoDocente()
        {
            return View();
        }
        [HttpPost]
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
        //metodo para editar
        [HttpGet]
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
        //metodo para eliminar
        [HttpPost]
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
