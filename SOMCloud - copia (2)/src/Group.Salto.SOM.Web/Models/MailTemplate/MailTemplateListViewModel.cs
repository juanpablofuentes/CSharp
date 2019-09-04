using Group.Salto.Controls.Table.Models;
using Group.Salto.SOM.Web.Models.WorkForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.MailTemplate
{
    public class MailTemplateListViewModel
    {
        public MultiItemViewModel<MailTemplateViewModel, int> MailTemplateList { get; set; }
        public MailTemplateViewModel MailTemplate { get; set; }
        public IList<WorkFormViewModel> WorkForm { get; set; }
        public MailTemplateFilterViewModel Filters { get; set; }
    }
}