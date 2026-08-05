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
            while (await reader.ReadAsync())
            {
                lista.Add(MapearEstudiante(reader));
            }
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
            while (await reader.ReadAsync())
            {
                lista.Add(MapearEstudiante(reader));
            }
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

            using var cmd = new MySqlCommand("sp_EliminarEstudiante", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@carnet", carnet?.Trim());

            await cmd.ExecuteNonQueryAsync();
        }

        private static Estudiante MapearEstudiante(DbDataReader reader)
        {
            var mySqlReader = (MySqlDataReader)reader;
            string carnet = mySqlReader.GetString(0);
            string contrasena = mySqlReader.IsDBNull(1) ? string.Empty : mySqlReader.GetString(1);
            string primerNombre = mySqlReader.GetString(2);
            string? segundoNombre = mySqlReader.IsDBNull(3) ? null : mySqlReader.GetString(3);
            string primerApellido = mySqlReader.GetString(4);
            string? segundoApellido = mySqlReader.IsDBNull(5) ? null : mySqlReader.GetString(5);
            string carrera = mySqlReader.GetString(6);
            int anio = mySqlReader.GetInt32(7);
            int cantidadFaltas = mySqlReader.GetInt32(8);

            return new Estudiante
            {
                Carnet = carnet,
                Contraseña = contrasena,
                PrimerNombre = primerNombre,
                SegundoNombre = segundoNombre,
                PrimerApellido = primerApellido,
                SegundoApellido = segundoApellido,
                Carrera = carrera,
                AñoIngreso = new DateOnly(anio, 1, 1),
                CantidadFaltas = cantidadFaltas
            };
        }
    }
}