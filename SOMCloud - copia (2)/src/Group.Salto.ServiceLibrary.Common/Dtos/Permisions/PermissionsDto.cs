using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Permisions
{
    public class PermissionsDto : BaseNameIdDto<int>
    {
        public string Description { get; set; }
        public string Observations { get; set; }
    }
}
