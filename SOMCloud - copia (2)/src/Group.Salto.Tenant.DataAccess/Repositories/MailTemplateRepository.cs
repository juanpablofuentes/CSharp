using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class MailTemplateRepository : BaseRepository<MailTemplate>, IMailTemplateRepository
    {
        public MailTemplateRepository(ITenantUnitOfWork uow) : base(uow) { }
        
        public IQueryable<MailTemplate> GetAll()
        {
            return All();
        }

        public Dictionary<int, string> GetAllKeyValues()
        {
            return All().OrderBy(m => m.Id).ToDictionary(t => t.Id, t => t.Name);
        }
        
        public SaveResult<MailTemplate> CreateMailTemplate(MailTemplate entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Create(entity);
            var result = SaveChange(entity);
            return result;
        }

        public MailTemplate GetById(int id)
        {
            return Filter(x => x.Id == id)
                        .SingleOrDefault();
        }

        public SaveResult<MailTemplate> UpdateMailTemplate(MailTemplate entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Update(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<MailTemplate> DeleteMailTemplate(MailTemplate entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            SaveResult<MailTemplate> result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public MailTemplate GetByIdIncludeReferencesToDelete(int Id)
        {
            return Filter(p => p.Id == Id)
                   .Include(x => x.Tasks)
                   .SingleOrDefault();
        }

        public MailTemplate GetByIdCanDelete(int Id)
        {
            return Filter(p => p.Id == Id)
                    .Include(x => x.Tasks)
                    .SingleOrDefault();
        }

        public IQueryable<MailTemplate> GetAllByFilters(FilterQueryDto filterQuery)
        {
            IQueryable<MailTemplate> query = All();
            query = FilterQuery(filterQuery, query);
            return query;
        }
        private IQueryable<MailTemplate> FilterQuery(FilterQueryDto filterQuery, IQueryable<MailTemplate> query)
        {
            if (!string.IsNullOrEmpty(filterQuery.Name))
            {
                query = query.Where(x => x.Name.Contains(filterQuery.Name));
            }
            return query;
        }
    }
}