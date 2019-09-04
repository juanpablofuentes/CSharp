using Group.Salto.Common.Helpers;
using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkForm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class WorkFormDtoExtension
    {
        public static WorkFormDto ToDto(this WorkForm source)
        {
            WorkFormDto result = null;
            if (source != null)
            {
                result = new WorkFormDto()
                {
                    Name = source.Name,
                    Template = source.Templates,
                    Type = source.Type,
                };
            }
            return result;
        }
        public static IList<WorkFormDto> ToListDto(this IList<WorkForm> source)
        {
            return source?.MapList(x => x.ToDto());
        }
        public static WorkForm ToEntity(this WorkFormDto source)
        {
            WorkForm result = null;
            if (source != null)
            {
                result = new WorkForm()
                {
                    Name = source.Name,
                    Templates = source.Template,
                    Type = source.Type,
                };
            }
            return result;
        }
    }
}