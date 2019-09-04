using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.SymptomCollection;
using Group.Salto.SOM.Web.Models.MultiSelect;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.SymptomCollection;
using System.Linq;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class SymptomCollectionDetailViewModelExtensions
    {
        public static SymptomCollectionDetailViewModel ToDetailViewModel(this SymptomCollectionDto source)
        {
            SymptomCollectionDetailViewModel result = null;
            if (source != null)
            {
                result = new SymptomCollectionDetailViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Element = source.Element,
                    Description = source.Description
                };
            }
            return result;
        } 

        public static SymptomCollectionDto ToDto(this SymptomCollectionDetailViewModel source)
        {
            SymptomCollectionDto result = null;
            if (source != null)
            {
                result = new SymptomCollectionDto()
                {
                    Id = source.Id ?? 0,
                    Name = source.Name,
                    Description = source.Description,
                    Element = source.Element
                };    
                result.SymptomSelected = source.SymptomsSelected?.Items.Where(x => x.IsChecked && int.TryParse(x.Value, out var temp))?
                                                    .Select(x => int.Parse(x.Value))?.ToList();
            }

            return result;
        }

        public static ResultViewModel<SymptomCollectionDetailViewModel> ToDetailViewModel(this ResultDto<SymptomCollectionDto> source)
        {
            ResultViewModel<SymptomCollectionDetailViewModel> result = null;
            if (source != null)
            {
                result = new ResultViewModel<SymptomCollectionDetailViewModel>()
                {
                    Data = source.Data.ToDetailViewModel(),
                    Feedbacks = source.Errors.ToViewModel(),
                };
            }

            return result;
        }
    }
}