using System;

namespace Group.Salto.Entities
{
    public class ModuleActionGroups
    {
        public Guid ModuleId { get; set; }
        public Guid ActionGroupsId { get; set; }
        public Module Module { get; set; }        
        public ActionGroups ActionGroups { get; set; }        
    }
}