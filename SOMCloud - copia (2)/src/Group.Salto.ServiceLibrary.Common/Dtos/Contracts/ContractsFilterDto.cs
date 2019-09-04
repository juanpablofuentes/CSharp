using Group.Salto.Controls.Entities;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Contracts
{
    public class ContractsFilterDto : ISortableFilter
    {
        public string Object { get; set; }
        public string OrderBy { get; set; }
        public bool Asc { get; set; }
    }
}