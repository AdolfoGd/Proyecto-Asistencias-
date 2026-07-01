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
    }
}
