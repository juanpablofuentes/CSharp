namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkCenter
{
    public class WorkCenterListDto : WorkCenterBaseDto
    {        
        public string Company { get; set; }
        public string ResponsableSelectedName { get; set; }
        public string MunicipalitySelectedName { get; set; }
        public bool HasPeopleAssigned { get; set; }
    }
}