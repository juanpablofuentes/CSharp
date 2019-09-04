using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class ServiceGaugesRepository : BaseRepository<WorkOrders>, IServiceGaugesRepository 
    {
        public ServiceGaugesRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }
        public List<List<DataBaseResultDto>> GetSelectSql (string sql, List<SqlParameter> parameterList)
        {
            List<List<DataBaseResultDto>> result = this.ExecuteRawSQL(sql, parameterList);
            return result;
        }
    }
}