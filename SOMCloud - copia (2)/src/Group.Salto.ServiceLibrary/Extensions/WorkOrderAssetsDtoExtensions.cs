using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class WorkOrderAssetsDtoExtensions
    {
        public static WorkOrderAssetsDto ToWorkOrderAssetsDto(this WorkOrders source)
        {
            WorkOrderAssetsDto result = null;
            if (source != null)
            {
                result = new WorkOrderAssetsDto()
                {
                    Id = source.Id,
                    InternalIdentifier = source.InternalIdentifier,
                    CreationDate = source.CreationDate.ToString(),
                    EndingDate = source.ActuationEndDate.ToString()
                };
            }
            return result;
        }

        public static IList<WorkOrderAssetsDto> ToWorkOrderAssetsListDto(this IList<WorkOrders> source)
        {
            return source?.MapList(x => x.ToWorkOrderAssetsDto());
        }

        public static AssetsDetailWorkOrderServicesDto ToAssetsDetailWorkOrderServicesDto(this WorkOrders source)
        {
            AssetsDetailWorkOrderServicesDto result = null;
            if (source != null)
            {
                result = new AssetsDetailWorkOrderServicesDto()
                {
                    Id = source.Id,
                    Observations = source.Observations,
                    Repair = source.TextRepair,
                    Services = source.Services?.ToList().ToAssetsDetailServicesListDto().ToList()
                };
            }
            return result;
        }

        public static AssetsDetailServicesDto ToAssetsDetailServicesDto(this Services source)
        {
            AssetsDetailServicesDto result = null;
            if (source != null)
            {
                result = new AssetsDetailServicesDto()
                {
                    ServiceId = source.Id.ToString(),
                    Status = source.PredefinedService.Name,
                    FormState = source.FormState,
                    DeliveryNumber = source.DeliveryNote,
                    ResponsibleName = source.PeopleResponsible?.FullName,
                    ExtraFieldsValues = source.ExtraFieldsValues.ToList().ToExtraFieldListDto(),
                };
            }
            return result;
        }

        public static IList<AssetsDetailServicesDto> ToAssetsDetailServicesListDto(this IList<Services> source)
        {
            return source?.MapList(x => x.ToAssetsDetailServicesDto());
        }
    }
}