using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Group.Salto.SOM.Web.Models.WorkCenter
{
    public class WorkCenterListViewModel 
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Municipality { get; set; }
        public string Company { get; set; }
        public string Responsable { get; set; }
        public bool HasPeopleAssigned { get; set; }
    }
}