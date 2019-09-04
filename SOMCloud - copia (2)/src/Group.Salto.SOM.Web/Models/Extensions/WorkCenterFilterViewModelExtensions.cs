using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkCenter;
using Group.Salto.SOM.Web.Models.WorkCenter;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class WorkCenterFilterViewModelExtensions
    {
        public static WorkCenterFilterDto ToDto (this WorkCenterFilterViewModel source)
        {
            WorkCenterFilterDto result = null;
            if (source != null)
            {
                result = new WorkCenterFilterDto()
                {
                    Name = source.Name,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }
            return result ?? new WorkCenterFilterDto();
        }

        public static WorkCenterListViewModel ToViewModel(this WorkCenterListDto source)
        {
            WorkCenterListViewModel customer = null;
            if (source != null)
            {
                customer = new WorkCenterListViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Municipality = source.MunicipalitySelectedName,
                    Company = source.Company,
                    Responsable = source.ResponsableSelectedName,
                    HasPeopleAssigned = source.HasPeopleAssigned
                };
            }
            return customer;
        }

        public static IList<WorkCenterListViewModel> ToViewModel(this IList<WorkCenterListDto> source)
        {
            return source?.MapList(wc => wc.ToViewModel());
        }
    }
}