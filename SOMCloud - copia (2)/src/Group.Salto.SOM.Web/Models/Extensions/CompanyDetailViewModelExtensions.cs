using System.Collections.Generic;
using System.Linq;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Company;
using Group.Salto.SOM.Web.Models.Company;
using Group.Salto.SOM.Web.Models.MultiCombo;
using Group.Salto.SOM.Web.Models.Result;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class CompanyDetailViewModelExtensions
    {
        public static ResultViewModel<CompanyDetailViewModel> ToCompanyDetailViewModel(this ResultDto<CompanyDetailDto> source)
        {
            var response = new ResultViewModel<CompanyDetailViewModel>()
            {
                Data = source.Data?.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel(),
            };
            return response;
        }

        public static CompanyDetailViewModel ToViewModel(this CompanyDetailDto source)
        {
            CompanyDetailViewModel result = null;
            if (source != null)
            {
                result = new CompanyDetailViewModel();
                source.ToViewModel(result);
                result.Departments = source.Departments.ToViewModel();
                result.WorkCentersSelected =
                    source.WorkCentersSelected.Select(x => new MultiComboViewModel<int, int>()
                    {
                        Value = x.Id,
                        Text = x.Name,
                        CanDelete = !x.IsLocked,
                    });
            }

            return result;
        }

        public static CompanyDetailDto ToDto(this CompanyDetailViewModel source)
        {
            CompanyDetailDto result = null;
            if (source != null)
            {
                result = new CompanyDetailDto();
                source.ToDto(result);
                result.Departments = source.DepartmentsSelected?.ToDto();
                result.WorkCentersSelected = source.WorkCentersSelected?.Select(x => new BaseNameIdDto<int>()
                {
                    Name = x.Text,
                    Id = x.Value,
                    IsLocked = !x.CanDelete
                });
            }

            return result;
        }

        public static bool HasValidDepartmentsSelected(this CompanyDetailViewModel source)
        {
            return source == null
                   || source.DepartmentsSelected == null
                   || source.DepartmentsSelected.All(x => !string.IsNullOrEmpty(x.Description));
        }
    }
}
