using DataAccess.Common.UoW;

namespace DataAccess.Common.Repositories
{
    public class ExplicitBaseRepository<TEntity> : BaseRepository<TEntity>, IExplicitCreation where TEntity : class
    {
        public ExplicitBaseRepository(IUnitOfWork uow) : base(uow) { }

        public void CreateInstance(string connectionString)
        {
            ((IExplicitCreation)this._uow).CreateInstance(connectionString);
        }

        public void DestroyInstance()
        {
            ((IExplicitCreation)this._uow).DestroyInstance();
        }
    }
}