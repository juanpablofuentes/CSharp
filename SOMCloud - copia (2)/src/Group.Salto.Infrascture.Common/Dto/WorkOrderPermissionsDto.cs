namespace Group.Salto.Infrastructure.Common.Dto
{
    public class WorkOrderPermissionsDto
    {
        public int Id { get; set; }
        public int PeopleId { get; set; }
        public int[] Permissions { get; set; }
        public int[] SubContracts { get; set; }
    }
}