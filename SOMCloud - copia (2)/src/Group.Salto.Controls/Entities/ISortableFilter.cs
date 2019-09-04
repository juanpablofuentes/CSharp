namespace Group.Salto.Controls.Entities
{
    public interface ISortableFilter
    {
        string OrderBy { get; set; }
        bool Asc { get; set; }
    }
}
