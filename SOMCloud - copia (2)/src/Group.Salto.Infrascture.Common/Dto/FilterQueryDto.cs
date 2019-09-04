using Group.Salto.Common.Enums;

namespace Group.Salto.Infrastructure.Common.Dto
{
    public class FilterQueryDto
    {
        public string Name { get; set; }
        public ActiveEnum Active { get; set; }
    }
}