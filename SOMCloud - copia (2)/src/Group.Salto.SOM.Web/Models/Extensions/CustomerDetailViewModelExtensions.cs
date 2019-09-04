using System.Collections.Generic;
using Group.Salto.Common.Constants.Customer;
using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Customer;
using Group.Salto.ServiceLibrary.Common.Dtos.Modules;
using Group.Salto.SOM.Web.Models.Modules;
using Group.Salto.SOM.Web.Models.Result;

namespace Group.Salto.SOM.Web.Models.Customer
{
    public static class CustomerDetailViewModelExtensions
    {
        public static ResultViewModel<CustomerDetailViewModel> ToCustomerDetailViewModel(this ResultDto<CustomerDto> source,
                                                                                            IList<ModuleDto> modules = null,
                                                                                            IList<KeyValuePair<int, string>> countries = null)
        {
            var response = new ResultViewModel<CustomerDetailViewModel>()
            {
                Data = new CustomerDetailViewModel()
                {
                    Customer = source.Data.ToViewModel(),
                    Modules = modules.ToViewModel(),
                    Countries = countries,
                    CountrySelected = source?.Data?.MunicipalitySelected?.CountryId ?? 0,
                    StateSelected = source?.Data?.MunicipalitySelected?.StateId ?? 0,
                    RegionSelected = source?.Data?.MunicipalitySelected?.RegionId ?? 0,
                    MunicipalitySelected = source?.Data?.MunicipalitySelected?.MunicipalityId ?? 0,
                },
                Feedbacks = source.Errors.ToViewModel(),
            };
            return response;
        }

        public static CustomerDto ToCustomerDto(this CustomerDetailViewModel source)
        {
            var result = source?.Customer?.ToDto();
            if (result != null)
            {
                result.ModulesAssociated = source.ModuleIdsSelected?.ToGuids(CustomerConstants.ModulesSplitCharacter);
                if (source.CountrySelected > 0 && source.RegionSelected > 0 && source.StateSelected > 0 && source.MunicipalitySelected > 0)
                {
                    result.MunicipalitySelected = new MunicipalityIdsDto()
                    {
                        CountryId = source.CountrySelected,
                        StateId = source.StateSelected,
                        RegionId = source.RegionSelected,
                        MunicipalityId = source.MunicipalitySelected,
                    };
                }
            }
            return result;
        }
    }
}