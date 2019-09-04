namespace DataAccess.Common.UoW
{
    public interface IExplicitCreation
    {
        void CreateInstance(string connectionString);
        void DestroyInstance();
    }
}