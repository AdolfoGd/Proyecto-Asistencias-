using Application.Interface;
using Domain.Entities;
using Infraestructure.DataBase;
using MySql.Data.MySqlClient;
using System.Data;

namespace Infraestructure.Repository
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly DBConnectionFactory _connectionFactory;

        public EmpleadoRepository(DBConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Empleado>> ListarEmpleadosAsync()
        {
            var lista = new List<Empleado>();
            using (MySqlConnection conn = (MySqlConnection)_connectionFactory.CreateConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_LeerEmpleados", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new Empleado
                            {
                                CodigoEmpleado = reader.GetString("CodigoEmpleado"),
                                Contraseña = reader.GetString("Contraseña"),
                                PrimerNombre = reader.GetString("PrimerNombre"),
                                SegundoNombre = reader.GetString("SegundoNombre"),
                                PrimerApellido = reader.GetString("PrimerApellido"),
                                SegundoApellido = reader.GetString("SegundoApellido"),
                                Departamento = reader.GetString("Departamento"),
                            });
                        }
                    }
                }
                return lista;
            }
        }

        public async Task<IEnumerable<Empleado>> ListarEmpleadosPorDepartamentoAsync(string departamento)
        {
            var lista = new List<Empleado>();
            using (MySqlConnection con = (MySqlConnection)_connectionFactory.CreateConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_LeerEmpleadosPorDepartamento", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_departamento", departamento);
                    await con.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new Empleado
                            {
                                CodigoEmpleado = reader.GetString("CodigoEmpleado"),
                                Contraseña = reader.GetString("Contraseña"),
                                PrimerNombre = reader.GetString("PrimerNombre"),
                                SegundoNombre = reader.GetString("SegundoNombre"),
                                PrimerApellido = reader.GetString("PrimerApellido"),
                                SegundoApellido = reader.GetString("SegundoApellido"),
                                Departamento = reader.GetString("Departamento"),
                            });
                        }
                    }
                }
                return lista;
            }
        }

        public async Task GuardarEmpleadoAsync(Empleado empleado)
        {
            using var con = (MySqlConnection)_connectionFactory.CreateConnection();
            await con.OpenAsync();
            try
            {
                using var cmd = new MySqlCommand("sp_InsertarEmpleado", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("@codigoEmpleado", empleado.CodigoEmpleado));
                cmd.Parameters.Add(new MySqlParameter("@contrasena", empleado.Contraseña));
                cmd.Parameters.Add(new MySqlParameter("@primerNombre", empleado.PrimerNombre));
                cmd.Parameters.Add(new MySqlParameter("@segundoNombre", empleado.SegundoNombre));
                cmd.Parameters.Add(new MySqlParameter("@primerApellido", empleado.PrimerApellido));
                cmd.Parameters.Add(new MySqlParameter("@segundoApellido", empleado.SegundoApellido));
                cmd.Parameters.Add(new MySqlParameter("@departamento", empleado.Departamento));
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task EditarEmpleadoAsync(Empleado empleado)
        {
            using var con = (MySqlConnection)_connectionFactory.CreateConnection();
            await con.OpenAsync();
            using (MySqlCommand cmd = new MySqlCommand("sp_ActualizarEmpleado", con)) 
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("@codigoEmpleado", empleado.CodigoEmpleado));
                cmd.Parameters.Add(new MySqlParameter("@contrasena", empleado.Contraseña));
                cmd.Parameters.Add(new MySqlParameter("@primerNombre", empleado.PrimerNombre));
                cmd.Parameters.Add(new MySqlParameter("@segundoNombre", empleado.SegundoNombre));
                cmd.Parameters.Add(new MySqlParameter("@primerApellido", empleado.PrimerApellido));
                cmd.Parameters.Add(new MySqlParameter("@segundoApellido", empleado.SegundoApellido));
                cmd.Parameters.Add(new MySqlParameter("@departamento", empleado.Departamento));
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task EliminarEmpleadoAsync(string codigoEmpleado)
        {
            using var con = (MySqlConnection)_connectionFactory.CreateConnection();
            await con.OpenAsync();
            using (MySqlCommand cmd = new MySqlCommand("sp_EliminarEmpleado", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", codigoEmpleado);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        

        public async Task<Empleado> ValidarCredencialesAsync(string codigo, string contraseña)
        {
            using (MySqlConnection conn = (MySqlConnection)_connectionFactory.CreateConnection())
            {
                string query = "SELECT Codigo_Empleado, Primer_Nombre, Primer_Apellido FROM Empleados WHERE Codigo_Empleado = @usuario AND Contrasena = @password";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@usuario", codigo);
                    cmd.Parameters.AddWithValue("@password", contraseña);

                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Empleado
                            {
                                CodigoEmpleado = reader.GetString("Codigo_Empleado"),
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