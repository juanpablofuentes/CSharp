using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.PeoplePermissions;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.PeoplePermissions;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.PeoplePermissions
{
    public class PeoplePermissionsService : BaseService, IPeoplePermissionsService
    {
        private readonly IPeoplePermissionsRepository _peoplePermissionsRepository;

        public PeoplePermissionsService(ILoggingService logginingService, 
            IPeoplePermissionsRepository peoplePermissionsRepository) : base(logginingService)
        {
            _peoplePermissionsRepository = peoplePermissionsRepository ?? throw new ArgumentNullException($"{nameof(peoplePermissionsRepository)} is null ");
        }

        public IList<PeoplePermissionsDto> GetByPeopleId(int peopleId)
        {
            LogginingService.LogInfo($"PeoplePermissionsService -> GetByPeopleId");
            IList<PeoplePermissionsDto> result = _peoplePermissionsRepository.GetByPeopleId(peopleId)?.ToDto();

            return result;
        }
    }
}