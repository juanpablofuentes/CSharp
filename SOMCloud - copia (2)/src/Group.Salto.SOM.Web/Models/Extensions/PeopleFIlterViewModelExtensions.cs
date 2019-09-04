using Group.Salto.Controls.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos.People;
using Group.Salto.SOM.Web.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class PeopleFIlterViewModelExtensions
    {
        public static PeopleFilterDto ToDto(this PeopleFilterViewModel source)
        {
            PeopleFilterDto result = null;
            if (source != null)
            {
                result = new PeopleFilterDto()
                {
                    Name = source.Name,
                    Active = source.Active,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                    Page = source.Page,
                    Size = source.Size,
                    ExportAllToExcel = source.ExportAllToExcel,
                };
            }
            return result;
        }
    }
}