using Group.Salto.ServiceLibrary.Common.Dtos.Identity;

namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.Extension
{
    public static class LoginDtoExtension
    {
        public static IdentityLoginDto ToIdentityLoginDto(this LoginDto source)
        {
            IdentityLoginDto result = new IdentityLoginDto
            {
                Email = source.Email,
                Password = source.Password,
                RememberMe = source.RememberMe,
                UserName = source.UserName
            };
            return result;
        }

        public static LoginDto ToLoginDto(this IdentityLoginDto source)
        {
            LoginDto result = new LoginDto
            {
                Email = source.Email,
                Password = source.Password,
                RememberMe = source.RememberMe,
                UserName = source.UserName
            };
            return result;
        }
    }
}