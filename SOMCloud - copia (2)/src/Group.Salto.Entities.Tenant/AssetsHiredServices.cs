namespace Group.Salto.Entities.Tenant
{
    public class AssetsHiredServices
    {
        public int AssetId { get; set; }
        public int HiredServiceId { get; set; }

        public HiredServices HiredService { get; set; }
        public Assets Asset { get; set; }
    }
}
