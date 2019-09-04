using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Common;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Group.Salto.DataAccess.Repositories
{
    public class DamagedEquipmentRepository : BaseRepository<DamagedEquipment>, IDamagedEquipmentRepository
    {
        public DamagedEquipmentRepository(IUnitOfWork uow) : base(uow)
        {
        }
        public IQueryable<DamagedEquipment> GetAll()
        {
            return All();
        }

        public List<string> GetAllNamesById(IList<Guid> IdDamagedEquipment)
        {
            return Filter(x => IdDamagedEquipment.Contains(x.Id)).Select(x => x.Name).ToList();
        }

        public DamagedEquipment GetById(Guid id)
        {
            return Find(x => x.Id == id);
        }

        public Dictionary<Guid, string> GetAllKeyValues()
        {
            return All()
               .OrderBy(x => x.Id)
               .ToDictionary(t => t.Id, t => t.Name);
        }
    }
}