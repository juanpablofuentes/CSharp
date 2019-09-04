using System;

namespace Group.Salto.Entities.Tenant
{
    public class TaskTokens
    {
        public string Token { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Consumed { get; set; }
    }
}
