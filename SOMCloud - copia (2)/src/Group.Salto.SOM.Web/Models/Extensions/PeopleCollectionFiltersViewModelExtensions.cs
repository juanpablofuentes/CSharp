using Group.Salto.ServiceLibrary.Common.Dtos.PeopleCollection;
using Group.Salto.SOM.Web.Models.PeopleCollection;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class PeopleCollectionFiltersViewModelExtensions
    {
        public static PeopleCollectionFilterDto ToDto(this PeopleCollectionFilterViewModel source)
        {
            PeopleCollectionFilterDto result = null;
            if (source != null)
            {
                result = new PeopleCollectionFilterDto()
                {
                    Name = source.Name,
                    Description = source.Description,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }

            return result;
        }
    }
}