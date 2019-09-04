using DataAccess.Common;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group.Salto.DataAccess.Repositories
{
    public class WorkFormRepository : BaseRepository<WorkForm>, IWorkFormRepository
    {
        public WorkFormRepository(IUnitOfWork uow) : base(uow){}

        public IQueryable<WorkForm> GetAll()
        {
            return All();
        }
    }
}