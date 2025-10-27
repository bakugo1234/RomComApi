using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using RomCom.Common.Enums;

namespace RomCom.Repository.Setup.Contract
{
    public interface IConnectionProvider
    {
        SqlConnection GetOpenDbConnection(Region? region = null);
    }
}
