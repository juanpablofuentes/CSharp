namespace Group.Salto.Infrastructure.Common.Dto
{
    public class AdvancedSearchDto: FilterQueryDto
    {
        public string Text { get; set; }
        public int SelectType { get; set; }
    }
}