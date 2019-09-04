using System.Collections.Generic;
using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.People;
using Group.Salto.SOM.Web.Models.MultiCombo;
using Group.Salto.SOM.Web.Models.People;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class PeopleSelectedViewModelExtensions
    {
        public static PeopleSelectedViewModel ToViewModel(this PeopleSelectableDto source)
        {
            PeopleSelectedViewModel result = null;
            if (source != null)
            {
                result = new PeopleSelectedViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Surname = source.FirstSurname
                };
            }
            return result;
        }

        public static IList<PeopleSelectedViewModel> ToViewModel(this IList<PeopleSelectableDto> source)
        {
            return source?.MapList(pS => pS.ToViewModel());
        }

        public static PeopleSelectableDto ToDto(this PeopleSelectedViewModel source)
        {
            PeopleSelectableDto result = null;
            if (source != null)
            {
                result = new PeopleSelectableDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    FirstSurname = source.Surname
                };
            }
            return result;
        }

        public static IList<PeopleSelectableDto> ToDto(this IList<PeopleSelectedViewModel> source)
        {
            return source?.MapList(pS => pS.ToDto());
        }

        public static MultiComboViewModel<int, int> ToMultiComboViewModel(this PeopleSelectableDto source)
        {
            MultiComboViewModel<int, int> result = null;
            if (source != null)
            {
                result = new MultiComboViewModel<int, int>()
                {
                    Value = source.Id,
                    Text = $"{source.Name} {source.FirstSurname}",
                    TextSecondary = source.FirstSurname
                };
            }
            return result;
        }

        public static IEnumerable<MultiComboViewModel<int, int>> ToMultiComboViewModel(this IEnumerable<PeopleSelectableDto> source)
        {
            return source?.MapList(x => x.ToMultiComboViewModel());
        }

        public static IList<MultiComboViewModel<int, int>> ToMultiComboViewModel(this IList<PeopleSelectableDto> source)
        {
            return source?.MapList(x => x.ToMultiComboViewModel());
        }

        public static PeopleSelectableDto ToPeopleSelectableDto(this MultiComboViewModel<int, int> source)
        {
            PeopleSelectableDto result = null;
            if (source != null)
            {
                result = new PeopleSelectableDto()
                {
                    Id = source.Value,
                    Name = source.Text,
                    FirstSurname = source.TextSecondary
                };
            }
            return result;
        }

        public static IList<PeopleSelectableDto> ToPeopleSelectableDto(this IList<MultiComboViewModel<int, int>> source)
        {
            return source?.MapList(x => x.ToPeopleSelectableDto());
        }
    }
}