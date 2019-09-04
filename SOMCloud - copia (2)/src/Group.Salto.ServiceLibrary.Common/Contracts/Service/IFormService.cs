using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Service;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.File;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Service
{
    public interface IFormService
    {
        IEnumerable<ServiceFilesDto> GetFilesFromService(int serviceId);
        WorkOrderServiceDto GetFormsWO(int Id, int FatherId, int GeneratedServiceId);
        List<RequestFileDto> GetFilesFromExtraFieldsValues(int Id);
        List<WorkOrderExtraFieldsValuesDto> GetExtraFieldsValues(int Id);
        ResultDto<IList<WorkOrderExtraFieldsValuesDto>> UpdateExtraFieldsValues(IList<WorkOrderExtraFieldsValuesDto> model, FormServiceDto formServiceDto);
    }
}