using Group.Salto.Common;
using Group.Salto.Common.Helpers;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkCenter;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkCenter;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.WorkCenter
{
    public class WorkCenterAdapter : BaseService, IWorkCenterAdapter
    {
        private IMunicipalityRepository _municipalityRepository;
        private IPeopleRepository _peopleRepository;
        private IWorkCenterService _workCenterService;
        

        public WorkCenterAdapter(ILoggingService logginingService,
            IMunicipalityRepository municipalityRepository,
            IPeopleRepository peopleRepository,
            IWorkCenterService workCenterService)
            : base(logginingService)
        {
            _municipalityRepository = municipalityRepository ?? throw new ArgumentNullException($"{nameof(IMunicipalityRepository)} is null");
            _peopleRepository = peopleRepository ?? throw new ArgumentNullException($"{nameof(IPeopleRepository)} is null");
            _workCenterService = workCenterService ?? throw new ArgumentNullException($"{nameof(IWorkCenterService)} is null");
        }

        public ResultDto<IList<WorkCenterListDto>> GetList(WorkCenterFilterDto filter)
        {
            ResultDto<IList<WorkCenterListDto>> workCentersFiltered = _workCenterService.GetAllFiltered(filter);
            Dictionary<int, string> municipalities = _municipalityRepository.GetAllKeyValues();
            Dictionary<int, string> people = _peopleRepository.GetAllKeyValue();

            IEnumerable<WorkCenterListDto> workCenterWithMunicipalitiesList = workCentersFiltered.Data.GroupJoin(
            municipalities,
            wc => wc.MunicipalitySelected,
            m => m.Key,
            (wc, mp) => mp
                .Select(m => new WorkCenterListDto { Id = wc.Id, Name = wc.Name, Company = wc.Company, ResponsableSelected = wc.ResponsableSelected, MunicipalitySelectedName = m.Value, HasPeopleAssigned = wc.HasPeopleAssigned})
                .DefaultIfEmpty(new WorkCenterListDto { Id = wc.Id, Name = wc.Name, Company = wc.Company, ResponsableSelected = wc.ResponsableSelected, MunicipalitySelectedName = string.Empty, HasPeopleAssigned = wc.HasPeopleAssigned }))
            .SelectMany(g => g);

            List<WorkCenterListDto> workCenterWithResponsablesList = workCenterWithMunicipalitiesList.GroupJoin(
            people,
            wc => wc.ResponsableSelected,
            m => m.Key,
            (wc, mp) => mp
                .Select(m => new WorkCenterListDto { Id = wc.Id, Name = wc.Name, Company = wc.Company, ResponsableSelectedName = m.Value, MunicipalitySelectedName = wc.MunicipalitySelectedName, HasPeopleAssigned = wc.HasPeopleAssigned })
                .DefaultIfEmpty(new WorkCenterListDto { Id = wc.Id, Name = wc.Name, Company = wc.Company, ResponsableSelectedName = string.Empty, MunicipalitySelectedName = wc.MunicipalitySelectedName, HasPeopleAssigned = wc.HasPeopleAssigned }))
            .SelectMany(g => g).ToList();

            return ProcessResult(workCenterWithResponsablesList.MapList(wc => wc));
        }

        public ResultDto<WorkCenterDetailDto> GetById(int id)
        {
            ResultDto<WorkCenterDetailDto> workCenter = _workCenterService.GetByIdWithPeopleCompaniesIncludes(id);
            var municipalitiesIds = _municipalityRepository.GetByIdWithStatesRegionsCountriesIncludes(workCenter.Data.MunicipalitySelected.Value).ToIdsDto();

            WorkCenterDetailDto result = new WorkCenterDetailDto
            {
                Id = workCenter.Data.Id,
                Name = workCenter.Data.Name,
                Address = workCenter.Data.Address,
                CompanySelected = workCenter.Data.CompanySelected,
                ResponsableSelected = workCenter.Data.ResponsableSelected,
                MunicipalitySelected = municipalitiesIds?.MunicipalityId,
                StateSelected = workCenter.Data?.StateSelected,
                RegionSelected = workCenter.Data?.RegionSelected,
                CountrySelected = workCenter.Data?.CountrySelected
            };

            return ProcessResult(result, result != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }
    }
}