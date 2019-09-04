using System;
using System.Collections.Generic;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Analysis
{
    public interface IAnalysisService
    {
        ResultDto<bool> AddAllServicesToAnalize(WorkOrders currentWorkOrder, Entities.Tenant.People currentPeople);
        void AddOrUpdateServiceAnalysis(WorkOrders currentWorkOrder, Services currentService, Entities.Tenant.People currentPeople);
        DateTime? GetServiceClosingDate(Services currentService);
        List<ServiceFieldErrorEnum> ValidateServiceFields(WorkOrders currentWorkOrder, Services currentService);
        T GetFormValue<T>(ExtraFieldSystemTypeEnum extraFieldType, Services currentService);
    }
}
