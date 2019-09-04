using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Group.Salto.Entities.Tenant.QueryEntities;
using Microsoft.EntityFrameworkCore;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class ErpSystemInstanceQueryRepository : BaseRepository<ErpSystemInstanceQuery>, IErpSystemInstanceQueryRepository
    { 
        public ErpSystemInstanceQueryRepository(ITenantUnitOfWork uow) : base(uow) { }

        public Dictionary<int, string> GetAllKeyValues()
        {
            return All()
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.Name);
        }

        public ErpSystemInstanceQuery GetByIdAndName(int erpId, string name)
        {
            return Find(erp => erp.ErpSystemInstanceId == erpId && erp.Name == name);
        }

        public decimal GetItemPriceFromQuery(string sqlQuery, int itemId)
        {
            decimal result = 0;

            var connection = _uow.Context.Database.GetDbConnection();
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            var command = connection.CreateCommand();
            var param = command.CreateParameter();
            param.ParameterName = "@itemId";
            param.DbType = DbType.Int32;
            param.Direction = ParameterDirection.Input;
            param.Value = itemId;

            command.CommandText = sqlQuery;
            command.Parameters.Add(param);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result = (decimal) reader.GetDouble(0);
            }
            connection.Close();
            return result;
        }

        public IEnumerable<FieldMaterialForm> GetMaterialFormItemsFromPeople(string sqlQuery, int peopleId)
        {
            var author = _uow.Context.Query<FieldMaterialForm>().FromSql(sqlQuery, peopleId);
            return author;
        }
    }
}