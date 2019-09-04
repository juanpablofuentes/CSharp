using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Origins
{
    public class OriginsDto : BaseNameIdDto<int>
    {
        public string Description { get; set; }
        public string Observations { get; set; }
        public int CanBeDeleted { get; set; }
        public bool IsDeleted { get; set; }
    }
}
