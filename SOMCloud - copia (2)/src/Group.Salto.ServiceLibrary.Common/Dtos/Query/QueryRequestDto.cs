namespace Group.Salto.ServiceLibrary.Common.Dtos.Query
{
    public class QueryRequestDto
    {
        public QueryTypeEnum QueryType { get; set; }
        public QueryTypeParametersDto QueryTypeParameters { get; set; }
    }
}