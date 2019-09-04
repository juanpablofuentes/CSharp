using Group.Salto.Controls.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Dtos.FinalClients
{
    public class FinalClientsFilterDto : ISortableFilter
    {
        public string OrderBy { get; set; }
        public bool Asc { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Observations { get; set; }
        public bool IsDeleted { get; set; }
    }
}