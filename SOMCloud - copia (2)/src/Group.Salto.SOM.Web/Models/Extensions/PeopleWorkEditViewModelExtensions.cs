using Group.Salto.ServiceLibrary.Common.Dtos.People;
using Group.Salto.SOM.Web.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.MultiCombo;
using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.SubContract;
using System.Globalization;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class PeopleWorkEditViewModelExtensions
    {
        public static WorkEditViewModel ToWorkViewModel(this PeopleDto source)
        {
            WorkEditViewModel people = null;
            if (source != null)
            {
                people = new WorkEditViewModel()
                {
                    CompanyId = source.CompanyId,
                    ResponsibleId = source.ResponsibleId,
                    DepartmentId = source.DepartmentId,
                    PointRateId = source.PointRateId,
                    SubcontractId = source.SubcontractId,
                    SubcontractorResponsible = source.SubcontractorResponsible,
                    ProjectId = source.ProjectId,
                    CostKm = ((decimal?)source.CostKm)?.DecimalToString(CultureInfo.InvariantCulture.TwoLetterISOLanguageName),
                    DocumentationUrl = source.DocumentationUrl,
                    PriorityId = source.PriorityId,
                    IsActive = source.IsActive,
                    AnnualHours = source.AnnualHours,
                    KnowledgeSelected = source.KnowledgeSelected.ToMultiComboViewModel(),
                    WorkCenterId = source.WorkCenterId
                };
            }

            return people;
        }

        public static MultiComboViewModel<int, int> ToMultiComboViewModel(this PeopleKnowledgeDto source)
        {
            MultiComboViewModel<int, int> result = null;
            if (source != null)
            {
                result = new MultiComboViewModel<int, int>()
                {
                    Value = source.Id,
                    ValueSecondary = source.Priority,
                    TextSecondary = source.Priority.ToString("00"),
                    Text = source.Name,
                };
            }
            return result;
        }

        public static IList<MultiComboViewModel<int, int>> ToMultiComboViewModel(this IList<PeopleKnowledgeDto> source)
        {
            return source?.MapList(pk => pk.ToMultiComboViewModel());
        }

        public static PeopleKnowledgeDto ToPeopleKnowledgeDto(this MultiComboViewModel<int, int> source)
        {
            PeopleKnowledgeDto result = null;
            if (source != null)
            {
                result = new PeopleKnowledgeDto()
                {
                    Id = source.Value,
                    Name = source.Text,
                    Priority = source.ValueSecondary
                };
            }
            return result;
        }

        public static IList<PeopleKnowledgeDto> ToPeopleKnowledgeDto(this IList<MultiComboViewModel<int, int>> source)
        {
            return source?.MapList(pk => pk.ToPeopleKnowledgeDto());
        }
    }
}