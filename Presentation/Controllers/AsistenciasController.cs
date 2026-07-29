using Application.DTOs;
using Application.Service;
using Microsoft.AspNetCore.Mvc;

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
       
    }
}
