using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Result
{
    public interface IResultBreadcrumbViewModel
    {
        IList<BreadcrumbViewModel> Breadcrumbs { get; set; }
    }
}
