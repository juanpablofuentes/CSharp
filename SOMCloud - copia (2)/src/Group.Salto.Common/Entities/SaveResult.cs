namespace Group.Salto.Common
{
    public class SaveResult<TEntity>
    {
        public SaveResult(TEntity entity)
        {
            Entity = entity;
        }

        public bool IsOk { get; set; } = true;
        public TEntity Entity { get; set; }
        public SaveError Error { get; set; }
    }
}
