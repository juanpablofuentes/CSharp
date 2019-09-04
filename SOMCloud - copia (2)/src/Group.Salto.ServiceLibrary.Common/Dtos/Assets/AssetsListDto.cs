using System;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Assets
{
    public class AssetsListDto
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string StockNumber { get; set; }
        public string AssetNumber { get; set; }
        public int AssetStatusId { get; set; }
        public string AssetStatusName { get; set; }
        public string StdEndDate { get; set; }
        public string BlnEndDate { get; set; }
        public string ProEndDate { get; set; }
    }
}