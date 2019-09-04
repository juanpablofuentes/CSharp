using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkForm;
using Group.Salto.SOM.Web.Models.WorkForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class WorkFormViewModelExtensions
    {
        public static WorkFormDto ToDto(this WorkFormViewModel source)
        {
            WorkFormDto result = null;
            if (source != null)
            {
                result = new WorkFormDto()
                {
                    Name = source.Name,
                    Template = source.Template,
                    Type = source.Type,
                };
            }
            return result;
        }
        public static IList<WorkFormDto> ToListDto(this IList<WorkFormViewModel> source)
        {
            return source?.MapList(x => x.ToDto());
        }
        public static WorkFormViewModel ToViewModel(this WorkFormDto source)
        {
            WorkFormViewModel result = null;
            if (source != null)
            {
                result = new WorkFormViewModel()
                {
                    Name = source.Name,
                    Template = source.Template,
                    Type = source.Type,
                };
            }
            return result;
        }
        public static IList<WorkFormViewModel> ToListViewModel(this IList<WorkFormDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }
    }
}