using Group.Salto.Common.Entities.Contracts;

namespace Group.Salto.Common.Entities
{
    public class SoftDeleteBaseEntity : BaseEntity, ISoftDelete
    {
        public bool IsDeleted { get; set; }
    }
}