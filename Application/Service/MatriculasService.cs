using Application.DTOs;
using Application.Interface;
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
                CodidoGrupo = m.CodidoGrupo,
                FechaRegistro = m.FechaRegistro
            });
        }
    }
}
