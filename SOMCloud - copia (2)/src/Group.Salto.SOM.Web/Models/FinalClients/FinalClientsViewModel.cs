using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using Group.Salto.Common.Constants;
using Group.Salto.Common.Helpers;
using Group.Salto.SOM.Web.Extensions;

namespace Group.Salto.SOM.Web.Models.FinalClients
{
    public class FinalClientsViewModel
    {
        public int Id { get; set; }
        public string IdExtern { get; set; }
        public int OriginId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Nif { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public string Fax { get; set; }
        public string Status { get; set; }
        public string Observations { get; set; }
        public int? PeopleCommercialId { get; set; }
        public string Code { get; set; }       
    }
}