using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IPostconditionsCollectionRepository : IRepository<PostconditionCollections>
    {
        IQueryable<PostconditionCollections> GetAllByTaskId(int id);
        PostconditionCollections GetById(int id);
        SaveResult<PostconditionCollections> DeletePostconditionCollection(PostconditionCollections entity);
        SaveResult<PostconditionCollections> CreatePostconditionCollection(PostconditionCollections entity);

    }
}