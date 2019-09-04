using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.PeopleCollection;

namespace Group.Salto.ServiceLibrary.Common.Contracts.PeopleCollection
{
    public interface IPeopleCollectionService
    {
        ResultDto<IList<PeopleCollectionBaseDto>> GetAllFiltered(PeopleCollectionFilterDto filter);
        ResultDto<PeopleCollectionDto> GetById(int id);
        ResultDto<PeopleCollectionDto> Create(PeopleCollectionDto model);
        ResultDto<PeopleCollectionDto> Update(PeopleCollectionDto model);
        ResultDto<bool> Delete(int id);
        ResultDto<bool> CanDelete(int id);
        ResultDto<List<MultiSelectItemDto>> GetPeopleCollectionMultiSelect(int userId, List<int> peopleCollections);
        IList<BaseNameIdDto<int>> GetAllKeyValues(int peopleId);
    }
}