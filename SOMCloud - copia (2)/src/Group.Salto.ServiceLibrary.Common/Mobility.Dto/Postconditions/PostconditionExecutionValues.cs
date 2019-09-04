using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos;

namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.Postconditions
{
    public class PostconditionExecutionValues
    {
        public WorkOrders WorkOrder { get; set; }
        public Entities.Tenant.Postconditions Postcondition { get; set; }
        public ResultDto<bool> Result { get; set; }
    }
}
