using Group.Salto.Controls.Table.Models;
using System;

namespace Group.Salto.SOM.Web.Models.PaymentMethod
{
    public class PaymentMethodFilterViewModel : BaseFilter
    {
        public PaymentMethodFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
    }
}