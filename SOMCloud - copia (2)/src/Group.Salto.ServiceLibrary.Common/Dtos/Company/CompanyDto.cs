using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Company
{
    public class CompanyDto : BaseNameIdDto<int>
    {
        public double? CostKm { get; set; }
        public bool HasPeopleAssigned { get; set; }
    }
}
