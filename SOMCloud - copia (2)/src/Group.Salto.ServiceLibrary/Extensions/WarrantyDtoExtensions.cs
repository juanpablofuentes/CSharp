using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Team;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class WarrantyDtoExtensions
    {
        public static Guarantee ToEntity(this GuaranteeDto source)
        {
            Guarantee result = null;
            if (source != null)
            {
                result = new Guarantee
                {
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

        public static Guarantee Update(this Guarantee target, GuaranteeDto source)
        {
            if (target != null && source != null)
            {
                target.Armored = source.Armored;
                target.BlnEndDate = source.BlnEndDate;
                target.BlnStartDate = source.BlnStartDate;
                target.IdExternal = source.IdExternal;
                target.ProEndDate = source.ProEndDate;
                target.ProStartDate = source.ProStartDate;
                target.Provider = source.Provider;
                target.Standard = source.Standard;
                target.StdEndDate = source.StdEndDate;
                target.StdStartDate = source.StdStartDate;
            }
            return target;
        }
    }
}