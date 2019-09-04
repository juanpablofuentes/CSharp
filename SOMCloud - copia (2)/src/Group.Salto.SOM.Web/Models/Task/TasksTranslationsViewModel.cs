using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Task
{
    public class TasksTranslationsViewModel
    {
        public Guid Id { get; set; }
        public string NameText { get; set; }
        public string DescriptionText { get; set; }
        public int LanguageId { get; set; }
    }
}