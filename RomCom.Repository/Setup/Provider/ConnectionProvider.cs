using System.Collections.Generic;
using RomCom.Common.Enums;
using Microsoft.Data.SqlClient;
using RomCom.Repository.Setup.Contract;
using Microsoft.Extensions.Configuration;

namespace RomCom.Repository.Setup.Provider
{
    public class ConnectionProvider : IConnectionProvider
    {
        private readonly string _masterConnectionString;
        private readonly string _mainConnectionString;

        public ConnectionProvider(IConfiguration configuration)
        {
            _masterConnectionString = configuration.GetConnectionString("MasterConnection");
            _mainConnectionString = configuration.GetConnectionString("MainConnection");
        }

        public SqlConnection GetOpenDbConnection(Region? region = null)
        {
            string connectionString = region switch
            {
                Region.Master => _masterConnectionString,
                Region.Main => _mainConnectionString,
                _ => _masterConnectionString // Default to Master
            };

            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
