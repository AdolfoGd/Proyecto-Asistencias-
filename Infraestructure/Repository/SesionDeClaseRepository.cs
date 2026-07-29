using Application.Interface;
using Domain.Entities;
using Infraestructure.DataBase;
using MySql.Data.MySqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Repository
{
    public class SesionDeClaseRepository : ISeseionDeClaseRepository
    {
        private readonly DBConnectionFactory _connectionFactory;

        public async Task<IEnumerable<Sesion_de_Classe>> ListarSesionDeClaseAsync()
        {
            var lista = new List<Sesion_de_Classe>();
            using (MySqlConnection conn = (MySqlConnection)_connectionFactory.CreateConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_LeerSesiones", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var sesion = new Sesion_de_Classe
                            {
                                IdSesion = reader.GetInt32("IdSesion"),
                                CodigoGrupo = reader.GetInt32("CodigoGrupo"),
                                TemaImpartido = reader.GetString("TemaImpartido"),
                                FechaHoraInicio = reader.GetDateTime("FechaHoraInicio"),
                                FechaHoraFin = reader.GetDateTime("FechaHoraFin"),
                                Seccion = reader.GetString("Seccion"),
                                Departamento = reader.GetString("Departamento"),
                            };
                            lista.Add(sesion);
                        }
                    }
                }   
                return lista;
            }
        }

        public async Task<IEnumerable<Sesion_de_Classe>> ListarSesionDeClasePorIdGrupoAsync(int IdGrupo)
        {
            var lista = new List<Sesion_de_Classe>();
            using (MySqlConnection con = (MySqlConnection)_connectionFactory.CreateConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_LeerSesionPorGrupo", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idGrupo", IdGrupo);

                    await con.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var matriculas = new Sesion_de_Classe
                            {
                                IdSesion = reader.GetInt32("IdSesion"),
                                CodigoGrupo = reader.GetInt32("CodigoGrupo"),
                                TemaImpartido = reader.GetString("TemaImpartido"),
                                FechaHoraInicio = reader.GetDateTime("FechaHoraInicio"),
                                FechaHoraFin = reader.GetDateTime("FechaHoraFin"),
                                Seccion = reader.GetString("Seccion"),
                                Departamento = reader.GetString("Departamento"),
                            };
                            lista.Add(matriculas);
                        }
                    }
                }
                return lista;
            }
        }

        public async Task GuardarSesionDeClaseAsync(Sesion_de_Classe sesionDeClase)
        {
            using var con = (MySqlConnection)_connectionFactory.CreateConnection();
            await con.OpenAsync();

            try
            {
                using var cmd = new MySqlCommand("sp_InsertarSesion", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new MySqlParameter("@codigoGrupo", sesionDeClase.CodigoGrupo));
                cmd.Parameters.Add(new MySqlParameter("@temaImpartido", sesionDeClase.TemaImpartido));
                cmd.Parameters.Add(new MySqlParameter("@fechaHoraInicio", sesionDeClase.FechaHoraInicio));
                cmd.Parameters.Add(new MySqlParameter("@FechaHoraFin", sesionDeClase.FechaHoraFin));
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task EditarSesionDeClaseAsync(Sesion_de_Classe sesionDeClase)
        {
            using var con = (MySqlConnection)_connectionFactory.CreateConnection();
            await con.OpenAsync();
            using (MySqlCommand cmd = new MySqlCommand("sp_ActualizarSesion", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("@idSesion ", sesionDeClase.Seccion));
                cmd.Parameters.Add(new MySqlParameter("@codigoGrupo", sesionDeClase.CodigoGrupo));
                cmd.Parameters.Add(new MySqlParameter("@temaImpartido", sesionDeClase.TemaImpartido));
                cmd.Parameters.Add(new MySqlParameter("@fechaHoraInicio", sesionDeClase.FechaHoraInicio));
                cmd.Parameters.Add(new MySqlParameter("@fechaHoraFin", sesionDeClase.FechaHoraFin));
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task EliminarSesionDeClaseAsync(int idsesion)
        {
            using var con = (MySqlConnection)_connectionFactory.CreateConnection();
            await con.OpenAsync();
            using (MySqlCommand cmd = new MySqlCommand("sp_EliminarSesion", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", idsesion);

                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

}
