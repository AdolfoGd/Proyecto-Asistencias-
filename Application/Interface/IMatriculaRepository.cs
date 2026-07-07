using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interface
{
    public interface IMatriculaRepository
    {
        public Task<IEnumerable<Matriculas>> ListarMatriculasAsync();
        public Task<IEnumerable<Matriculas>> ListarMatriculasporIdEstudianteAsync(int IdEstudiante);
        Task GuardarMatriculaAsync(Matriculas matricula);
        Task EditarMatriculaAsync(Matriculas matricula);
        Task EliminarMatriculaAsync(int id);
    }
}
