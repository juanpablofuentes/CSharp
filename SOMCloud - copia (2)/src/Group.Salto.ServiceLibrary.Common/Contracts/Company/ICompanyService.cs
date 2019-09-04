using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Company;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Company
{
    public interface ICompanyService
    {
        ResultDto<IList<CompanyDto>> GetAllFiltered(CompanyFilterDto filter);
        ResultDto<CompanyDetailDto> GetById(int id);
        ResultDto<CompanyDetailDto> Update(CompanyDetailDto company);
        ResultDto<CompanyDetailDto> Create(CompanyDetailDto company);
        ResultDto<bool> Delete(int id);
        IList<BaseNameIdDto<int>> GetAllNotDeleteKeyValues();
        IList<BaseNameIdDto<int>> GetDepartmentsByCompanyIdKeyValues(int? companyId);
    }
}