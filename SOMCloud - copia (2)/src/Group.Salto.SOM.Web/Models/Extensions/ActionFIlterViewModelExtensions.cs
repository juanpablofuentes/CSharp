using Group.Salto.ServiceLibrary.Common.Dtos.Actions;
using Group.Salto.SOM.Web.Models.Actions;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ActionFIlterViewModelExtensions
    {
        public static ActionFilterDto ToDto(this ActionsFilterViewModel source)
        {
            ActionFilterDto result = null;
            if (source != null)
            {
                result = new ActionFilterDto()
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
