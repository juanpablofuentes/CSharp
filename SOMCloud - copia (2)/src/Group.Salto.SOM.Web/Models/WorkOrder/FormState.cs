using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.WorkOrder
{
    public class FormState
    {
        public string FormStateClass(string stateForm)
        {
            return stateForm == "ValidationPending" ? "info" : stateForm == "DeliveringPending" ? "warning" : stateForm == "Delivered" ? "secondary" : "success";
        }
    }
}