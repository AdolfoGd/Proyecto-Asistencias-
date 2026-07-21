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
    public class MatriculasRepository : IMatriculaRepository
    {
        private readonly DBConnectionFactory _connectionFactory;

        public async Task<IEnumerable<Matriculas>> ListarMatriculasAsync()
        {
            var lista = new List<Matriculas>();
            using (MySqlConnection conn = (MySqlConnection)_connectionFactory.CreateConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_LeerMatriculas", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var grupos = new Matriculas
                            {
                                IdMatricula = reader.GetInt32("IdMatricula"),
                                IdEstudiante = reader.GetInt32("IdEstudiante"),
                                CodigoGrupo = reader.GetString("CodigoGrupo"),
                                FechaRegistro = DateOnly.FromDateTime((DateTime)reader["FechaRegistro"]),
                            };
                            lista.Add(grupos);
                        }
                    }
                }
                return lista;
            }
        }
        public async Task<IEnumerable<Matriculas>> ListarMatriculasporIdEstudianteAsync(int IdEstudiante)
        {
            var lista = new List<Matriculas>();
            using (MySqlConnection con = (MySqlConnection)_connectionFactory.CreateConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_LeerMatriculasPorEstudiante", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idEstudiante", IdEstudiante);

                    await con.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var matriculas = new Matriculas
                            {
                                IdMatricula = reader.GetInt32("IdMatricula"),
                                IdEstudiante = reader.GetInt32("IdEstudiante"),
                                CodigoGrupo = reader.GetString("CodigoGrupo"),
                                FechaRegistro = DateOnly.FromDateTime((DateTime)reader["FechaRegistro"]),
                            };
                            lista.Add(matriculas);
                        }
                    }
                }
                return lista;
            }
        }
        public async Task GuardarMatriculaAsync(Matriculas matricula)
        {
            using var con = (MySqlConnection)_connectionFactory.CreateConnection();
            await con.OpenAsync();

            try
            {
                using var cmd = new MySqlCommand("sp_InsertarMatricula", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new MySqlParameter("@idEstudiante", matricula.IdEstudiante));
                cmd.Parameters.Add(new MySqlParameter("@codigoGrupo", matricula.CodigoGrupo));
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
