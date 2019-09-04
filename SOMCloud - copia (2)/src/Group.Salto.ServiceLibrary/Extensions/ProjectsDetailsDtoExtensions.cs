using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.AdvancedSearch;
using Group.Salto.ServiceLibrary.Common.Dtos.Contacts;
using Group.Salto.ServiceLibrary.Common.Dtos.PredefinedServices;
using Group.Salto.ServiceLibrary.Common.Dtos.Projects;
using Group.Salto.ServiceLibrary.Common.Dtos.TechnicalCodes;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ProjectsDetailsDtoExtensions
    {
        public static ProjectsDetailsDto ToDetailDto(this Projects source)
        {
            ProjectsDetailsDto result = null;
            if (source != null)
            {
                result = new ProjectsDetailsDto()
                {
                    Id = source.Id,
                    ContractId = source.ContractId.HasValue ? source.ContractId.Value : 0,
                    Name = source.Name,
                    Serie = source.Serie,
                    Counter = source.Counter,
                    StartDate = source.StartDate,
                    EndDate = source.EndDate,
                    VisibilityPda = source.VisibilityPda,
                    ProjectManagerId = (source.PeopleProjects != null && source.PeopleProjects.Count > 0) ? source.PeopleProjects.FirstOrDefault().PeopleId : 0,
                    QueueId = source.QueuetId.HasValue ? source.QueuetId.Value : 0,
                    IsActive = source.IsActive,
                    WorkOrderStatusesId  = source.WorkOrderStatusesId.HasValue ? source.WorkOrderStatusesId.Value : 0,
                    CollectionsExtraFieldId = source.CollectionsExtraFieldId.HasValue ? source.CollectionsExtraFieldId.Value: 0,
                    CollectionsClosureCodesId = source.CollectionsClosureCodesId,
                    CollectionsTypesWorkOrdersId = source.CollectionsTypesWorkOrdersId,
                    WorkOrderCategoriesCollectionId = source.WorkOrderCategoriesCollectionId,
                    Observations = source.Observations,
                    DefaultTechnicalCode = source.DefaultTechnicalCode,
                    BackOfficeResponsible = source.BackOfficeResponsible,
                    TechnicalResponsible = source.TechnicalResponsible,
                    ContactsSelected = source.ProjectsContacts?.ToList()?.ToProjectsContactsDto(),
                    TechnicalCodesSelected = source.TechnicalCodes?.ToList()?.ToTechnicalCodesDto(),
                    PredefinedServicesSelected = source.PredefinedServices?.ToList().ToPredefinedServicesDto()
                };
            }
            return result;
        }

        public static AdvancedSearchDto ToAdvancedSearchDto(this Projects source)
        {
            AdvancedSearchDto result = null;
            if (source != null)
            {
                result = new AdvancedSearchDto
                {
                    Id = source.Id,
                    Name = source.Name,
                };
                result.Details.Add(source.Name);
                result.Details.Add(source.PeopleProjects?.FirstOrDefault()?.People?.FullName);
                result.Details.Add(source.Contract?.Object);
            }

            return result;
        }

        public static IList<AdvancedSearchDto> ToAdvancedSearchListDto(this IList<Projects> source)
        {
            return source?.MapList(x => x.ToAdvancedSearchDto());
        }

        public static Projects ToEntity(this ProjectsDetailsDto source)
        {
            Projects result = null;
            if (source != null)
            {
                result = new Projects()
                {
                    ContractId = source.ContractId != 0 ? source.ContractId : (int?)null,
                    Name = source.Name,
                    Serie = source.Serie,
                    Counter = source.Counter,
                    StartDate = source.StartDate,
                    EndDate = source.EndDate,
                    VisibilityPda = source.VisibilityPda,
                    IsActive = source.IsActive,
                    WorkOrderStatusesId = source.WorkOrderStatusesId != 0 ? source.WorkOrderStatusesId : (int?) null,
                    CollectionsExtraFieldId = source.CollectionsExtraFieldId != 0 ? source.CollectionsExtraFieldId : (int?)null,
                    CollectionsClosureCodesId = source.CollectionsClosureCodesId,
                    CollectionsTypesWorkOrdersId = source.CollectionsTypesWorkOrdersId,
                    WorkOrderCategoriesCollectionId = source.WorkOrderCategoriesCollectionId,
                    Observations = source.Observations,
                    DefaultTechnicalCode = source.DefaultTechnicalCode,
                    BackOfficeResponsible = source.BackOfficeResponsible,
                    TechnicalResponsible = source.TechnicalResponsible,
                    QueuetId = source.QueueId != 0 ? source.QueueId : (int?)null
                };
            }
            return result;
        }

        public static void UpdateProject(this Projects target, Projects source)
        {
            if (source != null && target != null)
            {
                target.ContractId = source.ContractId;
                target.Name = source.Name;
                target.Serie = source.Serie;
                target.Counter = source.Counter;
                target.StartDate = source.StartDate;
                target.EndDate = source.EndDate;
                target.VisibilityPda = source.VisibilityPda;
                target.IsActive = source.IsActive;
                target.WorkOrderStatusesId = source.WorkOrderStatusesId;
                target.CollectionsExtraFieldId = source.CollectionsExtraFieldId;
                target.CollectionsClosureCodesId = source.CollectionsClosureCodesId;
                target.CollectionsTypesWorkOrdersId = source.CollectionsTypesWorkOrdersId;
                target.WorkOrderCategoriesCollectionId = source.WorkOrderCategoriesCollectionId;
                target.Observations = source.Observations;
                target.DefaultTechnicalCode = source.DefaultTechnicalCode;
                target.BackOfficeResponsible = source.BackOfficeResponsible;
                target.TechnicalResponsible = source.TechnicalResponsible;
                target.QueuetId = source.QueuetId;
            }
        }

        public static Projects AddPeopleProjects(this ProjectsDetailsDto projectsDetailsDto, Projects localProject)
        {
            localProject.PeopleProjects = localProject.PeopleProjects ?? new List<PeopleProjects>();
            localProject.PeopleProjects.Add(new PeopleProjects() { PeopleId = projectsDetailsDto.ProjectManagerId, IsManager = true });
            return localProject;
        }

        public static Projects AssignPermissions(this Projects entity, IEnumerable<int> permissionIds)
        {
            entity.ProjectPermission?.Clear();
            if (permissionIds != null && permissionIds.Any())
            {
                entity.ProjectPermission = entity.ProjectPermission ?? new List<ProjectsPermissions>();
                foreach (var element in permissionIds)
                {
                    entity.ProjectPermission.Add(new ProjectsPermissions()
                    {
                        PermissionId = element,
                    });
                }
            }

            return entity;
        }

        public static Projects AssignProjectsContacts(this Projects entity, IList<ContactsDto> contacts)
        {
            entity.ProjectsContacts?.Clear();
            if (contacts != null && contacts.Any())
            {
                entity.ProjectsContacts = entity.ProjectsContacts ?? new List<ProjectsContacts>();
                IList<Contacts> localContacts = contacts.ToEntity();
                foreach (Contacts localContact in localContacts)
                {
                    entity.ProjectsContacts.Add(new ProjectsContacts()
                    {
                        Contact = localContact
                    });
                }
            }
            return entity;
        }

        public static Projects AssignTechnicalCodes(this Projects entity, IList<TechnicalCodesDto> technicalCodes)
        {
            entity.TechnicalCodes?.Clear();
            if (technicalCodes != null && technicalCodes.Any())
            {
                entity.TechnicalCodes = entity.TechnicalCodes ?? new List<TechnicalCodes>();
                IList<TechnicalCodes> localTechnicalCodes = technicalCodes.ToEntity();
                foreach (TechnicalCodes localTechnicalCode in localTechnicalCodes)
                {
                    entity.TechnicalCodes.Add(localTechnicalCode);
                }
            }
            return entity;
        }

        public static Projects AssignToAddPredefinedServices(this Projects entity, ProjectsDetailsDto projectsDetailsDto)
        {
            IList<PredefinedServicesDto> forInsert = projectsDetailsDto.PredefinedServicesSelected.Where(x => x.State == "C").ToList();
            if (forInsert != null && forInsert.Any())
            {
                entity.PredefinedServices = entity.PredefinedServices ?? new List<PredefinedServices>();
                IList<PredefinedServices> localPredefinedServices = forInsert.ToEntity();
                foreach (PredefinedServices row in localPredefinedServices)
                {
                    entity.PredefinedServices.Add(row);
                }
            }

            return entity;
        }

        public static Projects AssignToUpdatePredefinedServices(this Projects entity, ProjectsDetailsDto projectsDetailsDto)
        {
            IList<PredefinedServicesDto> forUpdate = projectsDetailsDto.PredefinedServicesSelected.Where(x => x.State == "U").ToList();
            if (forUpdate != null && forUpdate.Any())
            {
                foreach (PredefinedServices predefinedService in entity.PredefinedServices)
                {
                    PredefinedServicesDto localps = forUpdate.Where(x => x.Id == predefinedService.Id).FirstOrDefault();
                    if (localps != null)
                    { 
                        predefinedService.UpdatePredefinedServices(localps.ToEntity());
                    }
                }
            }

            return entity;
        }

        public static bool IsValid(this ProjectsDetailsDto source)
        {
            bool result = false;
            result = source != null;
            return result;
        }
    }
}