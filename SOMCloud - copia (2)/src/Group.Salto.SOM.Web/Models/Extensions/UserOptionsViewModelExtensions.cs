using Group.Salto.ServiceLibrary.Common.Dtos.User;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.UserOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class UserOptionsViewModelExtensions
    {
        public static UserOptionsViewModel ToViewModel(this UserDto source)
        {
            UserOptionsViewModel result = null;
            if (source != null)
            {
                result = new UserOptionsViewModel()
                {
                    Id = source.Id,
                    LanguageId = source.LanguageId,
                    NumberEntriesPerPageId = source.NumberEntriesPerPage
                };
            }
            return result;
        }

        public static UserOptionsDto ToUserOptionsDto(this ResultViewModel<UserOptionsViewModel> source)
        {
            UserOptionsDto result = null;
            if (source != null && source.Data != null)
            {
                result = new UserOptionsDto()
                {
                    Id = source.Data.Id,
                    OldPassword = source.Data.OldPassword,
                    Password = source.Data.NewPassword,
                    LanguageId = source.Data.LanguageId,
                    NumberEntriesPerPage = source.Data.NumberEntriesPerPageId,
                };
            }
            return result;
        }
    }
}
