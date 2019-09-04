using System;

namespace Group.Salto.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime UpdateDate { get; set; }
    }

    public class BaseEntity<TEntity> : BaseEntity
    {
        public new TEntity Id { get; set; }
    }
}
