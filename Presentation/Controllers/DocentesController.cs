using Application.Interface;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Authorize]
    public class DocentesController : Controller
    {
        private readonly IDocentesRepository _docenteRepository;

        public DocentesController(IDocentesRepository docenteRepository)
        {
            _docenteRepository = docenteRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Estudiante,Administrador")]
        public async Task<IActionResult> Index()
        {
            var docentes = await _docenteRepository.ListarDocentesAsync();
            return View(docentes);
        }

        // ACCIONES EXCLUSIVAS PARA ADMINISTRADOR

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            return View("Nuevo");
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Docentes docente)
        {
            if (ModelState.IsValid)
            {
                await _docenteRepository.GuardarDocentesAsync(docente);
                return RedirectToAction(nameof(Index));
            }

            return View("Nuevo", docente);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var docentes = await _docenteRepository.ListarDocentesAsync();
            var docente = docentes.FirstOrDefault(d => d.Carnet == id);

            if (docente == null)
                return NotFound();

            return View("Editar", docente);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Docentes docente)
        {
            if (ModelState.IsValid)
            {
                await _docenteRepository.EditarDocentesAsync(docente);
                return RedirectToAction(nameof(Index));
            }
            return View("Editar", docente);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            if (int.TryParse(id, out int docenteId))
            {
                await _docenteRepository.EliminarDocentesAsync(docenteId);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}