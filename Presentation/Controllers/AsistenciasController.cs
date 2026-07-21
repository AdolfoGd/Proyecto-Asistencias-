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

        //construir con clean arquitecture los sig procesos la entidad mascotas minimo 5 propiedades due;o minimo 5 propiedades
        //medicamento min 5 prop crear controller de listar e insertar vista de ambos la base de datos se llama sp listar mascotas
        // con el nombre de igual manero en las clases mascotasnombre
    }
}
