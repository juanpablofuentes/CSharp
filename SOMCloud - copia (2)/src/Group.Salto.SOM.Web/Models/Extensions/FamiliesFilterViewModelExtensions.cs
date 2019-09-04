using Group.Salto.ServiceLibrary.Common.Dtos.Families;
using Group.Salto.SOM.Web.Models.Families;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class FamiliesFilterViewModelExtensions
    {
        public static FamiliesFilterDto ToDto(this FamiliesFilterViewModel source)
        {
            FamiliesFilterDto result = null;
            if (source != null)
            {
                result = new FamiliesFilterDto()
                {
                    Name = source.Name,
                    Description = source.Description,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }

            return result;
        }
    }
}