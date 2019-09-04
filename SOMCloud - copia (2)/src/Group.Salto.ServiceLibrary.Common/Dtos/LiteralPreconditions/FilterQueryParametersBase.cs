using Group.Salto.ServiceLibrary.Common.Contracts.LiteralPreconditions;

namespace Group.Salto.ServiceLibrary.Common.Dtos.LiteralPreconditions
{
    public class FilterQueryParametersBase : IFilterQueryParameters
    {
        public int LiteralPreconditionId { get; set; }
        public int PreconditionId { get; set; }
        public string LiteralPreconditionTypeName { get; set; }
        public int LanguageId { get; set; }
        public int UserId { get; set; }
    }
}