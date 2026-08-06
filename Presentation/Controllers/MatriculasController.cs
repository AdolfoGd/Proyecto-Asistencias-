using Application.DTOs;
using Application.Service;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Presentation.Controllers
{
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
            return View(matriculas);
        }

        [HttpGet]
        public IActionResult NuevaMatricula()
        {
            return View();
        }

        [HttpPost]
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
        public async Task<IActionResult> Editar(int id)
        {
            var lista = await _service.ListarMatriculasAsync();
            var matricula = lista.FirstOrDefault(m => m.IdMatricula == id);
            if (matricula == null) return NotFound();
            return View(matricula);
        }

        [HttpPost]
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

