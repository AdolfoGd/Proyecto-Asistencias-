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
        public async Task<IActionResult> Index()
        {
            var asistencias = await _service.ListarAsistencia();
            return View(asistencias);
        }   
    }
}
