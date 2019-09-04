using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.AssetsAudit;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class AssetsChangesDtoExtensions
    {
        public static AssetsChangesDto ToDto(this AssetsAuditChanges source)
        {
            AssetsChangesDto result = null;
            if (source != null)
            {
                result = new AssetsChangesDto
                {
                    Property = source.Property,
                    OldValue = source.OldValue,
                    NewValue = source.NewValue
                };
            }
            return result;
        }

        public static IEnumerable<AssetsChangesDto> ToListDto(this ICollection<AssetsAuditChanges> source)
        {
            return source?.MapList(x => x.ToDto());
        }
    }
}