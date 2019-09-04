using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Entities.Tenant.ContentTranslations;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class ExtraFieldsRepository : BaseRepository<ExtraFields>, IExtraFieldsRepository
    {
        public ExtraFieldsRepository(ITenantUnitOfWork uow) : base(uow)
        {

        }

        public IQueryable<ExtraFields> GetAllWithIncludeTranslations()
        {
            return this.Filter(x => !x.IsDeleted, GetIncludeExtraFieldsTranslations());
        }

        public IQueryable<ExtraFields> GetAllByDelSystemWithIncludeTranslations(bool isSystem)
        {
            return this.Filter(x => x.DelSystem == isSystem, GetIncludeExtraFieldsTranslations()).OrderBy(x => x.Name);
        }

        public ExtraFields GetByIdWithIncludeTranslations(int id)
        {
            return Find(x => x.Id == id, GetIncludeExtraFieldsTranslations());
        }

        public IList<ExtraFields> GetByIds(IEnumerable<int> ids)
        {
            return this.Filter(x => ids.Contains(x.Id)).ToList();
        }

        public SaveResult<ExtraFields> CreateExtraFields(ExtraFields entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Create(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public ExtraFields GetByIdWithCollectionsServices(int id)
        {
            return this.Find(x => x.Id == id && !x.IsDeleted, GetIncludeExtraFieldsTranslationsAndCollectionAndService());
        }

        public SaveResult<ExtraFields> UpdateExtraFields (ExtraFields entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Update(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<ExtraFields> DeleteExtraFields(ExtraFields entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            var result = SaveChange(entity);
            return result;
        }

        private List<Expression<Func<ExtraFields, object>>> GetIncludeExtraFieldsTranslations()
        {
            return GetIncludesPredicate(new List<Type>() { typeof(ExtraFieldsTranslations) });
        }

        private List<Expression<Func<ExtraFields, object>>> GetIncludeExtraFieldsTranslationsAndCollectionAndService()
        {
            return GetIncludesPredicate(new List<Type>() { typeof(ExtraFieldsTranslations), typeof(CollectionsExtraFieldExtraField), typeof(ExtraFieldsValues) });
        }

        private List<Expression<Func<ExtraFields, object>>> GetIncludesPredicate(IList<Type> includeCollection)
        {
            var includesPredicate = new List<Expression<Func<ExtraFields, object>>>();
            foreach (var element in includeCollection)
            {
                if (element == typeof(ExtraFieldsTranslations))
                {
                    includesPredicate.Add(p => p.ExtraFieldsTranslations);
                }
                if (element == typeof(CollectionsExtraFieldExtraField))
                {
                    includesPredicate.Add(p => p.CollectionsExtraFieldExtraField);
                }
                if (element == typeof(ExtraFieldsValues))
                {
                    includesPredicate.Add(p => p.ExtraFieldsValues);
                }
            }
            return includesPredicate;
        }

        public ExtraFields GetExtraFieldFromName(string extraFieldName)
        {
            return Find(ef => ef.Name == extraFieldName);
        }
    }
}