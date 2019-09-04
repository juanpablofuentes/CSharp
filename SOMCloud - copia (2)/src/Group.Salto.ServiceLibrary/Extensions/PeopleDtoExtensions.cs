using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.People;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class PeopleDtoExtensions
    {
        public static PeopleDto ToDto(this People source)
        {
            PeopleDto result = null;
            if (source != null)
            {
                result = new PeopleDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Surname = source.FisrtSurname,
                    SecondSurname = source.SecondSurname,
                    DNI = source.Dni,
                    Email = source.Email,
                    Phone = source.Telephone,
                    PhoneEX = source.Extension,
                    UserConfigurationId = source.UserConfigurationId,
                    IsVisible = source.IsVisible,
                    CompanyId = source.CompanyId,
                    ResponsibleId = source.ResponsibleId,
                    DepartmentId = source.DepartmentId,
                    PointRateId = source.PointsRateId,
                    SubcontractId = source.SubcontractId,
                    SubcontractorResponsible = source.SubcontractorResponsible.HasValue ? source.SubcontractorResponsible.Value : false,
                    ProjectId = source.PeopleProjects?.FirstOrDefault()?.ProjectId,
                    CostKm = source.CostKm,
                    DocumentationUrl = source.DocumentationUrl,
                    PriorityId = source.Priority,
                    IsActive = source.IsActive.HasValue ? source.IsActive.Value : false,
                    AnnualHours = source.AnnualHours,
                    KnowledgeSelected = source.KnowledgePeople?.ToList()?.ToKnowledgePeopleDto(),
                    WorkCenterId = source.WorkCenterId,
                    Latitude = source.Latitude,
                    Longitude = source.Longitude,
                    WorkRadiusKm = source.WorkRadiusKm,
                    Company = source.Company.ToDto()
                };
            }

            return result;
        }

        public static IList<PeopleDto> ToDto(this IList<People> source)
        {
            return source?.MapList(c => c.ToDto());
        }

        public static IEnumerable<PeopleDto> ToDto(this IEnumerable<People> source)
        {
            return source?.MapList(c => c.ToDto());
        }

        public static People ToEntity(this PeopleDto source)
        {
            People result = null;
            if (source != null)
            {
                result = new People()
                {
                    Id = source.Id,
                    Name = source.Name,
                    FisrtSurname = source.Surname,
                    SecondSurname = source.SecondSurname,
                    Dni = source.DNI,
                    Email = source.Email,
                    Telephone = source.Phone,
                    Extension = source.PhoneEX,
                    IsVisible = source.IsVisible,
                    IsActive = source.IsActive,
                    CompanyId = (source.CompanyId.HasValue && source.CompanyId.Value != 0) ? source.CompanyId.Value : (int?)null,
                    ResponsibleId = (source.ResponsibleId.HasValue && source.ResponsibleId.Value != 0) ? source.ResponsibleId.Value : (int?)null,
                    DepartmentId = (source.DepartmentId.HasValue && source.DepartmentId.Value != 0) ? source.DepartmentId.Value : (int?)null,
                    PointsRateId = (source.PointRateId.HasValue && source.PointRateId.Value != 0) ? source.PointRateId.Value : (int?)null,
                    CostKm = source.CostKm,
                    SubcontractId = (source.SubcontractId.HasValue && source.SubcontractId.Value != 0) ? source.SubcontractId.Value : (int?)null,
                    SubcontractorResponsible = source.SubcontractorResponsible,
                    DocumentationUrl = source.DocumentationUrl,
                    Priority = source.PriorityId.HasValue ? source.PriorityId.Value : 1,
                    AnnualHours = source.AnnualHours,
                    WorkCenterId = (source.WorkCenterId.HasValue && source.WorkCenterId.Value != 0) ? source.WorkCenterId.Value : (int?)null,
                    Latitude = source.Latitude,
                    Longitude = source.Longitude,
                    WorkRadiusKm = source.WorkRadiusKm
                };

                result.IsClientWorker = result.CompanyId.HasValue ? 1 : 0;

                if (source.UserConfigurationId != null)
                {
                    result.UserConfigurationId = source.UserConfigurationId;
                }
            }

            return result;
        }

        public static void UpdatePeople(this People target, People source)
        {
            if (source != null && target != null)
            {
                target.Name = source.Name;
                target.FisrtSurname = source.FisrtSurname;
                target.SecondSurname = source.SecondSurname;
                target.Dni = source.Dni;
                target.Email = source.Email;
                target.Telephone = source.Telephone;
                target.Extension = source.Extension;
                target.IsVisible = source.IsVisible;
                target.CompanyId = source.CompanyId;
                target.ResponsibleId = source.ResponsibleId;
                target.DepartmentId = source.DepartmentId;
                target.PointsRateId = source.PointsRateId;
                target.CostKm = source.CostKm;
                target.IsActive = source.IsActive;
                target.SubcontractId = source.SubcontractId;
                target.SubcontractorResponsible = source.SubcontractorResponsible;
                target.DocumentationUrl = source.DocumentationUrl;
                target.Priority = source.Priority;
                target.AnnualHours = source.AnnualHours;
                target.IsClientWorker = source.IsClientWorker;
                target.WorkCenterId = source.WorkCenterId;
                target.Latitude = source.Latitude;
                target.Longitude = source.Longitude;
                target.WorkRadiusKm = source.WorkRadiusKm;
            }
        }

        public static bool IsValid(this PeopleDto source)
        {
            bool result = false;

            result = source != null && !string.IsNullOrEmpty(source.Name);

            if (result && !string.IsNullOrEmpty(source.DNI))
            {
                result = ValidationsHelper.ValidateNIF(source.DNI);
                if (!result)
                    result = ValidationsHelper.ValidateNIE(source.DNI);
            }

            if (result && !string.IsNullOrEmpty(source.Email))
            {
                result = ValidationsHelper.IsEmailValid(source.Email);
            }

            if (result && source.CompanyId != 0 && source.SubcontractId != 0)
            {
                result = false;
            }

            if (result && source.CompanyId == 0 && source.SubcontractId == 0)
            {
                result = false;
            }
            return result;
        }

        public static void Clear(this People source)
        {
            //TODO Review all references must be deleted
            source?.PeopleCollectionsAdmins?.Clear();
            source?.KnowledgePeople?.Clear();
            source?.PeopleCollectionsPeople?.Clear();
            source?.PeoplePermissions?.Clear();
            source?.Vehicles?.Clear();
        }
    }
}