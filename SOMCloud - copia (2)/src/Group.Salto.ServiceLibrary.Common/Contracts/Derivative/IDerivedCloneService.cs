using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Derivative
{
    public interface IDerivedCloneService
    {
        ResultDto<Services> CreateService(Entities.Tenant.People peopleResponsible, string serviceFolder, string container, DerivedServices derivedService, int responsibleId);
        ResultDto<Services> CloneService(Entities.Tenant.People peopleResponsible, string serviceFolder, string container, Services service, WorkOrders workOrder);
        WorkOrders CreateWorkOrder(WorkOrdersDeritative derivedWo, WorkOrders wo);
    }
}
