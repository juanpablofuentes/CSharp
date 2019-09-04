namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkCenter
{
    public class WorkCenterBaseDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? MunicipalitySelected { get; set; }  
        public int? ResponsableSelected { get; set; }
    }
}