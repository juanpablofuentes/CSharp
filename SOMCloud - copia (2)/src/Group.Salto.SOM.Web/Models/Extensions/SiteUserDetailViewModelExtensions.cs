using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.SiteUser;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.SiteUser;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class SiteUserDetailViewModelExtensions
    {
        public static SiteUserDetailViewModel ToViewModel (this SiteUserDetailDto source)
        {
            SiteUserDetailViewModel result = null;
            if(source != null)
            {
                result = new SiteUserDetailViewModel()
                {
                    IdUser = source.Id,
                    Name = source.Name,
                    FirstSurname = source.FirstSurname,
                    SecondSurname = source.SecondSurname,
                    Email = source.Email,
                    Telephone = source.Telephone,
                    Position = source.Position,
                    LocationId = source.LocationId,
                };
            }
            return result;
        }

        public static ResultViewModel<SiteUserDetailViewModel> ToViewModel (this ResultDto<SiteUserDetailDto> source)
        {
            var response = source != null ? new ResultViewModel<SiteUserDetailViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static SiteUserDetailDto ToDto(this SiteUserDetailViewModel source)
        {
            SiteUserDetailDto result = null;
            if (source != null)
            {
                result = new SiteUserDetailDto()
                {
                    Id = source.IdUser,
                    Name = source.Name,
                    FirstSurname = source.FirstSurname,
                    SecondSurname = source.SecondSurname,
                    Email = source.Email,
                    Telephone = source.Telephone,
                    Position = source.Position,
                    LocationId = source.LocationId,
                };
            }
            return result;
        }
    }
}