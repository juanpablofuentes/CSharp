using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.PredefinedServices;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class PredefinedServicesDtoExtensions
    {
        public static PredefinedServiceDto ToDto(this PredefinedServices dbModel)
        {
            var dto = new PredefinedServiceDto
            {
                Id = dbModel.Id,
                Name = dbModel.Name,
                Billable = dbModel.Billable,
                MustValidate = dbModel.MustValidate
            };

            return dto;
        }

        public static IEnumerable<PredefinedServiceDto> ToDto(this IEnumerable<PredefinedServices> dbModelList)
        {
            var dtoList = new List<PredefinedServiceDto>();
            foreach (var dbModel in dbModelList)
            {
                dtoList.Add(dbModel.ToDto());
            }

            return dtoList;
        }

        public static PredefinedServiceTaskDto ToPredefinedServiceTaskDto(this PredefinedServices dbModel)
        {
            var dto = new PredefinedServiceTaskDto
            {
                Id = dbModel.Id,
                Name = dbModel.Name,
                ClosingCodeIsMandatory = dbModel.LinkClosingCode,
                CollectionsExtraField = dbModel.CollectionExtraField?.ToCollectionsExtraFieldDto() ?? new TaskCollectionsServiceExtraFieldDto{ExtraFieldValues = new List<ExtendedExtraFieldValueDto>()},
                ClosingCodes = dbModel.Project?.CollectionsClosureCodes?.ClosingCodes?.Where(cc => cc.ClosingCodesFatherId == null).ToClosingCodesApiDto()
            };

            return dto;
        }

        public static IEnumerable<PredefinedServiceTaskDto> ToPredefinedServiceTaskDto(this IEnumerable<PredefinedServices> dbModelList)
        {
            var dtoList = new List<PredefinedServiceTaskDto>();
            foreach (var dbModel in dbModelList)
            {
                dtoList.Add(dbModel.ToPredefinedServiceTaskDto());
            }

            return dtoList;
        }

        public static PredefinedServicesDto ToPredefinedServicesDto(this PredefinedServices source)
        {
            PredefinedServicesDto result = null;
            if (source != null)
            {
                result = new PredefinedServicesDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    LinkClosingCode = source.LinkClosingCode,
                    Billable = source.Billable.HasValue ? source.Billable.Value: false,
                    MustValidate = source.MustValidate.HasValue ? source.MustValidate.Value : false,
                    ExtraFieldCollectionId = source.CollectionExtraFieldId ?? null,
                    ExtraFieldCollectionName = source.CollectionExtraField?.Name,
                    PermissionsString = source.PredefinedServicesPermission?.ToList().ToPredefinedServicesString(),
                    PermissionsIds = source.PredefinedServicesPermission?.ToList().ToPredefinedServicesIds(),
                };
            }

            return result;
        }

        public static IList<PredefinedServicesDto> ToPredefinedServicesDto(this IList<PredefinedServices> source)
        {
            return source?.MapList(cc => cc.ToPredefinedServicesDto());
        }

        public static PredefinedServices ToEntity(this PredefinedServicesDto source)
        {
            PredefinedServices result = null;
            if (source != null)
            {
                result = new PredefinedServices()
                {
                    Name = source.Name,
                    CollectionExtraFieldId = (source.ExtraFieldCollectionId.HasValue && source.ExtraFieldCollectionId.Value != 0) ? source.ExtraFieldCollectionId.Value : (int?) null,
                    LinkClosingCode = source.LinkClosingCode,
                    Billable = source.Billable,
                    MustValidate = source.MustValidate,
                };
                result.AssignPredefinedServicePermissions(source.PermissionsIds);
            }

            return result;
        }

        public static IList<PredefinedServices> ToEntity(this IList<PredefinedServicesDto> source)
        {
            return source?.MapList(e => e.ToEntity());
        }

        public static void UpdatePredefinedServices(this PredefinedServices target, PredefinedServices source)
        {
            if (source != null && target != null)
            {
                target.Name = source.Name;
                target.CollectionExtraFieldId = source.CollectionExtraFieldId;
                target.LinkClosingCode = source.LinkClosingCode;
                target.Billable = source.Billable;
                target.MustValidate = source.MustValidate;
                target.PredefinedServicesPermission = source.PredefinedServicesPermission;
            }
        }

        public static void AssignPredefinedServicePermissions(this PredefinedServices entity, IEnumerable<int> permissionIds)
        {
            entity.PredefinedServicesPermission?.Clear();
            if (permissionIds != null && permissionIds.Any())
            {
                entity.PredefinedServicesPermission = entity.PredefinedServicesPermission ?? new List<PredefinedServicesPermission>();
                foreach (var element in permissionIds)
                {
                    entity.PredefinedServicesPermission.Add(new PredefinedServicesPermission()
                    {
                        PermissionId = element,
                    });
                }
            }
        }
    }
}