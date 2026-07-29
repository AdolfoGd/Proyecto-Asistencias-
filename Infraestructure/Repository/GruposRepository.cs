using Application.Interface;
using Domain.Entities;
using Infraestructure.DataBase;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Infraestructure.Repository
{
    public class GruposRepository : IGruposRepository
    {

        private readonly DBConnectionFactory _connectionFactory; 

        public async Task<IEnumerable<Grupos>> ListarGrupoAsync()
        {
            var lista = new List<Grupos>();
            using (MySqlConnection conn = (MySqlConnection)_connectionFactory.CreateConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_LeerGrupos", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var grupos = new Grupos
                            {
                                CodigoGrupo = reader.GetString("CodigoGrupo"),
                                IdAsignatura = reader.GetInt32("IdAsignatura"),
                                IdDocente = reader.GetString("IdDocente"),
                                Seccion = reader.GetString("Seccion") 
                            };
                            lista.Add(grupos);
                        }
                    }
                }
                return lista;
            }
        }
        public async Task<IEnumerable<Grupos>> ListarGrupoporCodigoAsync(string Codigogrupo)
        {
            var lista = new List<Grupos>();
            using (MySqlConnection con = (MySqlConnection)_connectionFactory.CreateConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_LeerGrupoPorCodigo", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codigoGrupo", Codigogrupo);

                    await con.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var grupos = new Grupos
                            {
                                CodigoGrupo = reader.GetString("CodigoGrupo"),
                                IdAsignatura = reader.GetInt32("IdAsignatura"),
                                IdDocente = reader.GetString("IdDocente"),
                                Seccion = reader.GetString("Seccion")
                            };
                            lista.Add(grupos);
                        }
                    }
                }
                return lista;
            }
        }
        public async Task GuardarGrupoAsync(Grupos grupo)
        {
            using var con = (MySqlConnection)_connectionFactory.CreateConnection();
            await con.OpenAsync();

            try
            {
                using var cmd = new MySqlCommand("sp_InsertarGrupo", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new MySqlParameter("@codigoGrupo", grupo.CodigoGrupo));
                cmd.Parameters.Add(new MySqlParameter("@idAsignatura", grupo.IdAsignatura));
                cmd.Parameters.Add(new MySqlParameter("@idDocente", grupo.IdDocente));
                cmd.Parameters.Add(new MySqlParameter("@seccion", grupo.Seccion));
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task EditarGrupoAsync(Grupos grupo)
        {
            using var con = (MySqlConnection)_connectionFactory.CreateConnection();
            await con.OpenAsync();
            using (MySqlCommand cmd = new MySqlCommand("sp_ActualizarEstudiante", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("@codigoGrupo ", grupo.CodigoGrupo));
                cmd.Parameters.Add(new MySqlParameter("@idAsignatura", grupo.IdAsignatura));
                cmd.Parameters.Add(new MySqlParameter("@idDocente", grupo.IdDocente));
                cmd.Parameters.Add(new MySqlParameter("@seccion", grupo.Seccion));
                await cmd.ExecuteNonQueryAsync();
            }
        }
        async Task IGruposRepository.EliminarGrupoAsync(string Codigogrupo)
        {
            using var con = (MySqlConnection)_connectionFactory.CreateConnection();
            await con.OpenAsync();
            using (MySqlCommand cmd = new MySqlCommand("sp_EliminarEstudiante", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", Codigogrupo);

                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
