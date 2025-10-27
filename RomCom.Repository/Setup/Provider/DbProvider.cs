using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using RomCom.Common.Enums;
using System.Data;
using RomCom.Repository.Setup.Contract;

namespace RomCom.Repository.Setup.Provider
{
    public class DbProvider : IDbProvider
    {
        private readonly IConnectionProvider _connectionProvider;

        public DbProvider(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public IEnumerable<T> ExecuteQuery<T>(string procedureName, object param = null, CommandType commandType = CommandType.StoredProcedure, Region? region = null)
        {
            using var conn = _connectionProvider.GetOpenDbConnection(region);
            return conn.Query<T>(procedureName, param, commandType: commandType);
        }

        public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string procedureName, object param = null, CommandType commandType = CommandType.StoredProcedure, Region? region = null)
        {
            using var conn = _connectionProvider.GetOpenDbConnection(region);
            return await conn.QueryAsync<T>(procedureName, param, commandType: commandType);
        }

        public T ExecuteFirst<T>(string procedureName, object param = null, CommandType commandType = CommandType.StoredProcedure, Region? region = null)
        {
            using var conn = _connectionProvider.GetOpenDbConnection(region);
            return conn.QueryFirstOrDefault<T>(procedureName, param, commandType: commandType);
        }

        public async Task<T> ExecuteFirstAsync<T>(string procedureName, object param = null, CommandType commandType = CommandType.StoredProcedure, Region? region = null)
        {
            using var conn = _connectionProvider.GetOpenDbConnection(region);
            return await conn.QueryFirstOrDefaultAsync<T>(procedureName, param, commandType: commandType);
        }

        public T ExecuteScalar<T>(string procedureName, object param = null, CommandType commandType = CommandType.StoredProcedure, Region? region = null)
        {
            using var conn = _connectionProvider.GetOpenDbConnection(region);
            return conn.ExecuteScalar<T>(procedureName, param, commandType: commandType);
        }

        public async Task<T> ExecuteScalarAsync<T>(string procedureName, object param = null, CommandType commandType = CommandType.StoredProcedure, Region? region = null)
        {
            using var conn = _connectionProvider.GetOpenDbConnection(region);
            return await conn.ExecuteScalarAsync<T>(procedureName, param, commandType: commandType);
        }

        public async Task<int> ExecuteAsync(string procedureName, object param = null, CommandType commandType = CommandType.StoredProcedure, Region? region = null)
        {
            using var conn = _connectionProvider.GetOpenDbConnection(region);
            return await conn.ExecuteAsync(procedureName, param, commandType: commandType);
        }
    }
}
