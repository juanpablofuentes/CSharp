namespace Group.Salto.Controls.Table.Filter
{
    public interface IPagedFilter
    {
        int Page { get; set; }
        int Size { get; set; }
        int PagesCount { get; }
        int TotalValues { get; set; }
    }
}
