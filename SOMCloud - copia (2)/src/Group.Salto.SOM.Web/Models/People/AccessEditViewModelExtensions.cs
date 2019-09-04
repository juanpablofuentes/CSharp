using Group.Salto.Common.Constants;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.People;
using Group.Salto.ServiceLibrary.Common.Dtos.User;
using Group.Salto.SOM.Web.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group.Salto.SOM.Web.Models.Extensions;

namespace Group.Salto.SOM.Web.Models.People
{
    public static class AccessEditViewModelExtensions
    {
        public static ResultViewModel<AccessEditViewModel> ToPersonalEditViewModel(this ResultDto<AccessUserDto> source)
        {
            var response = new ResultViewModel<AccessEditViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel(),
            };

            return response;
        }

        public static AccessEditViewModel ToViewModel(this AccessUserDto source)
        {
            AccessEditViewModel accessUser = null;
            if (source != null)
            {
                accessUser = new AccessEditViewModel()
                {
                    Id = source.Id,
                    UserName = source.UserName,
                    Rol = source.SelectedRol,
                    IsActive = source.IsActive,
                };
            }

            return accessUser;
        }

        public static AccessUserDto ToDto(this AccessEditViewModel source, PersonalEditViewModel peopleVM, string tenantId)
        {
            AccessUserDto accessUser = null;
            if (source != null)
            {
                accessUser = new AccessUserDto()
                {
                    Id = source.Id,
                    UserName = source.UserName ?? string.Empty,
                    Password = source.Password ?? string.Empty,
                    LanguageId = peopleVM.Language,
                    SelectedRol = source.Rol,
                    CustomerId = new Guid(tenantId),
                    NumberEntriesPerPage = source.NumberEntriesPerPage.HasValue ? source.NumberEntriesPerPage.Value : AppConstants.NumberEntriesPerPage,
                    UserConfigurationId = peopleVM.UserConfigurationId.HasValue ? peopleVM.UserConfigurationId.Value : 0,
                    IsActive = source.IsActive,
                    Permissions = source.Permissions.Items.ToDto()
                };
            }

            return accessUser;
        }

        public static AccessUserDto ToAccessUserDto(this AccessEditViewModel source, PersonalEditViewModel peopleVM, string tenantId)
        {
            return source?.ToDto(peopleVM, tenantId);
        }
    }
}