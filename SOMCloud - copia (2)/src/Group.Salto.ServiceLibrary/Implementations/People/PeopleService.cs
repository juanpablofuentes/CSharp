using Group.Salto.Common;
using Group.Salto.Common.Constants.People;
using Group.Salto.Common.Enums;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.People;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.People;
using Group.Salto.ServiceLibrary.Common.Dtos.PeopleVisible;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.People
{
    public class PeopleService : BaseFilterService, IPeopleService
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly IKnowledgeRepository _knowledgeRepository;
        private readonly IProjectRepository _projectRepository;

        public PeopleService(ILoggingService logginingService,
                             IPeopleRepository peopleRepository,
                             IKnowledgeRepository knowledgeRepository,
                             IProjectRepository projectRepository,
                             IPeopleQueryFactory queryFactory)
            : base(queryFactory, logginingService)
        {
            _peopleRepository = peopleRepository ?? throw new ArgumentNullException($"{nameof(IPeopleRepository)} is null ");
            _knowledgeRepository = knowledgeRepository ?? throw new ArgumentNullException($"{nameof(IKnowledgeRepository)} is null ");
            _projectRepository = projectRepository ?? throw new ArgumentNullException($"{nameof(IProjectRepository)} is null ");
        }

        public ResultDto<PeopleDto> GetById(int Id)
        {
            LogginingService.LogInfo($"Get People by id {Id}");
            PeopleDto result = _peopleRepository.GetById(Id)?.ToDto();

            return ProcessResult(result, result != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<IList<PeopleListDto>> GetByIds(int[] Ids, Common.Dtos.People.PeopleFilterDto filter)
        {
            LogginingService.LogInfo($"Get Peoples by ids");
            PeopleFilterRepositoryDto repositoryFilter = new PeopleFilterRepositoryDto() { Ids = Ids, Name = filter.Name, Active = filter.Active };
            IList<PeopleListDto> result = _peopleRepository.GetByIds(repositoryFilter)?.ToListDto();
            return ProcessResult(result);
        }

        public ResultDto<IList<PeopleListDto>> GetListFiltered(Common.Dtos.People.PeopleFilterDto filter)
        {
            LogginingService.LogInfo($"Get Peoples filtered");

            var query = _peopleRepository.GetAllFiltered(filter.Name, filter.Active);
            query = OrderBy(query, filter);
            return ProcessResult(query.ToList().ToListDto());
        }

        public ResultDto<IList<PeopleVisibleListDto>> GetVisibleListFiltered(PeopleVisibleFilterDto filter)
        {
            PeopleVisibleFilterRepositoryDto repositoryFilter = new PeopleVisibleFilterRepositoryDto()
            {
                Name = filter.Name,
                CompanyId = filter.CompanyId,
                DeparmentId = filter.DepartmentId,
                KnowledgeId = filter.KnowledgeId,
                WorkCenterId = filter.WorkCenterId
            };

            List<Entities.Tenant.People> query = _peopleRepository.GetAllVisibleFiltered(repositoryFilter).ToList();
            IList<PeopleVisibleListDto> result = query.ToVisibleListDto().ToList();
            result = OrderBy(result, filter);

            return ProcessResult(result);
        }

        public ResultDto<PeopleDto> CreatePeople(GlobalPeopleDto globalPeople)
        {
            LogginingService.LogInfo($"Creating new People");
            List<ErrorDto> errors = new List<ErrorDto>();

            if (!ValidatePeople(globalPeople.PeopleDto, out var validations))
            {
                errors.AddRange(validations);
            }
            else
            {
                Entities.Tenant.People newPeople = globalPeople.PeopleDto.ToEntity();
                newPeople = AssignKnowledges(newPeople, globalPeople.PeopleDto.KnowledgeSelected);
                newPeople = AssignProject(newPeople, globalPeople.PeopleDto.ProjectId);

                var data = globalPeople.AccessUserDto.Permissions.Where(x => x.IsChecked).Select(x => Convert.ToInt32(x.Value));
                newPeople = AssignPermissions(newPeople, data);

                var result = _peopleRepository.CreatePeople(newPeople);
                return ProcessResult(result.Entity?.ToDto(), result);
            }

            return ProcessResult(globalPeople.PeopleDto, errors);
        }

        public ResultDto<PeopleDto> UpdatePeople(GlobalPeopleDto globalPeople)
        {
            LogginingService.LogInfo($"Update People");
            List<ErrorDto> errors = new List<ErrorDto>();

            if (!ValidatePeople(globalPeople.PeopleDto, out var validations))
            {
                errors.AddRange(validations);
            }
            else
            {
                Entities.Tenant.People localPeople = _peopleRepository.GetById(globalPeople.PeopleDto.Id);

                if (localPeople != null)
                {
                    ResultDto<PeopleDto> result = null;

                    if (localPeople != null)
                    {
                        localPeople.UpdatePeople(globalPeople.PeopleDto.ToEntity());
                        localPeople = AssignKnowledges(localPeople, globalPeople.PeopleDto.KnowledgeSelected);
                        localPeople = AssignProject(localPeople, globalPeople.PeopleDto.ProjectId);

                        var data = globalPeople.AccessUserDto.Permissions.Where(x => x.IsChecked).Select(x => Convert.ToInt32(x.Value));
                        localPeople = AssignPermissions(localPeople, data);

                        var resultRepository = _peopleRepository.UpdatePeople(localPeople);
                        result = ProcessResult(resultRepository.Entity?.ToDto(), resultRepository);
                    }
                }
                else
                {
                    errors.Add(new ErrorDto() { ErrorType = ErrorType.EntityAlredyExists, ErrorMessageKey = PeopleConstants.PeopleNotExist });
                }
            }

            return ProcessResult(globalPeople.PeopleDto, errors);
        }

        public ResultDto<PeopleDto> DeletePeople(int id)
        {
            LogginingService.LogInfo($"Delete People");
            List<ErrorDto> errors = new List<ErrorDto>();
            bool deleteResult = false;

            var peopleToDelete = _peopleRepository.GetByIdIncludeReferencesToDelete(id);

            if (peopleToDelete != null)
            {
                peopleToDelete.Clear();
                deleteResult = _peopleRepository.DeletePeople(peopleToDelete);
            }
            else
            {
                errors.Add(new ErrorDto() { ErrorType = ErrorType.EntityAlredyExists, ErrorMessageKey = PeopleConstants.PeopleNotExist });
            }

            return ProcessResult(peopleToDelete.ToDto(), errors);
        }

        public IList<BaseNameIdDto<int>> GetByCompanyIdKeyValues(int companyId, int Id)
        {
            LogginingService.LogInfo($"Get people by company (less you) Key Value");
            var data = _peopleRepository.GetByCompanyAllKeyValues(companyId, Id);

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value
            }).ToList();
        }
        
        public IList<BaseNameIdDto<int>> GetAllCommercialKeyValues()
        {
            LogginingService.LogInfo($"Get Commercial people Key Value");
            var data = _peopleRepository.GetAllCommercialKeyValues();

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value
            }).ToList();
        }

        public IList<BaseNameIdDto<int>> GetActiveByCompanyKeyValue(int? companyId)
        {
            LogginingService.LogInfo($"Get active people by company key value");
            var data = _peopleRepository.GetActiveByCompanyKeyValue(companyId.Value);
            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value
            }).ToList();
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValue()
        {
            LogginingService.LogInfo($"Get people key value");
            var data = _peopleRepository.GetAllKeyValue();
            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value
            }).ToList();
        }

        public IList<BaseNameIdDto<int>> GetAllActiveKeyValue()
        {
            LogginingService.LogInfo($"Get people key value");
            var data = _peopleRepository.GetAllActiveKeyValue();
            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value
            }).ToList();
        }

        public IList<BaseNameIdDto<int>> GetAllDriversKeyValue()
        {
            LogginingService.LogInfo($"Get active people that are drivers key value");
            var data = _peopleRepository.GetAllDriversFiltered();
            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value
            }).ToList();
        }

        public IList<BaseNameIdDto<int>> GetPeopleTechniciansKeyValues(Common.Dtos.People.PeopleFilterDto filter)
        {
            LogginingService.LogInfo($"Get people technicians key value");
            var data = _peopleRepository.GetPeopleByPermissionKeyValues(PermissionTypeEnum.Technical, new Infrastructure.Common.Dto.PeopleFilterDto() { Name = filter?.Name, IsVisible = filter?.IsVisible, SubcontractId = filter?.SubcontractId } );
            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value.Trim()
            }).ToList();
        }

        public IList<BaseNameIdDto<int>> GetPeopleManagerKeyValues(Common.Dtos.People.PeopleFilterDto filter)
        {
            LogginingService.LogInfo($"Get people manager key value");
            var data = _peopleRepository.GetPeopleByPermissionKeyValues(PermissionTypeEnum.Manager, new Infrastructure.Common.Dto.PeopleFilterDto() { Name = filter.Name });
            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value
            }).ToList();
        }

        public ResultDto<List<MultiSelectItemDto>> GetPeopleTechniciansMultiSelect(int userId, List<int> technicians)
        {
            LogginingService.LogInfo($"GetPeopleTechniciansMultiSelect");
            Entities.Tenant.People people = _peopleRepository.GetByConfigId(userId);
            IEnumerable<BaseNameIdDto<int>> allKeyValues = GetPeopleTechniciansKeyValues(new Common.Dtos.People.PeopleFilterDto() { IsVisible = true, SubcontractId = people.SubcontractId });
            return GetMultiSelect(allKeyValues, technicians);
        }

        public ResultDto<List<MultiSelectItemDto>> GetActivePeopleMultiSelect(List<int> selectItems)
        {
            LogginingService.LogInfo($"GetActivePeopleMultiSelect");
            IEnumerable<BaseNameIdDto<int>> allQueues = GetAllActiveKeyValue();
            return GetMultiSelect(allQueues, selectItems);
        }

        public string GetNamesComaSeparated(List<int> ids)
        {
            List<string> names = _peopleRepository.GetByPeopleIds(ids).Select(x => x.Name).ToList();
            return names.Aggregate((i, j) => $"{i}, {j}");
        }

        private IQueryable<Entities.Tenant.People> OrderBy(IQueryable<Entities.Tenant.People> query, Common.Dtos.People.PeopleFilterDto filter)
        {
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.FisrtSurname);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.SecondSurname);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Dni);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Telephone);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Email);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.IsActive);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.IsClientWorker);

            return query;
        }

        private List<PeopleVisibleListDto> OrderBy(IList<PeopleVisibleListDto> data, PeopleVisibleFilterDto filter)
        {
            var query = data.AsQueryable();
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.FisrtSurname);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.SecondSurname);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Email);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Extension);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Telephone);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Company);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Department);
            return query.ToList();
        }

        private Entities.Tenant.People AssignKnowledges(Entities.Tenant.People entity, IList<PeopleKnowledgeDto> knowledges)
        {
            entity.KnowledgePeople?.Clear();
            if (knowledges != null && knowledges.Any())
            {
                entity.KnowledgePeople = entity.KnowledgePeople ?? new List<Entities.Tenant.KnowledgePeople>();
                List<Entities.Tenant.Knowledge> localKnowledges = _knowledgeRepository.FilterById(knowledges.Select(x => x.Id)).ToList();
                foreach (Entities.Tenant.Knowledge localKnowledge in localKnowledges)
                {
                    entity.KnowledgePeople.Add(new Entities.Tenant.KnowledgePeople()
                    {
                        Knowledge = localKnowledge,
                        Maturity = knowledges.SingleOrDefault(x => x.Id == localKnowledge.Id)?.Priority ?? 0,
                    });
                }
            }
            return entity;
        }

        private Entities.Tenant.People AssignProject(Entities.Tenant.People entity, int? projectId)
        {
            entity.PeopleProjects?.Clear();
            if (projectId.HasValue && projectId.Value > 0)
            {
                entity.PeopleProjects = entity.PeopleProjects ?? new List<Entities.Tenant.PeopleProjects>();
                Entities.Tenant.Projects locaProject = _projectRepository.GetById(projectId.Value);

                entity.PeopleProjects.Add(new Entities.Tenant.PeopleProjects()
                {
                    IsManager = false,
                    Projects = locaProject
                });
            }
            return entity;
        }

        private Entities.Tenant.People AssignPermissions(Entities.Tenant.People entity, IEnumerable<int> PermissionIds)
        {
            entity.PeoplePermissions?.Clear();
            if (PermissionIds != null && PermissionIds.Any())
            {
                entity.PeoplePermissions = entity.PeoplePermissions ?? new List<Entities.Tenant.PeoplePermissions>();
                foreach (var element in PermissionIds)
                {
                    entity.PeoplePermissions.Add(new Entities.Tenant.PeoplePermissions()
                    {
                        PermissionId = element
                    });
                }
            }

            return entity;
        }

        private bool ValidatePeople(PeopleDto people, out List<ErrorDto> result)
        {
            result = new List<ErrorDto>();
            if (!people.IsValid())
            {
                result.Add(new ErrorDto()
                {
                    ErrorType = ErrorType.ValidationError,
                });
            }

            return !result.Any();
        }
    }
}