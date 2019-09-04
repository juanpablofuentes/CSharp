namespace Group.Salto.ServiceLibrary.Common.Dtos.Assets
{
    public class AssetsLocationsDto
    {
        public int Id { get; set; }
        public int LocationClientId { get; set; }
        public string LocationName { get; set; }
        public int FinalClientId { get; set; }
        public string FinalClientName { get; set; }
        public int? SiteUserId { get; set; }
        public string SiteUserName { get; set; }
    }
}