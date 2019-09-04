using Group.Salto.Entities;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.SOM
{
    public interface IPostconditionTypesRepository
    {
        IQueryable<PostconditionTypes> GetAll();
        PostconditionTypes GetByName(string name);
    }
}