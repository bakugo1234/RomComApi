using System.Collections.Generic;
using RomCom.Common.Enums;
using Microsoft.Data.SqlClient;
using RomCom.Repository.Setup.Contract;

namespace RomCom.Repository.Setup.Provider
{
    public class ConnectionProvider : IConnectionProvider
    {
        private readonly string _connectionString;

        public ConnectionProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection GetOpenDbConnection(Region? region = null)
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
