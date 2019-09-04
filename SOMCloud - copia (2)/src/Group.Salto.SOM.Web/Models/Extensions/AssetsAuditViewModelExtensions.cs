using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.AssetsAudit;
using Group.Salto.SOM.Web.Models.AssetsAudit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class AssetsAuditViewModelExtensions
    {
        public static AssetsAuditViewModel ToViewModel(this AssetsAuditDto source)
        {
            AssetsAuditViewModel result = null;
            if (source != null)
            {
                result = new AssetsAuditViewModel()
                {
                    Registry = source.Registry,
                    UserName = source.UserName,
                    Changes = source.Changes.ToListViewModel()
                };
            }
            return result;
        }

        public static IList<AssetsAuditViewModel> ToListViewModel(this List<AssetsAuditDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }
    }
}