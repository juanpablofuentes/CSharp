using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.People;
using Group.Salto.ServiceLibrary.Common.Dtos.PeopleVisible;
using Group.Salto.SOM.Web.Models.People;
using Group.Salto.SOM.Web.Models.PeopleVisible;
using Group.Salto.SOM.Web.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class PeopleVisibleViewModelExtension
    {
        public static PeopleVisibleViewModel ToViewModel(this PeopleVisibleListDto source)
        {
            PeopleVisibleViewModel result = null;
            if (source != null)
            {
                result = new PeopleVisibleViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    FisrtSurname = source.FisrtSurname,
                    SecondSurname = source.SecondSurname,
                    Email = source.Email,
                    Telephone = source.Telephone,
                    Extension = source.Extension,
                    Company = source.Company,
                    Department = source.Department
                };
            }

            return result;
        }

        public static IList<PeopleVisibleViewModel> ToViewModel(this IList<PeopleVisibleListDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }
    }
}