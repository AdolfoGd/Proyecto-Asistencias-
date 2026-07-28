using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Presentation.Controllers
{
    public class DocentesController : Controller
    {
        private static List<DocentesDTO> _docentesSimulados = new List<DocentesDTO>();

        [HttpGet]
        public IActionResult Index()
        {
            return View(_docentesSimulados);
        }
        [HttpGet]
        public IActionResult Nuevo()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Nuevo(DocentesDTO nuevoDocente)
        {
            ModelState.Clear();
            nuevoDocente.CantidadFaltas = 0;
            _docentesSimulados.Add(nuevoDocente);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Editar(string id)
        {
            var docente = _docentesSimulados.FirstOrDefault(d => d.Carnet == id);

            if (docente == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(docente);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(DocentesDTO docenteEditado)
        {
            ModelState.Clear();

            var existente = _docentesSimulados.FirstOrDefault(d => d.Carnet == docenteEditado.Carnet);
            if (existente != null)
            {
                existente.CodigoEmpleado = docenteEditado.CodigoEmpleado;
                existente.PrimerNombre = docenteEditado.PrimerNombre;
                existente.PrimerApellido = docenteEditado.PrimerApellido;
                existente.Departamento = docenteEditado.Departamento;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(string id)
        {
            var docente = _docentesSimulados.FirstOrDefault(d => d.Carnet == id);
            if (docente != null)
            {
                _docentesSimulados.Remove(docente);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}