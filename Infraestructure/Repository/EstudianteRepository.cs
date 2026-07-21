    using Application.Interface;
using Domain.Entities;
using Infraestructure.DataBase;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;

namespace Infraestructure.Repository
{
    public class EstudianteRepository : IEstudianteRepository
    {
        private readonly DBConnectionFactory _connectionFactory;

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
                                AñoIngreso = DateOnly.FromDateTime((DateTime)reader["AñoIngreso"]),
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
                    cmd.Parameters.AddWithValue("@carnet", carnet);

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
                                AñoIngreso = DateOnly.FromDateTime((DateTime)reader["AñoIngreso"]),
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

                cmd.Parameters.Add(new MySqlParameter("@carnet", estudiante.Carnet));
                cmd.Parameters.Add(new MySqlParameter("@contraseña", estudiante.Contraseña));
                cmd.Parameters.Add(new MySqlParameter("@primernNombre", estudiante.PrimerNombre));
                cmd.Parameters.Add(new MySqlParameter("@segundoNombre", estudiante.SegundoNombre));
                cmd.Parameters.Add(new MySqlParameter("@primerApellido", estudiante.PrimerApellido));
                cmd.Parameters.Add(new MySqlParameter("@segundoApellido", estudiante.SegundoApellido));
                cmd.Parameters.Add(new MySqlParameter("@carrera", estudiante.Carrera));
                cmd.Parameters.Add(new MySqlParameter("@añoIngreso",estudiante.AñoIngreso));


                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Task EditarEstudianteAsync(Estudiante estudiante)
        {
            throw new NotImplementedException();
        }
        public Task EliminarEstudianteAsync(int id)
        {
            throw new NotImplementedException();
        }
        
    }
}
