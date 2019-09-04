using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.People;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.User
{
    public interface IUserService
    {
        ResultDto<IList<PeopleListDto>> GetAllFilteredByTenant(PeopleFilterDto filter, Guid tenantId);
        ResultDto<bool> DeleteUser(int userConfigurationId);
    }
}