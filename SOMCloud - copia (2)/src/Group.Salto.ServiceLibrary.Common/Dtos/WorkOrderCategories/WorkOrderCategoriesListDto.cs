namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategories
{
    public class WorkOrderCategoriesListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public double EstimatedDuration { get; set; }
    }
}