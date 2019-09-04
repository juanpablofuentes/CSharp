using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Clients;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ClientsDetailDtoExtensions
    {
        public static ClientDetailDto ToDetailDto(this Clients source)
        {
            ClientDetailDto result = null;
            if (source != null)
            {
                result = new ClientDetailDto()
                {
                    Id = source.Id,
                    CorporateName = source.CorporateName,
                    ComercialName = source.ComercialName,
                    Alias = source.Alias,
                    InternCode = source.InternCode,
                    ContableCode = source.ContableCode,
                    Address = source.Address,
                    MunicipalitySelected = source.MunicipalityId ?? 0,
                    PostalCodeId = source.PostalCodeId,
                    Phone = source.Phone,
                    MobilePhone = source.MobilePhone,
                    Mail = source.Mail,
                    Web = source.Web,
                    Observations = source.Observations,
                    BankCode = source.BankCode,
                    BranchNumber = source.BranchNumber,
                    ControlDigit = source.ControlDigit,
                    AccountNumber = source.AccountNumber,
                    SwiftCode = source.SwiftCode,
                    BankName = source.BankName,
                    BankAddress = source.BankAddress,
                    BankPostalCode = source.BankPostalCode,
                    BankCity = source.BankCity,
                    UnListed = source.UnListed
                };
            }
            return result;
        }

        public static Clients ToEntity(this ClientDetailDto source)
        {
            Clients result = null;
            if (source != null)
            {
                result = new Clients()
                {
                    CorporateName = source.CorporateName,
                    ComercialName = source.ComercialName,
                    Alias = source.Alias,
                    InternCode = source.InternCode,
                    ContableCode = source.ContableCode,
                    Address = source.Address,
                    MunicipalityId = source.MunicipalitySelected,
                    PostalCodeId = source.PostalCodeId,
                    Phone = source.Phone,
                    MobilePhone = source.MobilePhone,
                    Mail = source.Mail,
                    Web = source.Web,
                    Observations = source.Observations,
                    BankCode = source.BankCode,
                    BranchNumber = source.BranchNumber,
                    ControlDigit = source.ControlDigit,
                    AccountNumber = source.AccountNumber,
                    SwiftCode = source.SwiftCode,
                    BankName = source.BankName,
                    BankAddress = source.BankAddress,
                    BankPostalCode = source.BankPostalCode,
                    BankCity = source.BankCity,
                    UnListed = source.UnListed
                };
            }
            return result;
        }

        public static void UpdateClient(this Clients target, Clients source)
        {
            if (source != null && target != null)
            {
                target.CorporateName = source.CorporateName;
                target.ComercialName = source.ComercialName;
                target.Alias = source.Alias;
                target.InternCode = source.InternCode;
                target.ContableCode = source.ContableCode;
                target.Address = source.Address;
                target.MunicipalityId = source.MunicipalityId;
                target.PostalCodeId = source.PostalCodeId;
                target.Phone = source.Phone;
                target.MobilePhone = source.MobilePhone;
                target.Mail = source.Mail;
                target.Web = source.Web;
                target.Observations = source.Observations;
                target.BankCode = source.BankCode;
                target.BranchNumber = source.BranchNumber;
                target.ControlDigit = source.ControlDigit;
                target.AccountNumber = source.AccountNumber;
                target.SwiftCode = source.SwiftCode;
                target.BankName = source.BankName;
                target.BankAddress = source.BankAddress;
                target.BankPostalCode = source.BankPostalCode;
                target.BankCity = source.BankCity;
                target.UnListed = source.UnListed;
            }
        }

        public static bool IsValid(this ClientDetailDto source)
        {
            bool result = false;
            result = source != null && !string.IsNullOrEmpty(source.CorporateName);
            return result;
        }
    }
}