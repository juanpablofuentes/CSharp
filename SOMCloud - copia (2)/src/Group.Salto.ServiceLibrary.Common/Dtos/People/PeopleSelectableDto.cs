using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.ServiceLibrary.Common.Dtos.People
{
    public class PeopleSelectableDto : BaseNameIdDto<int>
    {
        public string FirstSurname { get; set; }
        public bool IsResponsable { get; set; }
    }
}