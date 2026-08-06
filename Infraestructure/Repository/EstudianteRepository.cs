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
                                Carnet = reader.GetInt32("Carnet"),
                                Contraseña = reader.GetString("Contraseña"),
                                PrimerNombre = reader.GetString("PrimerNombre"),
                                SegundoNombre = reader.GetString("SegundoNombre"),
                                PrimerApellido = reader.GetString("PrimerApellido"),
                                SegundoApellido = reader.GetString("SegundoApellido"),
                                Carrera = reader.GetString("Carrera"),
                                AñoIngreso = new DateOnly(reader.GetInt32("AñoIngreso"), 1, 1),
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
                                Carnet = reader.GetInt32("Carnet"),
                                Contraseña = reader.GetString("Contraseña"),
                                PrimerNombre = reader.GetString("PrimerNombre"),
                                SegundoNombre = reader.GetString("SegundoNombre"),
                                PrimerApellido = reader.GetString("PrimerApellido"),
                                SegundoApellido = reader.GetString("SegundoApellido"),
                                Carrera = reader.GetString("Carrera"),
                                AñoIngreso = new DateOnly(reader.GetInt32("AñoIngreso"), 1, 1),
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
            using var conn = (MySqlConnection)_connectionFactory.CreateConnection();
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("sp_InsertarEstudiante", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@carnet", estudiante.Carnet);
            cmd.Parameters.AddWithValue("@contrasena", estudiante.Contraseña);
            cmd.Parameters.AddWithValue("@primerNombre", estudiante.PrimerNombre);
            cmd.Parameters.AddWithValue("@segundoNombre", (object?)estudiante.SegundoNombre);
            cmd.Parameters.AddWithValue("@primerApellido", estudiante.PrimerApellido);
            cmd.Parameters.AddWithValue("@segundoApellido", (object?)estudiante.SegundoApellido);
            cmd.Parameters.AddWithValue("@carrera", estudiante.Carrera);
            cmd.Parameters.AddWithValue("@añoIngreso", estudiante.AñoIngreso);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task EditarEstudianteAsync(Estudiante estudiante)
        {
            using var conn = (MySqlConnection)_connectionFactory.CreateConnection();
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("sp_ActualizarEstudiante", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@carnet", estudiante.Carnet);
            cmd.Parameters.AddWithValue("@contrasena", estudiante.Contraseña);
            cmd.Parameters.AddWithValue("@primerNombre", estudiante.PrimerNombre);
            cmd.Parameters.AddWithValue("@segundoNombre", (object?)estudiante.SegundoNombre);
            cmd.Parameters.AddWithValue("@primerApellido", estudiante.PrimerApellido);
            cmd.Parameters.AddWithValue("@segundoApellido", (object?)estudiante.SegundoApellido);
            cmd.Parameters.AddWithValue("@carrera", estudiante.Carrera);
            cmd.Parameters.AddWithValue("@cantidadFaltas", estudiante.CantidadFaltas);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task EliminarEstudianteAsync(int carnet)
        {
            using var conn = (MySqlConnection)_connectionFactory.CreateConnection();
            await conn.OpenAsync();

            using (MySqlCommand cmd = new MySqlCommand("sp_EliminarDocente", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", carnet);

                await cmd.ExecuteNonQueryAsync();
            }
            ;
            
        }

        
        
    }
}