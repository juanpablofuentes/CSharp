using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Dtos.MailTemplate
{
    public class MailTemplateFilterDto : BaseFilterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Subject { get; set; }
    }
}