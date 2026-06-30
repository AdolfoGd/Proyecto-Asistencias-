using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Data.Sqlite;


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
            return new SqliteConnection(_connectionString);
        }
    }
    
}
