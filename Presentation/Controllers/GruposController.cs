using Application.DTOs;
using Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Authorize]
    public class GruposController : Controller
    {
        private readonly GrupoService _grupoService;

        public GruposController(GrupoService grupoService)
        {
            _grupoService = grupoService;
        }

        [HttpGet]
        [Authorize(Roles = "Estudiante,Administrador")]
        public async Task<IActionResult> Index()
        {
            var grupos = await _grupoService.ListarGrupoAsync();
            return View(grupos);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            return View("Nuevo");
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GruposDTO grupo)
        {
            if (ModelState.IsValid)
            {
                await _grupoService.NuevoGrupo(grupo);
                return RedirectToAction(nameof(Index));
            }
            return View("Nuevo", grupo);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var grupos = await _grupoService.ListarEmpleadosPorCodigo(id);
            var grupo = grupos.FirstOrDefault();

            if (grupo == null)
                return NotFound();

            return View("Editar", grupo);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GruposDTO grupo)
        {
            if (ModelState.IsValid)
            {
                await _grupoService.EditarGrupo(grupo);
                return RedirectToAction(nameof(Index));
            }
            return View("Editar", grupo);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            await _grupoService.EliminarGrupo(id);
            return RedirectToAction(nameof(Index));
        }
    }
}