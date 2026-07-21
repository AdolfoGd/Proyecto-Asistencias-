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
                    cmd.Parameters.AddWithValue("@departamento", departamento);

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

                cmd.Parameters.Add(new MySqlParameter("@p_carnet", docentes.Carnet));
                cmd.Parameters.Add(new MySqlParameter("@p_pass", docentes.Contraseña));
                cmd.Parameters.Add(new MySqlParameter("@p_n1", docentes.PrimerNombre));
                cmd.Parameters.Add(new MySqlParameter("@p_n2", docentes.SegundoNombre));
                cmd.Parameters.Add(new MySqlParameter("@p_app1", docentes.PrimerApellido));
                cmd.Parameters.Add(new MySqlParameter("@p_app2", docentes.SegundoApellido));
                cmd.Parameters.Add(new MySqlParameter("@p_cod", docentes.CodigoEmpleado));
                cmd.Parameters.Add(new MySqlParameter("@p_dept", docentes.Departamento));


                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task EditarDocentesAsync(Docentes docentes)
        {
            throw new NotImplementedException();
        }

        public Task EliminarDocentesAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
