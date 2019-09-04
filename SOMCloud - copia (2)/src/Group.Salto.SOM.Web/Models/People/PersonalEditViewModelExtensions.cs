using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.People;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.People
{
    public static class PersonalEditViewModelExtensions
    {
        public static PersonalEditViewModel ToPeopleViewModel(this PeopleDto source, int languageId)
        {
            PersonalEditViewModel people = null;
            if (source != null)
            {
                people = new PersonalEditViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Surname = source.Surname,
                    SecondSurname = source.SecondSurname,
                    DNI = source.DNI,
                    Email = source.Email,
                    Phone = source.Phone,
                    PhoneEX = source.PhoneEX,
                    Language = languageId,
                    UserConfigurationId = source.UserConfigurationId,
                    IsVisible = source.IsVisible
                };
            }

            return people;
        }

        public static PeopleDto ToDto(this PersonalEditViewModel peopleVM, WorkEditViewModel workVM, GeoLocalitzationEditViewModel geoVM)
        {
            PeopleDto people = null;
            if (peopleVM != null)
            {
                people = new PeopleDto()
                {
                    Id = peopleVM.Id,
                    Name = peopleVM.Name,
                    Surname = peopleVM.Surname,
                    SecondSurname = peopleVM.SecondSurname,
                    DNI = peopleVM.DNI,
                    Email = peopleVM.Email,
                    Phone = peopleVM.Phone,
                    PhoneEX = peopleVM.PhoneEX,
                    UserConfigurationId = peopleVM.UserConfigurationId,
                    IsVisible = peopleVM.IsVisible,
                    CompanyId = workVM.CompanyId,
                    CostKm = (double?)workVM.CostKm?.StringToDecimal(CultureInfo.InvariantCulture.TwoLetterISOLanguageName),
                    IsActive = workVM.IsActive,
                    ResponsibleId = workVM.ResponsibleId,
                    DepartmentId = workVM.DepartmentId,
                    PointRateId = workVM.PointRateId,
                    SubcontractId = workVM.SubcontractId,
                    SubcontractorResponsible = workVM.SubcontractorResponsible,
                    PriorityId = workVM.PriorityId,
                    DocumentationUrl = workVM.DocumentationUrl,
                    AnnualHours = workVM.AnnualHours,
                    ProjectId = workVM.ProjectId,
                    KnowledgeSelected = workVM.KnowledgeSelected.ToPeopleKnowledgeDto(),
                    WorkCenterId = workVM.WorkCenterId,
                    Latitude = (double?)geoVM.Latitude?.StringToDecimal(CultureInfo.InvariantCulture.TwoLetterISOLanguageName),
                    Longitude = (double?)geoVM.Longitude?.StringToDecimal(CultureInfo.InvariantCulture.TwoLetterISOLanguageName),
                    WorkRadiusKm = (double?)geoVM.WorkRadiusKm?.StringToDecimal(CultureInfo.InvariantCulture.TwoLetterISOLanguageName)
                };
            }

            return people;
        }

        public static PeopleDto ToPeopleDto(this PersonalEditViewModel peopleVM, WorkEditViewModel workVM, GeoLocalitzationEditViewModel geoVM)
        {
            return peopleVM?.ToDto(workVM, geoVM);
        }
    }
}