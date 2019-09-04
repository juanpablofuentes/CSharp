using DataAccess.Common;
using Group.Salto.Common;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.SOM;

namespace Group.Salto.DataAccess.Repositories
{
    public class UserRepository : BaseRepository<Users>, IUserRepository
    {
        public UserRepository(IUnitOfWork uow) : base(uow)
        {
        }
  
        public bool DeleteUser(Users user)
        {
            Delete(user);
            SaveResult<Users> result = SaveChange(user);
            result.Entity = user;

            return result.IsOk;
        }
    }
}