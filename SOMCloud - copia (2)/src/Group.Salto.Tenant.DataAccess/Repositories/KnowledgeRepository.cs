using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class KnowledgeRepository : BaseRepository<Knowledge>, IKnowledgeRepository
    {
        public KnowledgeRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public SaveResult<Knowledge> DeleteKnowledge(Knowledge knowledge)
        {
            knowledge.UpdateDate = DateTime.UtcNow;
            Delete(knowledge);
            SaveResult<Knowledge> result = SaveChange(knowledge);
            result.Entity = knowledge;
            return result;
        }

        public IQueryable<Knowledge> FilterById(IEnumerable<int> ids)
        {
            return Filter(x => ids.Any(k => k == x.Id));
        }

        public IQueryable<Knowledge> GetAll()
        {
            return All();
        }

        public Knowledge GetById(int id)
        {
            return Find(x => x.Id == id);
        }

        public Knowledge GetByIdWithAllReferences(int id)
        {
            return Find(x => x.Id == id, GetIncludeAll());
        }

        public SaveResult<Knowledge> UpdateKnowledge(Knowledge knowledge)
        {
            knowledge.UpdateDate = DateTime.UtcNow;
            Update(knowledge);
            var result = SaveChange(knowledge);
            return result;
        }

        public SaveResult<Knowledge> CreateKnowledge(Knowledge knowledge)
        {
            knowledge.UpdateDate = DateTime.UtcNow;
            Create(knowledge);
            var result = SaveChange(knowledge);
            return result;
        }

        public Dictionary<int, string> GetAllKeyValues()
        {
            return Filter(x=>!x.IsDeleted)
                .Select(s => new { s.Id, s.Name })
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.Name);
        }

        private List<Expression<Func<Knowledge, object>>> GetIncludeAll()
        {
            return GetIncludesPredicate(new List<Type>() {  typeof(KnowledgePeople),
                                                            typeof(KnowledgeSubContracts),
                                                            typeof(KnowledgeToolsType),
                                                            typeof(WorkOrderCategoryKnowledge),
                                                            typeof(KnowledgeWorkOrderTypes)});
        }

        private List<Expression<Func<Knowledge, object>>> GetIncludesPredicate(IList<Type> includeCollection)
        {
            var includesPredicate = new List<Expression<Func<Knowledge, object>>>();
            foreach (var element in includeCollection)
            {
                if (element == typeof(KnowledgePeople))
                {
                    includesPredicate.Add(p => p.KnowledgePeople);
                }
                if (element == typeof(KnowledgeSubContracts))
                {
                    includesPredicate.Add(p => p.KnowledgeSubContracts);
                }
                if (element == typeof(KnowledgeToolsType))
                {
                    includesPredicate.Add(p => p.KnowledgeToolsType);
                }
                if (element == typeof(KnowledgeWorkOrderTypes))
                {
                    includesPredicate.Add(p => p.KnowledgeWorkOrderTypes);
                }
                if (element == typeof(WorkOrderCategoryKnowledge))
                {
                    includesPredicate.Add(p => p.WorkOrderCategoryKnowledge);
                }
            }
            return includesPredicate;
        }
    }
}