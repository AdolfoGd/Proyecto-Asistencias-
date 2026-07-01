using Application.Interface;
using Domain.Entities;
using Infraestructure.DataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Repository
{
    public class DocentesRepository : IDocentesRepository
    {
        private readonly DBConnectionFactory _connectionFactory;

        public DocentesRepository(DBConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        Task<IEnumerable<Docentes>> IDocentesRepository.ListarDocentesAsync()
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<Docentes>> ListarDocentesPorDepartamentoAsync(string departamento)
        {
            throw new NotImplementedException();
        }
        public Task GuardarDocentesAsync(Docentes docentes)
        {
            throw new NotImplementedException();
        }

        public Task EditarDocentesAsync(Docentes docentes)
        {
            throw new NotImplementedException();
        }

        public Task EliminarDocentesAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
