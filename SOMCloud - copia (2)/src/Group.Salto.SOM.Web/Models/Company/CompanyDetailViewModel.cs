using System.Collections.Generic;
using Group.Salto.SOM.Web.Models.MultiCombo;

namespace Group.Salto.SOM.Web.Models.Company
{
    public class CompanyDetailViewModel : CompanyViewModel
    {
        public IList<DepartmentViewModel> Departments { get; set; }
        public IList<DepartmentViewModel> DepartmentsSelected { get; set; }
        public IEnumerable<MultiComboViewModel<int, int>> WorkCentersSelected { get; set; }
    }
}
