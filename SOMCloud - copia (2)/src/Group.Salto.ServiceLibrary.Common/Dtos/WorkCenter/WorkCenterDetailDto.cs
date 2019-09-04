namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkCenter
{
    public class WorkCenterDetailDto : WorkCenterBaseDto
    {
        public string Address { get; set; }
        public int? CompanySelected { get; set; }
        public int? StateSelected { get; set; }
        public int? RegionSelected { get; set; }
        public int? CountrySelected { get; set; }
    }
}