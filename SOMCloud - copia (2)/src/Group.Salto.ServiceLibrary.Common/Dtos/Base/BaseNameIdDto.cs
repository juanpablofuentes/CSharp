namespace Group.Salto.ServiceLibrary.Common.Dtos.Base
{
    public class BaseNameIdDto<T>
    {
        public T Id { get; set; }
        public string Name { get; set; }
        public bool IsLocked { get; set; } = false;
    }
}