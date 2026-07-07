using Application.Interface;
using Domain.Entities;
using Infraestructure.DataBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace Infraestructure.Repository
{
    public class AsistenciaRepository : IAsistenciaRepository
    {
        private readonly DBConnectionFactory _connectionFactory;

        public AsistenciaRepository(DBConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }


        async Task<IEnumerable<Asistencia>> IAsistenciaRepository.ListarAsistenciaAsync()
        {

            var lista = new List<Asistencia>();
            using (MySqlConnection conn = (MySqlConnection)_connectionFactory.CreateConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM asistencia", conn))
                {
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var asistencia = new Asistencia
                            {
                                IdAsistencia = reader.GetInt32("Id_Asistencia"),
                                IdSesion = reader.GetInt32("Id_Sesion"),
                                Carnet = reader.GetInt32("Carnet_Usuario"),
                                Estado = reader.GetString("Estado")
                                
                            };
                            lista.Add(asistencia);
                        }
                    }
                } return lista;
            }    


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

