namespace Group.Salto.ServiceLibrary.Common.Dtos.SiteUser
{
    public class SiteUserDetailDto : SiteUserListDto
    {
        public string Position { get; set; }
        public int LocationId { get; set; }
    }
}