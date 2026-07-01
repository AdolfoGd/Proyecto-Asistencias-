using Application.Interface;
using Domain.Entities;
using Infraestructure.DataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Repository
{
    public class EstudianteRepository : IEstudianteRepository
    {
        private readonly DBConnectionFactory _connectionFactory;

        public Task<IEnumerable<Estudiante>> ListarEstudiantesAsync()
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<Estudiante>> ListarEstudiantesPorCarnetAsync(string carnet)
        {
            throw new NotImplementedException();
        }
        public Task GuardarEstudianteAsync(Estudiante estudiante)
        {
            throw new NotSupportedException();
        }
        public Task EditarEstudianteAsync(Estudiante estudiante)
        {
            throw new NotImplementedException();
        }
        public Task EliminarEstudianteAsync(int id)
        {
            throw new NotImplementedException();
        }
        
    }
}
