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
            using var conn = (MySqlConnection)_connectionFactory.CreateConnection();
            using var cmd = new MySqlCommand("sp_LeerEstudiantes", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            return lista;
        }

        public async Task<IEnumerable<Estudiante>> ListarEstudiantesPorCarnetAsync(string carnet)
        {
            var lista = new List<Estudiante>();
            using var conn = (MySqlConnection)_connectionFactory.CreateConnection();
            using var cmd = new MySqlCommand("sp_LeerEstudiantesPorCarnet", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@carnet", carnet?.Trim());

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            return lista;
        }

        public async Task GuardarEstudianteAsync(Estudiante estudiante)
        {
            using var conn = (MySqlConnection)_connectionFactory.CreateConnection();
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("sp_InsertarEstudiante", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@carnet", estudiante.Carnet?.Trim());
            cmd.Parameters.AddWithValue("@contrasena", estudiante.Contraseña);
            cmd.Parameters.AddWithValue("@primerNombre", estudiante.PrimerNombre);
            cmd.Parameters.AddWithValue("@segundoNombre", (object?)estudiante.SegundoNombre ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@primerApellido", estudiante.PrimerApellido);
            cmd.Parameters.AddWithValue("@segundoApellido", (object?)estudiante.SegundoApellido ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@carrera", estudiante.Carrera);
            cmd.Parameters.AddWithValue("@añoIngreso", estudiante.AñoIngreso.Year);

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

            cmd.Parameters.AddWithValue("@carnet", estudiante.Carnet?.Trim());
            cmd.Parameters.AddWithValue("@contrasena", estudiante.Contraseña);
            cmd.Parameters.AddWithValue("@primerNombre", estudiante.PrimerNombre);
            cmd.Parameters.AddWithValue("@segundoNombre", (object?)estudiante.SegundoNombre ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@primerApellido", estudiante.PrimerApellido);
            cmd.Parameters.AddWithValue("@segundoApellido", (object?)estudiante.SegundoApellido ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@carrera", estudiante.Carrera);
            cmd.Parameters.AddWithValue("@cantidadFaltas", estudiante.CantidadFaltas);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task EliminarEstudianteAsync(string carnet)
        {
            using var conn = (MySqlConnection)_connectionFactory.CreateConnection();
            await conn.OpenAsync();

            using (MySqlCommand cmd = new MySqlCommand("sp_EliminarDocente", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", carnet?.Trim());

                await cmd.ExecuteNonQueryAsync();
            }
            ;
            
        }

        
        
    }
}