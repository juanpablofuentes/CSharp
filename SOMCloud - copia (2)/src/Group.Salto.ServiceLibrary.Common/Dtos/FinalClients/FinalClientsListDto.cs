using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Dtos.FinalClients
{
    public class FinalClientsListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Nif { get; set; }
        public string Phone1 { get; set; }       
        public string Fax { get; set; }        
        public string Observations { get; set; }
        public string Origin { get; set; }
    }
}