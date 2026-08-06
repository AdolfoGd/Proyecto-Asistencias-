using Application.DTOs;
using Application.Service;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Presentation.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly EmpleadoService _service;
        public EmpleadosController(EmpleadoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? departamento)
        {
            IEnumerable<EmpleadoDTO> empleados;
            if (!string.IsNullOrWhiteSpace(departamento))
            {
                empleados = await _service.ListarEmpleadosPorDepartamento(departamento);
                ViewBag.DepartamentoBuscado = departamento;
            }
            else
            {
                empleados = await _service.ListarEmpleados();
            }
            return View(empleados);
        }

        [HttpGet]
        public IActionResult NuevoEmpleado()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NuevoEmpleado(EmpleadoDTO empleado)
        {
            try
            {
                if (!ModelState.IsValid) return View(empleado);
                await _service.NuevoEmpleado(empleado);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(empleado);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Editar(string id)
        {
            var lista = await _service.ListarEmpleados();
            var empleado = lista.FirstOrDefault(e => e.CodigoEmpleado == id);
            if (empleado == null) return NotFound();
            return View(empleado);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(EmpleadoDTO empleado)
        {
            try
            {
                if (!ModelState.IsValid) return View(empleado);
                await _service.EditarEmpleado(empleado);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al editar: " + ex.Message);
                return View(empleado);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(string id)
        {
            try
            {
                await _service.EliminarEmpleado(id);
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