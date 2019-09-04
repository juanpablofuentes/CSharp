using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Client
{
    public class ClientBankDetailViewModel
    {
        public int Id { get; set; }
        public bool EditBankData { get; set; }
        public string BankCode { get; set; }
        public string BranchNumber { get; set; }
        public string ControlDigit { get; set; }
        public string AccountNumber { get; set; }
        public string SwiftCode { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public string BankPostalCode { get; set; }
        public string BankCity { get; set; }

        public bool LoadAsReadOnly()
        {
            return Id != 0;
        }
    }
}