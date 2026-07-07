using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;


namespace Infraestructure.DataBase
{
    public class DBConnectionFactory
    {
        private readonly string _connectionString;

        public DBConnectionFactory (string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
    
}
