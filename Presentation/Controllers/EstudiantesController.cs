using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Presentation.Controllers
{
    public class EstudiantesController : Controller
    {
        private static List<EstudianteDTO> _estudiantesSimulados = new List<EstudianteDTO>();

        [HttpGet]
        public IActionResult Index()
        {
            return View(_estudiantesSimulados);
        }

        [HttpGet]
        public IActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Nuevo(EstudianteDTO nuevoEstudiante)
        {
            ModelState.Clear();
            _estudiantesSimulados.Add(nuevoEstudiante);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Editar(string id)
        {
            var estudiante = _estudiantesSimulados.FirstOrDefault(e => e.Carnet == id);
            if (estudiante == null) return RedirectToAction(nameof(Index));

            return View(estudiante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(EstudianteDTO estudianteEditado)
        {
            ModelState.Clear();

            var existente = _estudiantesSimulados.FirstOrDefault(e => e.Carnet == estudianteEditado.Carnet);
            if (existente != null)
            {
                existente.Contraseña = estudianteEditado.Contraseña;
                existente.PrimerNombre = estudianteEditado.PrimerNombre;
                existente.SegundoNombre = estudianteEditado.SegundoNombre;
                existente.PrimerApellido = estudianteEditado.PrimerApellido;
                existente.SegundoApellido = estudianteEditado.SegundoApellido;
                existente.Carrera = estudianteEditado.Carrera;
                existente.AñoIngreso = estudianteEditado.AñoIngreso;
                existente.CantidadFaltas = estudianteEditado.CantidadFaltas;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(string id)
        {
            var estudiante = _estudiantesSimulados.FirstOrDefault(e => e.Carnet == id);
            if (estudiante != null)
            {
                _estudiantesSimulados.Remove(estudiante);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}