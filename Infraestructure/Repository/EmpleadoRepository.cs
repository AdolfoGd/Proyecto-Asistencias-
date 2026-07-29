using Application.Interface;
using Domain.Entities;
using Infraestructure.DataBase;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;

namespace Infraestructure.Repository
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly DBConnectionFactory _connectionFactory;

        async Task<IEnumerable<Empleado>> IEmpleadoRepository.ListarEmpleadosAsync()
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
                            var empleado = new Empleado
                            {
                                CodigoEmpleado = reader.GetString("CodigoEmpleado"),
                                Contraseña = reader.GetString("Contraseña"),
                                PrimerNombre = reader.GetString("PrimerNombre"),
                                SegundoNombre = reader.GetString("SegundoNombre"),
                                PrimerApellido = reader.GetString("PrimerApellido"),
                                SegundoApellido = reader.GetString("SegundoApellido"),
                                Departamento = reader.GetString("Departamento"),
                            };
                            lista.Add(empleado);
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
                    cmd.Parameters.AddWithValue("@departamento", departamento);

                    await con.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var empleado = new Empleado
                            {
                                CodigoEmpleado = reader.GetString("CodigoEmpleado"),
                                Contraseña = reader.GetString("Contraseña"),
                                PrimerNombre = reader.GetString("PrimerNombre"),
                                SegundoNombre = reader.GetString("SegundoNombre"),
                                PrimerApellido = reader.GetString("PrimerApellido"),
                                SegundoApellido = reader.GetString("SegundoApellido"),
                                Departamento = reader.GetString("Departamento"),
                            };
                            lista.Add(empleado);
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
                cmd.Parameters.Add(new MySqlParameter("@contraseña", empleado.Contraseña));
                cmd.Parameters.Add(new MySqlParameter("@primerNombre", empleado.PrimerNombre));
                cmd.Parameters.Add(new MySqlParameter("@segundoNombre", empleado.SegundoNombre));
                cmd.Parameters.Add(new MySqlParameter("@primernpAellido", empleado.PrimerApellido));
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
            using (MySqlCommand cmd = new MySqlCommand("sp_ActualizarDocente", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("@codigoEmpleado ", empleado.CodigoEmpleado));
                cmd.Parameters.Add(new MySqlParameter("@contraseña", empleado.Contraseña));
                cmd.Parameters.Add(new MySqlParameter("@primerNombre", empleado.PrimerNombre));
                cmd.Parameters.Add(new MySqlParameter("@egundoNombre", empleado.SegundoNombre));
                cmd.Parameters.Add(new MySqlParameter("@primerApellido", empleado.PrimerApellido));
                cmd.Parameters.Add(new MySqlParameter("@segundoApellido", empleado.SegundoApellido));
                cmd.Parameters.Add(new MySqlParameter("@departamento", empleado.Departamento));
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task EliminarEmpleadoAsync(int codigoempleado)
        {
            using var con = (MySqlConnection)_connectionFactory.CreateConnection();
            await con.OpenAsync();
            using (MySqlCommand cmd = new MySqlCommand("sp_EliminarDocente", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", codigoempleado);

                await cmd.ExecuteNonQueryAsync();
            }
        }

    }
}
