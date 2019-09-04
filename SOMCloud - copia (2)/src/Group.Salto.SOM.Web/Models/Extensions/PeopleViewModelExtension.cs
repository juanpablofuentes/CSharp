using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.People;
using Group.Salto.SOM.Web.Models.People;
using System.Collections.Generic;
using Group.Salto.SOM.Web.Models.MultiCombo;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class PeopleViewModelExtension
    {
        public static PeopleViewModel ToViewModel(this PeopleListDto source)
        {
            PeopleViewModel result = null;
            if (source != null)
            {
                result = new PeopleViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    FisrtSurname = source.FisrtSurname,
                    SecondSurname = source.SecondSurname,
                    Dni = source.Dni,
                    Email = source.Email,
                    Telephone = source.Telephone,
                    IsActive = source.IsActive,
                    IsClientWorker = source.IsClientWorker,
                    UserId = source.UserId,
                    UserName = source.UserName,
                    UserConfigurationId = source.UserConfigurationId
                };
            }

            return result;
        }

        public static IEnumerable<PeopleViewModel> ToViewModel(this IEnumerable<PeopleListDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }
    }
}