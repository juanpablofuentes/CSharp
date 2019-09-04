using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Team;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class GuaranteeDtoExtensions
    {
        public static GuaranteeDto ToDto(this Guarantee dbModel)
        {
            var dto = new GuaranteeDto
            {
                Id = dbModel.Id,
                Armored = dbModel.Armored,
                BlnEndDate = dbModel.BlnEndDate,
                BlnStartDate = dbModel.BlnStartDate,
                IdExternal = dbModel.IdExternal,
                ProEndDate = dbModel.ProEndDate,
                ProStartDate = dbModel.ProStartDate,
                Provider = dbModel.Provider,
                Standard = dbModel.Standard,
                StdEndDate = dbModel.StdEndDate,
                StdStartDate = dbModel.StdStartDate
            };

            return dto;
        }

        public static GuaranteeDto ToDetailsDto(this Guarantee source)
        {
            var result = new GuaranteeDto();
            if (source != null)
            {
                result = new GuaranteeDto
                {
                    Id = source.Id,
                    Armored = source.Armored,
                    BlnEndDate = source.BlnEndDate,
                    BlnStartDate = source.BlnStartDate,
                    IdExternal = source.IdExternal,
                    ProEndDate = source.ProEndDate,
                    ProStartDate = source.ProStartDate,
                    Provider = source.Provider,
                    Standard = source.Standard,
                    StdEndDate = source.StdEndDate,
                    StdStartDate = source.StdStartDate
                };
            }
            return result;
        }

    }
}
