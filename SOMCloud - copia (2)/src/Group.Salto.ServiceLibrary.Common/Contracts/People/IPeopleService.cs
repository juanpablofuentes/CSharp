using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.People;
using Group.Salto.ServiceLibrary.Common.Dtos.PeopleVisible;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.People
{
    public interface IPeopleService
    {
        ResultDto<PeopleDto> GetById(int Id);
        ResultDto<IList<PeopleListDto>> GetByIds(int[] Ids, PeopleFilterDto filter);
        ResultDto<IList<PeopleListDto>> GetListFiltered(PeopleFilterDto filter);
        ResultDto<IList<PeopleVisibleListDto>> GetVisibleListFiltered(PeopleVisibleFilterDto filter);
        ResultDto<PeopleDto> CreatePeople(GlobalPeopleDto globalPeople);
        ResultDto<PeopleDto> UpdatePeople(GlobalPeopleDto globalPeople);
        ResultDto<PeopleDto> DeletePeople(int id);
        IList<BaseNameIdDto<int>> GetByCompanyIdKeyValues(int companyId, int Id);
        IList<BaseNameIdDto<int>> GetAllCommercialKeyValues();
        IList<BaseNameIdDto<int>> Filter(QueryRequestDto queryRequest);
        IList<BaseNameIdDto<int>> GetActiveByCompanyKeyValue(int? companyId);
        IList<BaseNameIdDto<int>> GetAllKeyValue();
        IList<BaseNameIdDto<int>> GetAllDriversKeyValue();
        IList<BaseNameIdDto<int>> GetAllActiveKeyValue();
        IList<BaseNameIdDto<int>> GetPeopleTechniciansKeyValues(PeopleFilterDto filter);
        IList<BaseNameIdDto<int>> GetPeopleManagerKeyValues(PeopleFilterDto filter);
        ResultDto<List<MultiSelectItemDto>> GetPeopleTechniciansMultiSelect(int userId, List<int> technicians);
        ResultDto<List<MultiSelectItemDto>> GetActivePeopleMultiSelect(List<int> selectItems);
        string GetNamesComaSeparated(List<int> ids);
    }
}