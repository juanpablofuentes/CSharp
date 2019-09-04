using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Query;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkCenter;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkCenter;
using Group.Salto.ServiceLibrary.Extensions;

namespace Group.Salto.ServiceLibrary.Implementations.WorkCenter
{
    public class WorkCenterService : BaseFilterService, IWorkCenterService
    {
        private readonly IWorkCenterRepository _workCenterRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IPeopleRepository _peopleRepository;

        public WorkCenterService(ILoggingService logginingService,
                                IWorkCenterRepository workCenterRepository,
                                ICompanyRepository companyRepository,
                                IWorkCenterQueryFactory queryFactory,
                                IPeopleRepository peopleRepository)
            : base(queryFactory, logginingService)
        {
            _workCenterRepository = workCenterRepository ?? throw new ArgumentNullException($"{nameof(IWorkCenterRepository)} is null ");
            _companyRepository = companyRepository ?? throw new ArgumentNullException($"{nameof(ICompanyRepository)} is null ");
            _peopleRepository = peopleRepository ?? throw new ArgumentNullException($"{nameof(IPeopleRepository)} is null ");
        }

        public ResultDto<IList<WorkCenterListDto>> GetAllFiltered(WorkCenterFilterDto filter)
        {
            LogginingService.LogInfo($"Get all work centers with includes available");
            var query = _workCenterRepository.GetAllAvailablesWithIncludes();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = OrderBy(query, filter);
            return ProcessResult(query.ToList().MapList(wc => wc.ToDto()));
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues(int companyId)
        {
            LogginingService.LogInfo($"Get Project Key Value");
            var data = _workCenterRepository.GetActiveByCompanyKeyValue(companyId);

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }

        public ResultDto<WorkCenterDetailDto> GetByIdWithPeopleCompaniesIncludes(int id)
        {
            LogginingService.LogInfo($"Get Companies by id {id}");
            var result = _workCenterRepository.GetByIdWithPeopleCompaniesIncludes(id)?.ToDetailDto();
            return ProcessResult(result, result != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<WorkCenterDetailDto> Create(WorkCenterDetailDto workCenterDetailDto)
        {
            LogginingService.LogInfo($"Create WorkCenters");
            var workCenter = workCenterDetailDto.ToEntity();
            AddCompanyAndPeople(workCenterDetailDto, workCenter);
            var resultSave = _workCenterRepository.CreateWorkCenter(workCenter);
            return ProcessResult(resultSave.Entity.ToDetailDto(), resultSave);
        }

        public ResultDto<WorkCenterDetailDto> Update(WorkCenterDetailDto workCenterDetailDto)
        {
            LogginingService.LogInfo($"Update WorkCenters with id = {workCenterDetailDto.Id}");
            ResultDto<WorkCenterDetailDto> result = null;
            var workCenter = _workCenterRepository.GetByIdWithPeopleCompaniesIncludes(workCenterDetailDto.Id);
            if (workCenter != null)
            {
                workCenter.Name = workCenterDetailDto.Name;
                workCenter.Address = workCenterDetailDto.Address;
                workCenter.CountryId = workCenterDetailDto.CountrySelected;
                workCenter.RegionId = workCenterDetailDto.RegionSelected;
                workCenter.StateId = workCenterDetailDto.StateSelected;
                workCenter.MunicipalityId = workCenterDetailDto.MunicipalitySelected;
                AddCompanyAndPeople(workCenterDetailDto, workCenter);
                var resultSave = _workCenterRepository.UpdateWorkCenter(workCenter);
                result = ProcessResult(resultSave.Entity?.ToDetailDto(), resultSave);
            }
            return result ?? new ResultDto<WorkCenterDetailDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = workCenterDetailDto,
            };
        }

        public ResultDto<bool> Delete(int id)
        {
            LogginingService.LogInfo($"Delete WorkCenter by id {id}");
            ResultDto<bool> result = null;
            var localWorkCenter = _workCenterRepository.GetById(id);
            if (localWorkCenter != null)
            {
                var resultSave = _workCenterRepository.DeleteWorkCenter(localWorkCenter);
                result = ProcessResult(resultSave.IsOk, resultSave);
            }
            return result ?? new ResultDto<bool>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = false,
            };
        }

        private WorkCenters AddCompanyAndPeople(WorkCenterDetailDto workCenterDetailDto, WorkCenters workCenter)
        {
            if (workCenterDetailDto.CompanySelected.HasValue)
            {
                workCenter.Company = _companyRepository.GetById(workCenterDetailDto.CompanySelected.Value);
            }
                
            if (workCenterDetailDto.ResponsableSelected.HasValue)
            {
                workCenter.People = _peopleRepository.GetByIdWithoutIncludes(workCenterDetailDto.ResponsableSelected.Value);
            }

            return workCenter;
        }

        private IQueryable<WorkCenters> OrderBy(IQueryable<WorkCenters> query, WorkCenterFilterDto filter)
        {
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            return query;
        }
    }
}