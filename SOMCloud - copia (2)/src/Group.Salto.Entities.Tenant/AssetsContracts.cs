using Group.Salto.Common;

namespace Group.Salto.Entities.Tenant
{
    public class AssetsContracts
    {
        public int AssetsId { get; set; }
        public int ContractsId { get; set; }

        public Assets Assets { get; set; }
        public Contracts Contracts { get; set; }
    }
}