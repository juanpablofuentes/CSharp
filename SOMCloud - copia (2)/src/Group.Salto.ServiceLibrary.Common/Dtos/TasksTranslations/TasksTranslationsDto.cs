using System;

namespace Group.Salto.ServiceLibrary.Common.Dtos.TasksTranslations
{
    public class TasksTranslationsDto
    {
        public Guid Id { get; set; }
        public string NameText { get; set; }
        public string DescriptionText { get; set; }
        public int LanguageId { get; set; }
    }
}