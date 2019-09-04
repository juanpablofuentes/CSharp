using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.LiteralPreconditions;
using Group.Salto.ServiceLibrary.Common.Dtos.Postconditions;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Postcondition
{
    public interface IPostconditionsService
    {
        ResultDto<IList<PostconditionCollectionDto>> GetAllByTaskId(int id);
        ResultDto<bool> Delete(int id);
        ResultDto<bool> DeleteAllPostconditions(int id);
        PostconditionCollectionDto DeletePostconditionCollection(int id);
        bool CanDeletePostconditionCollection(int id);
        PostconditionsDto Update(PostconditionsDto postcondition);
        PostconditionsDto Create(PostconditionsDto postcondition);
        PostconditionCollectionDto CreatePostconditionCollection(int taskId);
        IList<BaseNameIdDto<int>> GetPostconditionValues(string PostconditionTypeName, FilterQueryParametersBase filterQueryParameters);
        IList<BaseNameIdDto<int>> GetTypeOtnValues(int id, FilterQueryParametersBase filterQueryParameters);
    }
}