using Application.DTOs;
using Application.Service;
using Google.Protobuf.Collections;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Presentation.Controllers
{
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
            IEnumerable<AsistenciaDTO> asistencias;

            // Si el usuario escribió un ID de sesión en el buscador
            if (idSesion.HasValue && idSesion.Value > 0)
            {
                asistencias = await _service.ListarAsistenciasPorSesion(idSesion.Value);
                // Guardamos el ID buscado para mantenerlo visible en el input
                ViewBag.IdSesionBuscada = idSesion.Value;
            }
            else
            {
                // Si no hay filtro, cargamos todas las asistencias
                asistencias = await _service.ListarAsistencia();
            }

            return View(asistencias);
        }
        //metodo para insertar
        [HttpGet]
        public async Task<IActionResult> NuevoAsistencia()
        {
            return View();
        }
        [HttpPost]
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
        public async Task<IActionResult> Editar(int id)
        {
            var asistencia = await _service.ListarAsistenciasPorSesion(id);
            var lista = asistencia.FirstOrDefault();

            if (asistencia == null||!asistencia.Any())
            {
                return NotFound();
            }
            return View(lista);
        }

       
        [HttpPost]
        public async Task<IActionResult> Editar(int idAsistencia, string estado)
        {
            try
            {
                await _service.EditarAsistencia(idAsistencia,estado);
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
