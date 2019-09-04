using Group.Salto.Common.Constants;
using Group.Salto.SOM.Web.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Group.Salto.SOM.Web.Models.Contracts
{
    public class ContractListViewModel
    {
        public int Id { get; set; }
        public string Object { get; set; }
        public bool Active { get; set; }
        public string Client { get; set; }
    }
}