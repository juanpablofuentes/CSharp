using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.ClosingCode;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.ClosingCode;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ClosingCodeDtoExtensions
    {
        public static ClosingCodes ToEntity(this ClosingCodeDto source, CollectionsClosureCodes closureCode)
        {
            ClosingCodes result = null;
            if (source != null)
            {
                result = new ClosingCodes()
                {
                    Name = source.Name,
                    Description = source.Description,
                    InverseClosingCodesFather = source.Childs.ToEntity(closureCode),
                    CollectionsClosureCodes = closureCode,
                };
            }

            return result;
        }

        public static IList<ClosingCodes> ToEntity(this IList<ClosingCodeDto> source, CollectionsClosureCodes closureCode)
        {
            return source?.MapList(x => x.ToEntity(closureCode));
        }


        public static List<ClosingCodeDto> GetClosingCodesDtoTree(this List<ClosingCodes> list,
                                                                    int? parent)
        {
            return list?.Where(x => x.ClosingCodesFatherId == parent).Select(x => new ClosingCodeDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Childs = GetClosingCodesDtoTree(list, x.Id)
            }).ToList();
        }

        public static bool CanDelete(this ClosingCodes soruce)
        {
            return !soruce.ServicesClosingCode.Any()
                   && !soruce.ServicesClosingCodeFirst.Any()
                   && !soruce.ServicesClosingCodeSecond.Any()
                   && !soruce.ServicesClosingCodeThird.Any()
                   && !soruce.DerivedServicesClosingCodesIdN1Navigation.Any()
                   && !soruce.DerivedServicesClosingCodesIdN2Navigation.Any()
                   && !soruce.DerivedServicesClosingCodesIdN3Navigation.Any();
        }

        public static List<string> ToClosingCodesFatherDto(this ClosingCodes model, List<string> closingCodes = null)
        {
            if (closingCodes == null)
            {
                closingCodes = new List<string>();
            }

            if (model != null)
            {
                closingCodes.Insert(0, model.Name);
                if (model.ClosingCodesFather != null)
                {
                    closingCodes = model.ClosingCodesFather.ToClosingCodesFatherDto(closingCodes);
                }
            }

            return closingCodes;
        }

        public static List<ClosingCodeApiDto> ToClosingCodesApiDto(this IEnumerable<ClosingCodes> dbModelList)
        {
            var dtoList = new List<ClosingCodeApiDto>();

            if (dbModelList != null)
            {
                foreach (var dbModel in dbModelList)
                {
                    dtoList.Add(dbModel.ToClosingCodesApiDto());
                }
            }

            return dtoList;
        }

        public static ClosingCodeApiDto ToClosingCodesApiDto(this ClosingCodes dbModel)
        {
            var dto = new ClosingCodeApiDto
            {
                Name = dbModel.Name,
                Id = dbModel.Id,
                Description = dbModel.Description
            };

            if (dbModel.InverseClosingCodesFather != null && dbModel.InverseClosingCodesFather.Any())
            {
                dto.Childs = dbModel.InverseClosingCodesFather.ToClosingCodesApiDto();
            }

            return dto;
        }
    }
}