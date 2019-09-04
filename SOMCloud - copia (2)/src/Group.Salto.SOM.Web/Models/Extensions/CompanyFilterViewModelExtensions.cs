using Group.Salto.ServiceLibrary.Common.Dtos.Company;
using Group.Salto.SOM.Web.Models.Company;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class CompanyFilterViewModelExtensions
    {
        public static CompanyFilterViewModel ToViewModel(this CompanyFilterDto source)
        {
            CompanyFilterViewModel result = null;
            if (source != null)
            {
                result = new CompanyFilterViewModel()
                {
                    Name = source.Name,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }
            return result;
        }

        public static CompanyFilterDto ToDto(this CompanyFilterViewModel source)
        {
            CompanyFilterDto result = null;
            if (source != null)
            {
                result = new CompanyFilterDto()
                {
                    Name = source.Name,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }
            return result;
        }
    }
}
