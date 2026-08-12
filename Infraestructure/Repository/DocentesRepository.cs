using Application.DTOs;
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
    public class DocentesRepository : IDocentesRepository
    {
        private readonly DBConnectionFactory _connectionFactory;

        public DocentesRepository(DBConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        async Task<IEnumerable<Docentes>> IDocentesRepository.ListarDocentesAsync()
        {
            var lista = new List<Docentes>();
            using (MySqlConnection conn = (MySqlConnection)_connectionFactory.CreateConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_LeerDocentes", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var docentes = new Docentes
                            {
                                Carnet = reader.GetString("Carnet"),
                                Contraseña = reader.GetString("Contraseña"),
                                PrimerNombre = reader.GetString("PrimerNombre"),
                                SegundoNombre = reader.GetString("SegundoNombre"),
                                PrimerApellido = reader.GetString("PrimerApellido"),
                                SegundoApellido = reader.GetString("SegundoApellido"),
                                CodigoEmpleado = reader.GetString("CodigoEmpleado"),
                                Departamento = reader.GetString("Departamento"),
                                CantidadFaltas = reader.GetInt32("CantidadFaltas")
                            };
                            lista.Add(docentes);
                        }
                    }
                }
                return lista;
            }
        }
        public async Task<IEnumerable<Docentes>> ListarDocentesPorDepartamentoAsync(string departamento)
        {
            var lista = new List<Docentes>();
            using (MySqlConnection con = (MySqlConnection)_connectionFactory.CreateConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_LeerDocentesPorDepartamento", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_departamento", departamento);

                    await con.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var docentes = new Docentes
                            {
                                Carnet = reader.GetString("Carnet"),
                                Contraseña = reader.GetString("Contraseña"),
                                PrimerNombre = reader.GetString("PrimerNombre"),
                                SegundoNombre = reader.GetString("SegundoNombre"),
                                PrimerApellido = reader.GetString("PrimerApellido"),
                                SegundoApellido = reader.GetString("SegundoApellido"),
                                CodigoEmpleado = reader.GetString("CodigoEmpleado"),
                                Departamento = reader.GetString("Departamento"),
                                CantidadFaltas = reader.GetInt32("CantidadFaltas")
                            };
                            lista.Add(docentes);
                        }
                    }
                }
                return lista;
            }
        }
        public async Task GuardarDocentesAsync(Docentes docentes)
        {
            using var con = (MySqlConnection)_connectionFactory.CreateConnection();
            await con.OpenAsync();

            try
            {
                using var cmd = new MySqlCommand("sp_InsertarDocente", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new MySqlParameter("@carnet", docentes.Carnet));
                cmd.Parameters.Add(new MySqlParameter("@contrasena", docentes.Contraseña));
                cmd.Parameters.Add(new MySqlParameter("@primerNombre", docentes.PrimerNombre));
                cmd.Parameters.Add(new MySqlParameter("@segundoNombre", docentes.SegundoNombre));
                cmd.Parameters.Add(new MySqlParameter("@primerApellido", docentes.PrimerApellido));
                cmd.Parameters.Add(new MySqlParameter("@segundoApellido", docentes.SegundoApellido));
                cmd.Parameters.Add(new MySqlParameter("@codigoEmpleado", docentes.CodigoEmpleado));
                cmd.Parameters.Add(new MySqlParameter("@departamento", docentes.Departamento));
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task EditarDocentesAsync(Docentes docentes)
        {
            using var con = (MySqlConnection)_connectionFactory.CreateConnection();
            await con.OpenAsync();
            using (MySqlCommand cmd = new MySqlCommand("sp_ActualizarDocente", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("@p_carnet", docentes.Carnet));
                cmd.Parameters.Add(new MySqlParameter("@p_contrasena", docentes.Contraseña));
                cmd.Parameters.Add(new MySqlParameter("@p_primerNombre", docentes.PrimerNombre));
                cmd.Parameters.Add(new MySqlParameter("@p_segundoNombre", docentes.SegundoNombre));
                cmd.Parameters.Add(new MySqlParameter("@p_primerApellido", docentes.PrimerApellido));
                cmd.Parameters.Add(new MySqlParameter("@p_segundoApellido", docentes.SegundoApellido));
                cmd.Parameters.Add(new MySqlParameter("@p_departamento", docentes.Departamento));
                cmd.Parameters.Add(new MySqlParameter("@p_cantidadFaltas", docentes.CantidadFaltas));
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task EliminarDocentesAsync(string carnet)
        {
            using var con = (MySqlConnection)_connectionFactory.CreateConnection();
            await con.OpenAsync();
            using (MySqlCommand cmd = new MySqlCommand("sp_EliminarDocente", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", carnet);

                await cmd.ExecuteNonQueryAsync();
            }
        }



        public async Task<Docentes?> ValidarCredencialesAsync(string carnet, string contraseña)
        {
            using (MySqlConnection conn = (MySqlConnection)_connectionFactory.CreateConnection())
            {
                string query = "SELECT Carnet, Primer_Nombre, Primer_Apellido FROM Docentes WHERE Carnet = @Carnet AND Contrasena = @Contrasena";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Carnet", carnet);
                    cmd.Parameters.AddWithValue("@Contrasena", contraseña);

                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Docentes
                            {
                                Carnet = reader.GetString("Carnet"),
                                PrimerNombre = reader.GetString("Primer_Nombre"),
                                PrimerApellido = reader.GetString("Primer_Apellido")
                            };
                        }
                    }
                }
                return null;
            }
        }
    }
}
