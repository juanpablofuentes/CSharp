using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.People;
using Group.Salto.ServiceLibrary.Common.Dtos.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Group.Salto.ServiceLibrary.Common.Contracts.User
{
    public interface IAccessService
    {
        Task<ResultDto<AccessUserDto>> GetUserById(string userId);
        Task<List<ErrorDto>> ValidateAccessUser(AccessUserDto accessUser, Users user);
        Task CreateNewUser(GlobalPeopleDto globalPeople, ErrorsDto errors);
        Task UpdateUser(GlobalPeopleDto globalPeople, Users user, ErrorsDto errors);
        ResultDto<bool> DeleteUser(int userConfigurationId, ErrorsDto errors);
        Task SaveAccessUser(GlobalPeopleDto globalPeople, ErrorsDto errors);
        string GetHashPassword(Users user, string password);
    }
}