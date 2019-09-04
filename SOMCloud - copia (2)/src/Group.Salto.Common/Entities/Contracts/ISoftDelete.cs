namespace Group.Salto.Common.Entities.Contracts
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
