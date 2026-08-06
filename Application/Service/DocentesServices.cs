using Application.DTOs;
using Application.Interface;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Service
{
    public class DocentesServices
    {
        private readonly IDocentesRepository _docentesRepository;

        public DocentesServices(IDocentesRepository docentesRepository)
        {
            _docentesRepository = docentesRepository;
        }
        public async Task<IEnumerable<DocentesDTO>> ListarDocentes()
        {
            var lista = await _docentesRepository.ListarDocentesAsync();
            return lista.Select(d => new DocentesDTO
            {
                Carnet = d.Carnet,
                Contraseña = d.Contraseña,
                PrimerNombre = d.PrimerNombre,
                SegundoNombre = d.SegundoNombre,
                PrimerApellido = d.PrimerApellido,
                SegundoApellido = d.SegundoApellido,
                CodigoEmpleado = d.CodigoEmpleado,
                Departamento = d.Departamento,
                CantidadFaltas = d.CantidadFaltas,

            });
        }
        public async Task<IEnumerable<DocentesDTO>> ListarDocentesPorDepartamento(string departamento)
        {
            var lista = await _docentesRepository.ListarDocentesPorDepartamentoAsync(departamento);
            return lista.Select(d => new DocentesDTO
            {
                Carnet = d.Carnet,
                Contraseña = d.Contraseña,
                PrimerNombre = d.PrimerNombre,
                SegundoNombre = d.SegundoNombre,
                PrimerApellido = d.PrimerApellido,
                SegundoApellido = d.SegundoApellido,
                CodigoEmpleado = d.CodigoEmpleado,
                Departamento = d.Departamento,
                CantidadFaltas = d.CantidadFaltas,
            });
        }
        public async Task NuevoDocente(DocentesDTO d)
        {
            var docentes = new Docentes
            {
                Carnet = d.Carnet,
                Contraseña = d.Contraseña,
                PrimerNombre = d.PrimerNombre,
                SegundoNombre = d.SegundoNombre,
                PrimerApellido = d.PrimerApellido,
                SegundoApellido = d.SegundoApellido,
                CodigoEmpleado = d.CodigoEmpleado,
                Departamento = d.Departamento

            };
            await _docentesRepository.GuardarDocentesAsync(docentes);
        }
        public async Task EditarDocente(DocentesDTO d)
        {
            var docentes = new Docentes
            {
                Carnet = d.Carnet,
                Contraseña = d.Contraseña,
                PrimerNombre = d.PrimerNombre,
                SegundoNombre = d.SegundoNombre,
                PrimerApellido = d.PrimerApellido,
                SegundoApellido = d.SegundoApellido,
                Departamento = d.Departamento,
                CantidadFaltas = d.CantidadFaltas
            };
            await _docentesRepository.EditarDocentesAsync(docentes);
        }
        public async Task EliminarDocente(string carnet)
        {
            await _docentesRepository.EliminarDocentesAsync(carnet);
        }


    }
}
