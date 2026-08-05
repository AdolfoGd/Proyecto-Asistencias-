using Application.Interface;
using Domain.Entities;
using Infraestructure.DataBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading.Tasks;
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
                using (MySqlCommand cmd = new MySqlCommand("sp_LeerAsistencias", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var asistencia = new Asistencia
                            {
                                IdAsistencia = reader.GetInt32("IdAsistencia"),
                                IdSesion = reader.GetInt32("IdSesion"),
                                Carnet = reader.GetInt32("Carnet"),
                                Estado = reader.GetString("Estado"),
                                Hora_Registro = reader["Hora_Registro"] != DBNull.Value? TimeOnly.FromTimeSpan((TimeSpan)reader["Hora_Registro"]): null

                            };
                            lista.Add(asistencia);
                        }
                    }
                } return lista;
            }    


        }
        async Task<IEnumerable<Asistencia>> IAsistenciaRepository.ListarAsistenciaPorSesionAsync(int idsesion)
        {
            var lista = new List<Asistencia>();
            using (MySqlConnection con = (MySqlConnection)_connectionFactory.CreateConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_LeerAsistenciaPorSesion", con))
                {
              
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idSesion", idsesion);

                    await con.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var asistencia = new Asistencia
                            {
                                IdAsistencia = reader.GetInt32("IdAsistencia"),
                                IdSesion = reader.GetInt32("IdSesion"),
                                Carnet = reader.GetInt32("Carnet"),
                                Estado = reader.GetString("Estado"),
                                Hora_Registro = TimeOnly.FromTimeSpan((TimeSpan)reader["Hora_Registro"])
                            };
                            lista.Add(asistencia);
                        }
                    }
                }
                return lista;
            }
        }
        
        public async Task NuevaAsistenciaAsync(Asistencia asistencia)
        {
            using var con = (MySqlConnection)_connectionFactory.CreateConnection();
            await con.OpenAsync();

            try
            {
                using var cmd = new MySqlCommand("sp_InsertarAsistencia", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new MySqlParameter("@idsesion", asistencia.IdSesion));
                cmd.Parameters.Add(new MySqlParameter("@carnet", asistencia.Carnet));
                cmd.Parameters.Add(new MySqlParameter("@estado", asistencia.Estado));


                await cmd.ExecuteNonQueryAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task EditarAsistenciaAsync(Asistencia asistencia, string estado)
        {

            using var con = (MySqlConnection)_connectionFactory.CreateConnection();
            await con.OpenAsync();
            using (MySqlCommand cmd = new MySqlCommand("sp_ActualizarAsistencia", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idAsistencia", asistencia.IdAsistencia);
                cmd.Parameters.AddWithValue("@estado", estado);

                await cmd.ExecuteNonQueryAsync();
            }
        }

        
        public async Task EliminarAsistenciaAsync(int id)
        {
            using var con = (MySqlConnection)_connectionFactory.CreateConnection();
            await con.OpenAsync();
            using (MySqlCommand cmd = new MySqlCommand("sp_EliminarAsistencia", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);

                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}

