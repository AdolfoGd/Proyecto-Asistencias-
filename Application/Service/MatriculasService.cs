using Application.DTOs;
using Application.Interface;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Service
{
    public class MatriculasService
    {
        private readonly IMatriculaRepository _matriculaRepository;

        public MatriculasService(IMatriculaRepository matriculaRepository)
        {
            _matriculaRepository = matriculaRepository;
        }

        public async Task<IEnumerable<MatriculasDTO>> ListarMatriculasAsync()
        {
            var lista = await _matriculaRepository.ListarMatriculasAsync();
            return lista.Select(m => new MatriculasDTO
            {
                IdMatricula = m.IdMatricula,
                IdEstudiante = m.IdEstudiante,
                CodigoGrupo = m.CodigoGrupo,
                FechaRegistro = m.FechaRegistro
            });
        }
        public async Task<IEnumerable<MatriculasDTO>> ListarMatriculasporIdEstudianteAsync(int IdEstudiante)
        {
            var lista = await _matriculaRepository.ListarMatriculasporIdEstudianteAsync(IdEstudiante);
            return lista.Select(m => new MatriculasDTO
            {
                IdMatricula = m.IdMatricula,
                IdEstudiante = m.IdEstudiante,
                CodigoGrupo = m.CodigoGrupo,
                FechaRegistro = m.FechaRegistro
            });
        }
        public async Task NuevaMatricula(MatriculasDTO m)
        {
            var  matriculas = new Matriculas
            {
                IdEstudiante = m.IdEstudiante,
                CodigoGrupo = m.CodigoGrupo,
            };
            await _matriculaRepository.GuardarMatriculaAsync(matriculas);
        }
        public async Task EditarMatricula(MatriculasDTO m)
        {
            var matriculas = new Matriculas
            {
                IdMatricula = m.IdMatricula,
                IdEstudiante = m.IdEstudiante,
                CodigoGrupo = m.CodigoGrupo,
            };
            await _matriculaRepository.EditarMatriculaAsync(matriculas);
        }
        public async Task EliminarMatricula(MatriculasDTO m)
        {
            var matriculas = new Matriculas
            {
                IdMatricula = m.IdMatricula
            };
        }
    }
}
