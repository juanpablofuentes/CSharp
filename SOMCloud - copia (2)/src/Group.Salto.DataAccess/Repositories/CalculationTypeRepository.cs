﻿using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Common;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Group.Salto.DataAccess.Repositories
{
    public class CalculationTypeRepository : BaseRepository<CalculationType>, ICalculationTypeRepository
    {
        public CalculationTypeRepository(IUnitOfWork uow) : base(uow)
        {
        }
        public IQueryable<CalculationType> GetAll()
        {
            return All();
        }

        public List<string> GetAllNamesById(IList<Guid> ids)
        {
            return Filter(x => ids.Contains(x.Id)).Select(x => x.Name).ToList();
        }

        public CalculationType GetById(Guid id)
        {
            return Find(x => x.Id == id);
        }

        public Dictionary<Guid, string> GetAllKeyValues()
        {
            return All()
                .OrderBy(o => o.Id)
                .ToDictionary(t => t.Id, t => t.Name);
        }
    
    }
}