using Group.Salto.Common;
using Group.Salto.Entities.Tenant.ContentTranslations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface ITasksTranslationsRepository
    {
        TasksTranslations CreateTranslation(TasksTranslations entity);
        TasksTranslations UpdateTranslation(TasksTranslations entity);
        TasksTranslations DeleteOnContext(TasksTranslations entity);
    }
}