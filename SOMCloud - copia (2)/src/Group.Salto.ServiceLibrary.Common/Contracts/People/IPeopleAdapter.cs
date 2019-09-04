using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.People;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Group.Salto.ServiceLibrary.Common.Contracts.People
{
    public interface IPeopleAdapter
    {
        IEnumerable<PeopleListDto> GetList(PeopleFilterDto filter, Guid tenantId);
        Task<ResultDto<GlobalPeopleDto>> CreatePeople(GlobalPeopleDto people);
        Task<ResultDto<GlobalPeopleDto>> UpdatePeople(GlobalPeopleDto people);
        ResultDto<bool> DeletePeople(int Id);
        ResultDto<List<MultiSelectItemDto>> GetPermissionList(int? peopleId);
        FileContentDto ExportToExcel(PeopleFilterDto filter, Guid tenantId);
    }
}