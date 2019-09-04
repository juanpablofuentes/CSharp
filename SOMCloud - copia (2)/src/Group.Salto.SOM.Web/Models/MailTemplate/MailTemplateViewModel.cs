using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.MailTemplate
{
    public class MailTemplateViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Subject { get; set; }
    }
}