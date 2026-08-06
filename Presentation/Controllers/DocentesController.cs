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
    }
}
