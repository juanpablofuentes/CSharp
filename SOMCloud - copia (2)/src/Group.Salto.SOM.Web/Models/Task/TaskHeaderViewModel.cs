using Group.Salto.Controls.Table.Models;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Language;
using Group.Salto.SOM.Web.Models.Languages;
using Group.Salto.SOM.Web.Models.MultiCombo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Group.Salto.SOM.Web.Models.Task
{
    public class TaskHeaderViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<int> PermissionsTasksSelected { get; set; }
        public IList<TasksTranslationsViewModel> TasksTranslationsList { get; set; }
        public IList<LanguageViewModel> AvailableLanguages { get; set; }
    }
}