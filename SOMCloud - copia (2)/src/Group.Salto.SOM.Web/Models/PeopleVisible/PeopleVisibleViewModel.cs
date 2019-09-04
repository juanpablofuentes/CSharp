using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.PeopleVisible
{
    public class PeopleVisibleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FisrtSurname { get; set; }
        public string SecondSurname { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Extension { get; set; }
        public string Company { get; set; }
        public string Department { get; set; }
    }
}