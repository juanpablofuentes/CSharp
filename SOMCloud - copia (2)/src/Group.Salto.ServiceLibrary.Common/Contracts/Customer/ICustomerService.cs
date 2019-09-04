using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Customer;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Customer
{
    public interface ICustomerService
    {
        Task<ResultDto<CustomerDto>> Create(CustomerDto source, int languageId);
        ResultDto<CustomerDto> GetById(Guid id);
        ResultDto<IList<CustomerDto>> GetAll();
        ResultDto<CustomerDto> UpdateCustomer(CustomerDto source);
        ResultDto<CustomerDto> CanCreate(CustomerDto customer);
        ResultDto<IList<CustomerDto>> GetAllFiltered(CustomerFilterDto filter);
    }
}
