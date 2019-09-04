using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.People;
using Group.Salto.ServiceLibrary.Common.Dtos.SubContract;
using Group.Salto.SOM.Web.Models.MultiCombo;
using Group.Salto.SOM.Web.Models.People;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.SubContract;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class SubContractDetailViewModelExtensions
    {
        public static IList<SubContractDetailViewModel> ToDetailViewModel(this IList<SubContractDto> source)
        {
            return source?.MapList(x => x.ToDetailViewModel());
        }

        public static SubContractDetailViewModel ToDetailViewModel(this SubContractDto source)
        {
            SubContractDetailViewModel result = null;
            if (source != null)
            {
                result = new SubContractDetailViewModel();
                source.ToViewModel(result);
                result.KnowledgeSelected = source.KnowledgeSelected.ToMultiComboViewModel();
                result.PeopleSelected =
                    source.PeopleSelected.Where(x => !x.IsResponsable).ToMultiComboViewModel().ToList();
                result.ResponsiblesSelected = source.PeopleSelected.Where(x => x.IsResponsable).ToMultiComboViewModel().ToList();
                result.PurchaseRateId = source.PurchaseRateId;
            }
            return result;
        }

        
        public static SubContractDto ToDto(this SubContractDetailViewModel source)
        {
            SubContractDto result = null;
            if (source != null)
            {
                result = new SubContractDto();
                source.ToDto(result);
                result.KnowledgeSelected = source.KnowledgeSelected.ToSubContractKnowledgeDto();
                result.PeopleSelected = MergePeople(source.PeopleSelected, source.ResponsiblesSelected).ToList();
                result.PurchaseRateId = source.PurchaseRateId;
            }

            return result;
        }

        private static IList<PeopleSelectableDto> MergePeople(IList<MultiComboViewModel<int, int>> peopleSelected, 
                                                                IList<MultiComboViewModel<int, int>> responsiblesSelected)
        {
            var elements = new List<PeopleSelectableDto>();
            if (responsiblesSelected != null)
            {
                elements.AddRange(responsiblesSelected.Select(x => new PeopleSelectableDto()
                {
                    Name = x.Text,
                    Id = x.Value,
                    FirstSurname = x.TextSecondary,
                    IsResponsable = true,
                }));
            }
            if (peopleSelected != null)
            {
                elements.AddRange(peopleSelected.Select(x => new PeopleSelectableDto()
                {
                    Name = x.Text,
                    Id = x.Value,
                    FirstSurname = x.TextSecondary,
                }));
            }
            elements = elements.GroupBy(x => x.Id).Select(x => x.FirstOrDefault()).ToList();
            return elements;
        }

        public static ResultViewModel<SubContractDetailViewModel> ToDetailViewModel(this ResultDto<SubContractDto> source)
        {
            ResultViewModel<SubContractDetailViewModel> result = null;
            if (source != null)
            {
                result = new ResultViewModel<SubContractDetailViewModel>()
                {
                    Data = source.Data.ToDetailViewModel(),
                    Feedbacks = source.Errors.ToViewModel(),
                };
            }

            return result;
        }
    }
}