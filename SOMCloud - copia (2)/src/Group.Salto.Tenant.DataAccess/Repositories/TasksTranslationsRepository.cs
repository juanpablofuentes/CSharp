using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant.ContentTranslations;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class TasksTranslationsRepository: BaseRepository<TasksTranslations>, ITasksTranslationsRepository
    {
        public TasksTranslationsRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public TasksTranslations CreateTranslation(TasksTranslations entity)
        {
            Create(entity);           
            return entity;
        }

        public TasksTranslations UpdateTranslation(TasksTranslations entity)
        {
            Update(entity);
            return entity;
        }

        public TasksTranslations DeleteOnContext(TasksTranslations entity)
        {
            Delete(entity);
            return entity;
        }

    }
}