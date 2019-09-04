using Group.Salto.Controls.Table.Models;

namespace Group.Salto.SOM.Web.Models.Company
{
    public class CompaniesViewModel
    {
        public MultiItemViewModel<CompanyViewModel, int> Companies { get; set; }
        public CompanyFilterViewModel Filters { get; set; }
    }
}
