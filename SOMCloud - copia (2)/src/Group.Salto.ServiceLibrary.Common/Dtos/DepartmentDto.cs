using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.ServiceLibrary.Common.Dtos
{
    public class DepartmentDto : BaseNameIdDto<int>
    {
        public bool HasPeopleAssigned { get; set; }
    }
}