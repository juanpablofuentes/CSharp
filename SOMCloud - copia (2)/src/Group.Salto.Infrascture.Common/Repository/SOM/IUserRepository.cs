using Group.Salto.Entities;

namespace Group.Salto.Infrastructure.Common.Repository.SOM
{
    public interface IUserRepository : IRepository<Users>
    {
        bool DeleteUser(Users user);
    }
}