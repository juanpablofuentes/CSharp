using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.PeopleCollection;
using Group.Salto.SOM.Web.Models.PeopleCollection;
using Group.Salto.SOM.Web.Models.Result;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class PeopleCollectionDetailViewModelExtensions
    {

        public static ResultViewModel<PeopleCollectionDetailViewModel> ToResultViewModel(
            this ResultDto<PeopleCollectionDto> source)
        {
            ResultViewModel<PeopleCollectionDetailViewModel> result = null;
            if (source != null)
            {
                result = new ResultViewModel<PeopleCollectionDetailViewModel>()
                {
                    Data = source.Data.ToViewModel(),
                    Feedbacks = source.Errors.ToViewModel(),
                };
            }

            return result;
        }

        public static PeopleCollectionDetailViewModel ToViewModel(this PeopleCollectionDto source)
        {
            PeopleCollectionDetailViewModel result = null;
            if (source != null)
            {
                result = new PeopleCollectionDetailViewModel();
                source.ToViewModel(result);
                result.People = source.People.ToMultiComboViewModel();
                result.PeopleAdministrator = source.PeopleAdmin.ToMultiComboViewModel();
            }

            return result;
        }

        public static PeopleCollectionDto ToDto(this PeopleCollectionDetailViewModel source)
        {
            PeopleCollectionDto result = null;
            if (source != null)
            {
                result = new PeopleCollectionDto();
                source.ToDto(result);
                result.People = source.People.ToPeopleSelectableDto();
                result.PeopleAdmin = source.PeopleAdministrator.ToPeopleSelectableDto();
            }

            return result;
        }
      
    }
}