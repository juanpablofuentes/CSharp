using Group.Salto.Controls.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos.People;
using Group.Salto.ServiceLibrary.Common.Dtos.PeopleVisible;
using Group.Salto.SOM.Web.Models.People;
using Group.Salto.SOM.Web.Models.PeopleVisible;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class PeopleVisibleFilterViewModelExtensions
    {
        public static PeopleVisibleFilterDto ToDto(this PeopleVisibleFilterViewModel source)
        {
            PeopleVisibleFilterDto result = null;
            if (source != null)
            {
                result = new PeopleVisibleFilterDto()
                {
                    Name = source.Name,
                    DepartmentId = (source.DepartmentId != 0) ? source.DepartmentId : (int?) null,
                    CompanyId = (source.CompanyId != 0) ? source.CompanyId : (int?)null,
                    KnowledgeId = (source.KnowledgeId != 0) ? source.KnowledgeId : (int?)null,
                    WorkCenterId = (source.WorkCenterId != 0) ? source.WorkCenterId : (int?)null,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy
                };
            }

            return result;
        }
    }
}