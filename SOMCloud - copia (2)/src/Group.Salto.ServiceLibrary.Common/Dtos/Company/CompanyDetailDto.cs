using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Company
{
    public class CompanyDetailDto : CompanyDto
    {
        public IList<DepartmentDto> Departments { get; set; }
        public IEnumerable<BaseNameIdDto<int>> WorkCentersSelected { get; set; }
    }
}
