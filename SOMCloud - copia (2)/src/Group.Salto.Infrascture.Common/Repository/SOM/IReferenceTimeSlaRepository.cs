using Group.Salto.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group.Salto.Infrastructure.Common.Repository.SOM
{
    public interface IReferenceTimeSlaRepository
    {
        IQueryable<ReferenceTimeSla> GetAll();
    }
}