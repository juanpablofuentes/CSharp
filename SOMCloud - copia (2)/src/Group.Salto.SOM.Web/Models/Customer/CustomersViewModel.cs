using System;
using Group.Salto.Controls.Table.Models;

namespace Group.Salto.SOM.Web.Models.Customer
{
    public class CustomersViewModel
    {
        public MultiItemViewModel<CustomerViewModel, Guid> Customers { get; set; }
        public CustomerFilterViewModel CustomerFilters { get; set; }
    }
}
