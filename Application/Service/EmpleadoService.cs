using Application.DTOs;
using Application.Interface;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Service
{
    public class EmpleadoService
    {
        private readonly IEmpleadoRepository _empleadoRepository;

        public EmpleadoService(IEmpleadoRepository empleadoRepository)
        {
            _empleadoRepository = empleadoRepository;
        }
        public async Task<IEnumerable<EmpleadoDTO>> ListarEmpleados()
        {
            var lista = await _empleadoRepository.ListarEmpleadosAsync();
            return lista.Select(e => new EmpleadoDTO
            {
                CodigoEmpleado = e.CodigoEmpleado,
                Contraseña = e.Contraseña,
                PrimerNombre = e.PrimerNombre,
                SegundoNombre = e.SegundoNombre,
                PrimerApellido = e.PrimerApellido,
                SegundoApellido = e.SegundoApellido,
                Departamento = e.Departamento

            });
        }
        public async Task<IEnumerable<EmpleadoDTO>> ListarEmpleadosPorDepartamento(string departamento)
        {
            var lista = await _empleadoRepository.ListarEmpleadosPorDepartamentoAsync(departamento);
            return lista.Select(e => new EmpleadoDTO
            {
                CodigoEmpleado = e.CodigoEmpleado,
                Contraseña = e.Contraseña,
                PrimerNombre = e.PrimerNombre,
                SegundoNombre = e.SegundoNombre,
                PrimerApellido = e.PrimerApellido,
                SegundoApellido = e.SegundoApellido,
                Departamento = e.Departamento
            });
        }
        public async Task NuevoEmpleado(EmpleadoDTO e)
        {
            var empleado = new Empleado
            {
                CodigoEmpleado = e.CodigoEmpleado,
                Contraseña = e.Contraseña,
                PrimerNombre = e.PrimerNombre,
                SegundoNombre = e.SegundoNombre,
                PrimerApellido = e.PrimerApellido,
                SegundoApellido = e.SegundoApellido,
                Departamento = e.Departamento

            };
            await _empleadoRepository.GuardarEmpleadoAsync(empleado);
        }
        public async Task EditarEmpleado(EmpleadoDTO e)
        {
            var empleado = new Empleado
            {
                CodigoEmpleado = e.CodigoEmpleado,
                Contraseña = e.Contraseña,
                PrimerNombre = e.PrimerNombre,
                SegundoNombre = e.SegundoNombre,
                PrimerApellido = e.PrimerApellido,
                SegundoApellido = e.SegundoApellido,
                Departamento = e.Departamento
            };
            await _empleadoRepository.EditarEmpleadoAsync(empleado);
        }

        // Corregido: ahora recibe string en vez de int
        public async Task EliminarEmpleado(string CodigoEmpleado)
        {
            await _empleadoRepository.EliminarEmpleadoAsync(CodigoEmpleado);
        }

    }
}