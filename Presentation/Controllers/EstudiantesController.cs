using Application.Interface;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Authorize]
    public class EstudiantesController : Controller
    {
        private readonly IEstudianteRepository _estudianteRepository;

        public EstudiantesController(IEstudianteRepository estudianteRepository)
        {
            _estudianteRepository = estudianteRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Estudiante,Administrador")]
        public async Task<IActionResult> Index()
        {
            var estudiantes = await _estudianteRepository.ListarEstudiantesAsync();
            return View(estudiantes);
        }

        // GET: Estudiantes/Create
        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            return View("Nuevo");
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                await _estudianteRepository.GuardarEstudianteAsync(estudiante);
                return RedirectToAction(nameof(Index));
            }
            return View("Nuevo", estudiante);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var estudiantes = await _estudianteRepository.ListarEstudiantesPorCarnetAsync(id);
            var estudiante = estudiantes.FirstOrDefault();

            if (estudiante == null)
                return NotFound();

            return View("Editar", estudiante);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                await _estudianteRepository.EditarEstudianteAsync(estudiante);
                return RedirectToAction(nameof(Index));
            }
            return View("Editar", estudiante);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            await _estudianteRepository.EliminarEstudianteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}