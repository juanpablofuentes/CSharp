using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Client
{
    public class ClientDetailViewModel
    {
        public ClientDetailViewModel()
        {
            ClientGeneralDetail = new ClientGeneralDetailViewModel();
            ClientBankDetail = new ClientBankDetailViewModel();
        }
        public ClientGeneralDetailViewModel ClientGeneralDetail { get; set; }
        public ClientBankDetailViewModel ClientBankDetail { get; set; }
    }
}