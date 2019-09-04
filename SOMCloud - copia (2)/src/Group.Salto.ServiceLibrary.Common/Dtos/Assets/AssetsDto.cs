namespace Group.Salto.ServiceLibrary.Common.Dtos.Assets
{
    public class AssetsDto
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string StockNumber { get; set; }
        public string AssetNumber { get; set; }
        public int AssetStatusId { get; set; }
        public string AssetStatusName { get; set; }
        public int Location { get; set; }
    }
}