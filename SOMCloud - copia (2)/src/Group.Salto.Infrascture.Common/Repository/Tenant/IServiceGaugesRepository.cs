using Group.Salto.Infrastructure.Common.Dto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IServiceGaugesRepository 
    {
        List<List<DataBaseResultDto>> GetSelectSql(string sql, List<SqlParameter> parameterList);
    }
}