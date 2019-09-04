using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
   public interface IToolsTypeRepository
    {
        IQueryable<ToolsType> GetAll();
        ToolsType GetById(int id);
        IQueryable<ToolsType> FilterById(IEnumerable<int> ids);
        SaveResult<ToolsType> CreateToolsType(ToolsType toolstype);
        SaveResult<ToolsType> DeleteToolsType(ToolsType entity);
        SaveResult<ToolsType> UpdateToolsType(ToolsType toolstype);
        Dictionary<int, string> GetAllKeyValues();
    }
}
