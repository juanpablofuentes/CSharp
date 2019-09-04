using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.LiteralPreconditions;
using Group.Salto.ServiceLibrary.Common.Dtos.PreconditionLiteralValues;
using Group.Salto.ServiceLibrary.Common.Dtos.PreconditionsTypes;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class LiteralPreconditionDtoExtension
    {
        public static LiteralPreconditionsDto ToDto(this LiteralsPreconditions source, List<PreconditionsTypesDto> types)
        {
            LiteralPreconditionsDto response = null;
            if (source != null) {
                var preconditionType = types.Where(x => x.Id == source.PreconditionsTypeId).FirstOrDefault();
                
                response = new LiteralPreconditionsDto()
                {
                    Id = source.Id,
                    ComparisonOperator = source.ComparisonOperator,
                    ExtraFieldId = source.Id,
                    PreconditionId = source.PreconditionId,
                    NomCampModel = preconditionType.Description,
                    PreconditionsLiteralValues = source.PreconditionsLiteralValues.ToDto(preconditionType.Description),
                    PreconditionsTypeId = source.PreconditionsTypeId,
                    PreconditionsTypeName = preconditionType.Name,
                };
            }
            return response;
        }

        public static LiteralPreconditionsDto ToDto(this LiteralsPreconditions source, PreconditionsTypesDto type)
        {
            LiteralPreconditionsDto response = null;
            if (source != null)
            {
                response = new LiteralPreconditionsDto()
                {
                    Id = source.Id,
                    ComparisonOperator = source.ComparisonOperator,
                    ExtraFieldId = source.Id,
                    PreconditionId = source.PreconditionId,
                    NomCampModel = type.Description,
                    PreconditionsLiteralValues = source.PreconditionsLiteralValues.ToDto(type.Description),
                    PreconditionsTypeId = source.PreconditionsTypeId,
                    PreconditionsTypeName = type.Name,
                };
            }
            return response;
        }

        public static LiteralsPreconditions ToEntity(this LiteralPreconditionsDto source, PreconditionsTypesDto type)
        {
            var response = new LiteralsPreconditions()
            {
                ComparisonOperator = source.ComparisonOperator,
                PreconditionId = source.PreconditionId,
                NomCampModel = type.Description,
                PreconditionsTypeId = type.Id,
            };
            return response;
        }

        public static IList<LiteralPreconditionsDto> ToDto(this ICollection<LiteralsPreconditions> source, List<PreconditionsTypesDto> types)
        {
            return source?.MapList(x => x.ToDto(types)).ToList();
        }

         public static LiteralsPreconditions AssignLiteralValues(this LiteralsPreconditions entity, IList<PreconditionLiteralValuesDto> literalValues)
        {
            if (literalValues != null && literalValues.Any())
            {
                entity.PreconditionsLiteralValues = entity.PreconditionsLiteralValues ?? new List<PreconditionsLiteralValues>();
                IList<PreconditionsLiteralValues> localLiteralValues = literalValues.ToEntity(entity.NomCampModel);
                foreach (PreconditionsLiteralValues localLiteralValue in localLiteralValues)
                {
                    entity.PreconditionsLiteralValues.Add(localLiteralValue);
                }
            }
            return entity;
        }

        public static IList<LiteralPreconditionsDto> ToListDto(this IQueryable<LiteralsPreconditions> source, List<PreconditionsTypesDto> types)
        {
            return source?.MapList(x => x.ToDto(types)).ToList();
        }
    }
}