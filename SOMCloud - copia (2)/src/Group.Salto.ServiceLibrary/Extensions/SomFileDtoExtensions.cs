using System.Collections.Generic;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class SomFileDtoExtensions
    {
        public static SomFileDto ToDto(this SomFiles source)
        {
            SomFileDto result = null;
            if (source != null)
            {
                result = new SomFileDto
                {
                    Id = source.Id,
                    UpdateDate = source.UpdateDate,
                    Name = source.Name,
                    Container = source.Container,
                    ContentMd5 = source.ContentMd5,
                    Directory = source.Directory,
                    ModifiedDate = source.ModifiedDate
                };
            }
            return result;
        }

        public static IList<SomFileDto> ToDto(this IList<SomFiles> source)
        {
            return source?.MapList(c => c.ToDto());
        }
    }
}
