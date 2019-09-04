using Group.Salto.ServiceLibrary.Common.Dtos.TasksTranslations;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Tasks
{
    public class TasksDto
    {
        public int FlowId { get; set; }
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> PermissionsTasksSelected { get; set; }
        public IList<TasksTranslationsDto> TasksPlainTranslations { get; set; }
        public Guid TriggerTypesId { get; set; }
        public string NameFieldModel { get; set; }
        public int TypeId { get; set; }
        public string TypeValue { get; set; }
    }
}