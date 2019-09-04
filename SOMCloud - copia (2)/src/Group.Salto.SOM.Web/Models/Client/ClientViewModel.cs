using Group.Salto.Common.Constants;
using Group.Salto.SOM.Web.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Group.Salto.SOM.Web.Models.Client
{
    public class ClientViewModel 
    {
        public int Id { get; set; }
        public string CorporateName { get; set; }
        public bool UnListed { get; set; }
        public string Phone { get; set; }
        public string Observations { get; set; }
    }
}