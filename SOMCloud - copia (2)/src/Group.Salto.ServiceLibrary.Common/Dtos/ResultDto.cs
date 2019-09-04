namespace Group.Salto.ServiceLibrary.Common.Dtos
{
    public class ResultDto<TDto>
    {
        public TDto Data { get; set; }
        public ErrorsDto Errors { get; set; }
    }
}
