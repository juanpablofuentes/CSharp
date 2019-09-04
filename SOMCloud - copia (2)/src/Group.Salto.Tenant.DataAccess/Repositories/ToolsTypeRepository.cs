using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class ToolsTypeRepository : BaseRepository<ToolsType>, IToolsTypeRepository
    {
        public ToolsTypeRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<ToolsType> GetAll()
        {
            return Filter(x => !x.IsDeleted);
        }

        public ToolsType GetById(int id)
        {
            return DbSet.Include(x=>x.KnowledgeToolsType)
                            .ThenInclude(x=>x.Knowledge)
                        .Include(x=>x.ToolsToolTypes)
                        .SingleOrDefault(x=>x.Id == id);
        }

        public IQueryable<ToolsType> FilterById(IEnumerable<int> ids)
        {
            return Filter(x => ids.Any(k => k == x.Id));
        }

        public SaveResult<ToolsType> CreateToolsType(ToolsType toolstype)
        {
            toolstype.UpdateDate = DateTime.UtcNow;
            Create(toolstype);
            var result = SaveChange(toolstype);
            return result;
        }

        public SaveResult<ToolsType> UpdateToolsType(ToolsType toolstype)
        {
            toolstype.UpdateDate = DateTime.UtcNow;
            Update(toolstype);
            var result = SaveChange(toolstype);
            return result;
        }

        public SaveResult<ToolsType> DeleteToolsType(ToolsType entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            SaveResult<ToolsType> result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }
        public Dictionary<int, string> GetAllKeyValues()
        {
            return All()
                .Where(s => !s.IsDeleted)
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.Name);
        }
    }
}