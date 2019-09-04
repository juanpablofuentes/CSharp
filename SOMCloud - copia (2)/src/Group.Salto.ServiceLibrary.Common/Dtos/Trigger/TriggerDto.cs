using System;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Trigger
{
    public class TriggerDto
    {
        public int TaskId { get; set; }
        public Guid TypeId { get; set; }
        public string TypeName { get; set; }
        public string Value { get; set; }
        public int ValueId { get; set; }
    }
}