using Application.Interface;
using Domain.Entities;
using Infraestructure.DataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Repository
{
    public class AsistenciaRepository : IAsistenciaRepository
    {
        private readonly DBConnectionFactory  _connectionFactory;

        public AsistenciaRepository(DBConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        

        Task<IEnumerable<Asistencia>> IAsistenciaRepository.ListarAsistenciaAsync()
        {
            throw new NotImplementedException();
        }
        Task<IEnumerable<Asistencia>> IAsistenciaRepository.ListarAsistenciaPorSesionAsync(int idSesion)
        {
            throw new NotImplementedException();
        }
        public Task NuevaAsistenciaAsync(Asistencia asistencia)
        {
            throw new NotImplementedException();
        }

        public Task EditarAsistenciaAsync(Asistencia asistencia)
        {
            throw new NotImplementedException();
        }

        public Task EliminarAsistenciaAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

