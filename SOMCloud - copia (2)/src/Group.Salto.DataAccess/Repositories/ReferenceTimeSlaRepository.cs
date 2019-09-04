using DataAccess.Common;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group.Salto.DataAccess.Repositories
{
    public class ReferenceTimeSlaRepository : BaseRepository<ReferenceTimeSla>, IReferenceTimeSlaRepository
    {
        public ReferenceTimeSlaRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<ReferenceTimeSla> GetAll()
        {
            return All();
        } 
    }
}