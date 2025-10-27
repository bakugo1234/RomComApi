using System.Collections.Generic;
using System.Threading.Tasks;
using RomCom.Common.Enums;

namespace RomCom.Repository.Setup.Contract
{
    public interface IDbProvider
    {
        IEnumerable<T> ExecuteQuery<T>(string procedureName, object param = null, System.Data.CommandType commandType = System.Data.CommandType.StoredProcedure, Region? region = null);
        Task<IEnumerable<T>> ExecuteQueryAsync<T>(string procedureName, object param = null, System.Data.CommandType commandType = System.Data.CommandType.StoredProcedure, Region? region = null);
        T ExecuteFirst<T>(string procedureName, object param = null, System.Data.CommandType commandType = System.Data.CommandType.StoredProcedure, Region? region = null);
        Task<T> ExecuteFirstAsync<T>(string procedureName, object param = null, System.Data.CommandType commandType = System.Data.CommandType.StoredProcedure, Region? region = null);
        T ExecuteScalar<T>(string procedureName, object param = null, System.Data.CommandType commandType = System.Data.CommandType.StoredProcedure, Region? region = null);
        Task<T> ExecuteScalarAsync<T>(string procedureName, object param = null, System.Data.CommandType commandType = System.Data.CommandType.StoredProcedure, Region? region = null);
        Task<int> ExecuteAsync(string procedureName, object param = null, System.Data.CommandType commandType = System.Data.CommandType.StoredProcedure, Region? region = null);
    }
}
