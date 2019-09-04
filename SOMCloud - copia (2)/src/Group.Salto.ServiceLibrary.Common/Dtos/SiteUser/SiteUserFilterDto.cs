namespace Group.Salto.ServiceLibrary.Common.Dtos.SiteUser
{
    public class SiteUserFilterDto : BaseFilterDto
    {
        public string Name { get; set; }
        public int SitesId { get; set; }
    }
}