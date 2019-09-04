using Group.Salto.Controls.Table.Models;
using System;

namespace Group.Salto.SOM.Web.Models.PaymentMethod
{
    public class PaymentsMethodViewModel
    {
        public MultiItemViewModel<PaymentMethodViewModel, int> PaymentMethod { get; set; }
        public PaymentMethodFilterViewModel PaymentMethodFilters { get; set; }
    }
}