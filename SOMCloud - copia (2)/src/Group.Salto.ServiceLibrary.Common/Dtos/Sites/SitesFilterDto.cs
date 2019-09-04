namespace Group.Salto.ServiceLibrary.Common.Dtos.Sites
{
    public class SitesFilterDto : BaseFilterDto
    {
        public int finalClientId { get; set; }
        public string Name { get; set; }
    }
}