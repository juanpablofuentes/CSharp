using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.AssetsAudit;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class AssetsAuditDtoExtensions
    {
        public static AssetsAuditDto ToDto(this AssetsAudit source)
        {
            AssetsAuditDto result = null;
            if (source != null)
            {
                result = new AssetsAuditDto
                {
                    Registry = source.RegistryDate,
                    UserName = source.UserName,
                    Changes = source.AssetsAuditChanges?.ToListDto().ToList()
                };
            }
            return result;
        }

        public static IEnumerable<AssetsAuditDto> ToListDto(this ICollection<AssetsAudit> source)
        {
            return source?.MapList(x => x.ToDto());
        }
    }
}