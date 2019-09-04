using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Clients;
using Group.Salto.SOM.Web.Models.Client;
using Group.Salto.SOM.Web.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ClientsDetailViewModelExtensions
    {
        public static ClientDetailDto ToDto(this ClientDetailViewModel source)
        {
            ClientDetailDto result = null;
            if (source != null)
            {
                result = new ClientDetailDto()
                {
                    Id = source.ClientGeneralDetail.Id,
                    CorporateName = source.ClientGeneralDetail.CorporateName,
                    ComercialName = source.ClientGeneralDetail.ComercialName,
                    Alias = source.ClientGeneralDetail.Alias,
                    InternCode = source.ClientGeneralDetail.InternCode,
                    ContableCode = source.ClientGeneralDetail.ContableCode,
                    Address = source.ClientGeneralDetail.Address,
                    MunicipalitySelected = source.ClientGeneralDetail.MunicipalitySelected,
                    PostalCodeId = source.ClientGeneralDetail.PostalCode,
                    Phone = source.ClientGeneralDetail.Phone,
                    MobilePhone = source.ClientGeneralDetail.MobilePhone,
                    Mail = source.ClientGeneralDetail.Mail,
                    Web = source.ClientGeneralDetail.Web,
                    Observations = source.ClientGeneralDetail.Observations,
                    UnListed = source.ClientGeneralDetail.UnListed,
                    BankCode = source.ClientBankDetail.BankCode,
                    BranchNumber = source.ClientBankDetail.BranchNumber,
                    ControlDigit = source.ClientBankDetail.ControlDigit,
                    AccountNumber = source.ClientBankDetail.AccountNumber,
                    SwiftCode = source.ClientBankDetail.SwiftCode,
                    BankName = source.ClientBankDetail.BankName,
                    BankAddress = source.ClientBankDetail.BankAddress,
                    BankPostalCode = source.ClientBankDetail.BankPostalCode,
                    BankCity = source.ClientBankDetail.BankCity
                };
            }
            return result;
        }

        public static ResultViewModel<ClientDetailViewModel> ToViewModel(this ResultDto<ClientDetailDto> source)
        {
            var response = source != null ? new ResultViewModel<ClientDetailViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static ClientDetailViewModel ToViewModel(this ClientDetailDto source)
        {
            ClientDetailViewModel result = null;
            if (source != null)
            {
                result = new ClientDetailViewModel();
                result.ClientGeneralDetail = new ClientGeneralDetailViewModel()
                {
                    Id = source.Id,
                    CorporateName = source.CorporateName,
                    ComercialName = source.ComercialName,
                    Alias = source.Alias,
                    InternCode = source.InternCode,
                    ContableCode = source.ContableCode,
                    Address = source.Address,
                    MunicipalitySelected = source.MunicipalitySelected,
                    StateSelected = source.StateSelected,
                    RegionSelected = source.RegionSelected,
                    CountrySelected = source.CountrySelected,
                    PostalCode = source.PostalCodeId,
                    Phone = source.Phone,
                    MobilePhone = source.MobilePhone,
                    Mail = source.Mail,
                    Web = source.Web,
                    Observations = source.Observations,
                    UnListed = source.UnListed
                };

                result.ClientBankDetail = new ClientBankDetailViewModel()
                {
                    Id = source.Id,
                    BankCode = source.BankCode,
                    BranchNumber = source.BranchNumber,
                    ControlDigit = source.ControlDigit,
                    AccountNumber = source.AccountNumber,
                    SwiftCode = source.SwiftCode,
                    BankName = source.BankName,
                    BankAddress = source.BankAddress,
                    BankPostalCode = source.BankPostalCode,
                    BankCity = source.BankCity,
                    EditBankData = (source.Id != 0)
                };
            }
            return result;
        }
    }
}