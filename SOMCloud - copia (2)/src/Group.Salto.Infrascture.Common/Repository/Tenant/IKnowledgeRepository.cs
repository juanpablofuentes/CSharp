using System.Collections.Generic;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IKnowledgeRepository : IRepository<Knowledge>
    {
        IQueryable<Knowledge> GetAll();
        Knowledge GetById(int id);
        SaveResult<Knowledge> CreateKnowledge(Knowledge knowledge);
        SaveResult<Knowledge> UpdateKnowledge(Knowledge knowledge);
        SaveResult<Knowledge> DeleteKnowledge(Knowledge knowledge);
        IQueryable<Knowledge> FilterById(IEnumerable<int> ids);
        Dictionary<int, string> GetAllKeyValues();
        Knowledge GetByIdWithAllReferences(int id);
    }
}