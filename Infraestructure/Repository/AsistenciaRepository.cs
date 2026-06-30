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
        
        public Task ListarAsistenciaAsync()
        {
            throw new NotImplementedException();
        }

        public Task GuardarAsistencia(Asistencia asistencia)
        {
            throw new NotImplementedException();
        }
        public Task EditarAsistencia(Asistencia asistencia)
        {
            throw new NotImplementedException();
        }
        public Task NuevaAsistencia(Asistencia asistencia)
        {
            throw new NotSupportedException();
        }
        public Task EliminarAsistencia(int id)
        {
            throw new NotSupportedException();
        }
    }
}

