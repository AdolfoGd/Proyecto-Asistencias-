using Application.Interface;
using Domain.Entities;
using Infraestructure.DataBase;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class EstudianteRepository : IEstudianteRepository
    {
        private readonly DBConnectionFactory _connectionFactory;

        public EstudianteRepository(DBConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Estudiante>> ListarEstudiantesAsync()
        {
            var lista = new List<Estudiante>();
            using (MySqlConnection conn = (MySqlConnection)_connectionFactory.CreateConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_LeerEstudiantes", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var estudiante = new Estudiante
                            {
                                Carnet = reader.GetString("Carnet"),
                                Contraseña = reader.GetString("Contraseña"),
                                PrimerNombre = reader.GetString("PrimerNombre"),
                                SegundoNombre = reader.GetString("SegundoNombre"),
                                PrimerApellido = reader.GetString("PrimerApellido"),
                                SegundoApellido = reader.GetString("SegundoApellido"),
                                Carrera = reader.GetString("Carrera"),
                                AnioIngreso = reader.GetInt32("AñoIngreso"),
                                CantidadFaltas = reader.GetInt32("CantidadFaltas")
                            };
                            lista.Add(estudiante);
                        }
                    }
                }
                return lista;
            }
        }

        public async Task<IEnumerable<Estudiante>> ListarEstudiantesPorCarnetAsync(string carnet)
        {
            var lista = new List<Estudiante>();
            using (MySqlConnection con = (MySqlConnection)_connectionFactory.CreateConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_LeerEstudiantesPorCarnet", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_carnet", carnet);

                    await con.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var estudiante = new Estudiante
                            {
                                Carnet = reader.GetString("Carnet"),
                                Contraseña = reader.GetString("Contraseña"),
                                PrimerNombre = reader.GetString("PrimerNombre"),
                                SegundoNombre = reader.GetString("SegundoNombre"),
                                PrimerApellido = reader.GetString("PrimerApellido"),
                                SegundoApellido = reader.GetString("SegundoApellido"),
                                Carrera = reader.GetString("Carrera"),
                                AnioIngreso = reader.GetInt32("AñoIngreso"),
                                CantidadFaltas = reader.GetInt32("CantidadFaltas")
                            };
                            lista.Add(estudiante);
                        }
                    }
                }
                return lista;
            }
        }

        public async Task GuardarEstudianteAsync(Estudiante estudiante)
        {
            using var con = (MySqlConnection)_connectionFactory.CreateConnection();
            await con.OpenAsync();
            try
            {
                using var cmd = new MySqlCommand("sp_InsertarEstudiante", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@carnet", estudiante.Carnet);
                cmd.Parameters.AddWithValue("@contrasena", estudiante.Contraseña);
                cmd.Parameters.AddWithValue("@primerNombre", estudiante.PrimerNombre);
                cmd.Parameters.AddWithValue("@segundoNombre", estudiante.SegundoNombre);
                cmd.Parameters.AddWithValue("@primerApellido", estudiante.PrimerApellido);
                cmd.Parameters.AddWithValue("@segundoApellido", estudiante.SegundoApellido);
                cmd.Parameters.AddWithValue("@carrera", estudiante.Carrera);
                cmd.Parameters.AddWithValue("@añoIngreso", estudiante.AnioIngreso);
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task EditarEstudianteAsync(Estudiante estudiante)
        {
            using var con = (MySqlConnection)_connectionFactory.CreateConnection();
            await con.OpenAsync();
            using (MySqlCommand cmd = new MySqlCommand("sp_ActualizarEstudiante", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("@p_carnet", estudiante.Carnet));
                cmd.Parameters.Add(new MySqlParameter("@p_contrasena", estudiante.Contraseña));
                cmd.Parameters.Add(new MySqlParameter("@p_primerNombre", estudiante.PrimerNombre));
                cmd.Parameters.Add(new MySqlParameter("@p_segundoNombre", estudiante.SegundoNombre));
                cmd.Parameters.Add(new MySqlParameter("@p_primerApellido", estudiante.PrimerApellido));
                cmd.Parameters.Add(new MySqlParameter("@p_segundoApellido", estudiante.SegundoApellido));
                cmd.Parameters.Add(new MySqlParameter("@p_carrera", estudiante.Carrera));
                cmd.Parameters.Add(new MySqlParameter("@p_añoIngreso", estudiante.AnioIngreso));
                cmd.Parameters.Add(new MySqlParameter("@p_cantidadFaltas", estudiante.CantidadFaltas));
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task EliminarEstudianteAsync(int carnet)
        {
            using var conn = (MySqlConnection)_connectionFactory.CreateConnection();
            await conn.OpenAsync();

            using (MySqlCommand cmd = new MySqlCommand("sp_EliminarEstudiante", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", carnet);

                await cmd.ExecuteNonQueryAsync();
            }
            ;
            
        }

        public async Task<Estudiante?> ValidarCredencialesAsync(string carnet, string contraseña)
        {
            using (MySqlConnection conn = (MySqlConnection)_connectionFactory.CreateConnection())
            {
                string query = "SELECT Carnet, Primer_Nombre, Primer_Apellido FROM Estudiantes WHERE Carnet = @Carnet AND Contrasena = @Contrasena";

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
                            return new Estudiante
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