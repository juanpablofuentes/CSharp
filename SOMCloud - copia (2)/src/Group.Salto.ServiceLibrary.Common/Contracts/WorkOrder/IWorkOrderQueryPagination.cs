using Group.Salto.ServiceLibrary.Common.Dtos.Grid;

namespace Group.Salto.ServiceLibrary.Common.Contracts.WorkOrder
{
    public interface IWorkOrderQueryPagination
    {
        string CreatePagination(GridDto gridConfig);
    }
}