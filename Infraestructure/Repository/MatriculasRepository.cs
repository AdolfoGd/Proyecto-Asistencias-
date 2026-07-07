using Application.Interface;
using Domain.Entities;
using Infraestructure.DataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Repository
{
    public class MatriculasRepository : IMatriculaRepository
    {
        private readonly DBConnectionFactory _connectionFactory;

        public async Task<IEnumerable<Matriculas>> ListarMatriculasAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Matriculas>> ListarMatriculasporIdEstudianteAsync(int IdEstudiante)
        {
            throw new NotImplementedException();
        }
        public async Task GuardarMatriculaAsync(Matriculas matricula)
        {
            throw new NotImplementedException();
        }
        public async Task EditarMatriculaAsync(Matriculas matricula)
        {
            throw new NotImplementedException();
        }
        public async Task EliminarMatriculaAsync(int id)
        {
            throw new NotImplementedException();
        }

        
    }

   
}
