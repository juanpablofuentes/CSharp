using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Projects;
using Group.Salto.SOM.Web.Models.Projects;
using Group.Salto.SOM.Web.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ProjectDetailViewModelExtensions
    {
        public static ProjectsDetailViewModel ToViewModel(this ProjectsDetailsDto source)
        {
            ProjectsDetailViewModel result = null;
            if (source != null)
            {
                result = new ProjectsDetailViewModel();
                result.GenericDetailViewModel = new GenericDetailViewModel()
                {
                    Id = source.Id,
                    ContractId = source.ContractId,
                    FirstName = source.Name,
                    Serie = source.Serie,
                    Counter = source.Counter,
                    StartDate = source.StartDate,
                    EndDate = source.EndDate,
                    VisibilityPda = source.VisibilityPda,
                    ProjectManagerId = source.ProjectManagerId,
                    QueueId = source.QueueId,
                    IsActive = source.IsActive,
                    WOStatusestId = source.WorkOrderStatusesId,
                    ExtraFieldsCollectionId = source.CollectionsExtraFieldId,
                    ClosingCodesCollectionId = source.CollectionsClosureCodesId,
                    WOTypeCollectionId = source.CollectionsTypesWorkOrdersId,
                    WOCategoriesCollectionId = source.WorkOrderCategoriesCollectionId,
                    Observations = source.Observations,
                    BackOfficeManager = source.BackOfficeResponsible,
                    TechnicalSupport = source.TechnicalResponsible,
                    DefaultTechnicianCode = source.DefaultTechnicalCode,
                    ContactsSelected = source.ContactsSelected.ToContactsEditViewModel()
                };
                result.TechniciansSelected = source.TechnicalCodesSelected.ToTechniciansEditViewModel();
                result.PredefinedServicesSelected = source.PredefinedServicesSelected.ToPredefinedServicesEditViewModel();
            }
            return result;
        }

        public static ResultViewModel<ProjectsDetailViewModel> ToViewModel(this ResultDto<ProjectsDetailsDto> source)
        {
            var response = source != null ? new ResultViewModel<ProjectsDetailViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static ProjectsDetailsDto ToDto(this ProjectsDetailViewModel genericVM)
        {
            ProjectsDetailsDto result = null;
            if (genericVM != null)
            {
                result = new ProjectsDetailsDto()
                {
                    Id = genericVM.GenericDetailViewModel.Id,
                    ContractId = genericVM.GenericDetailViewModel.ContractId,
                    Name = genericVM.GenericDetailViewModel.FirstName,
                    Serie = genericVM.GenericDetailViewModel.Serie,
                    Counter = genericVM.GenericDetailViewModel.Counter,
                    StartDate = genericVM.GenericDetailViewModel.StartDate,
                    EndDate = genericVM.GenericDetailViewModel.EndDate,
                    VisibilityPda = genericVM.GenericDetailViewModel.VisibilityPda,
                    ProjectManagerId = genericVM.GenericDetailViewModel.ProjectManagerId,
                    QueueId = genericVM.GenericDetailViewModel.QueueId,
                    IsActive = genericVM.GenericDetailViewModel.IsActive,
                    WorkOrderStatusesId = genericVM.GenericDetailViewModel.WOStatusestId,
                    CollectionsExtraFieldId = genericVM.GenericDetailViewModel.ExtraFieldsCollectionId,
                    CollectionsClosureCodesId = genericVM.GenericDetailViewModel.ClosingCodesCollectionId,
                    CollectionsTypesWorkOrdersId = genericVM.GenericDetailViewModel.WOTypeCollectionId,
                    WorkOrderCategoriesCollectionId = genericVM.GenericDetailViewModel.WOCategoriesCollectionId,
                    Observations = genericVM.GenericDetailViewModel.Observations,
                    BackOfficeResponsible = genericVM.GenericDetailViewModel.BackOfficeManager,
                    TechnicalResponsible = genericVM.GenericDetailViewModel.TechnicalSupport,
                    DefaultTechnicalCode = genericVM.GenericDetailViewModel.DefaultTechnicianCode,
                    Permissions = genericVM.GenericDetailViewModel.Permissions.Items.ToDto(),
                    ContactsSelected = genericVM.GenericDetailViewModel.ContactsSelected.ToContactsContractsDto(),
                    TechnicalCodesSelected = genericVM.TechniciansSelected.ToTechnicalCodesDto(),
                    PredefinedServicesSelected = genericVM.PredefinedServicesSelected.ToPredefinedServicesDto()
                };
            }
            return result;
        }
    }
}