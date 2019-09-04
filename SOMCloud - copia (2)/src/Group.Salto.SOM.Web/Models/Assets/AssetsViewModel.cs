using System;

namespace Group.Salto.SOM.Web.Models.Assets
{
    public class AssetsViewModel
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string StockNumber { get; set; }
        public string AssetNumber { get; set; }
        public int AssetStatusId { get; set; }
        public string AssetStatusName { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Location { get; set; }
        public string StdEndDate { get; set; }
        public string BlnEndDate { get; set; }
        public string ProEndDate { get; set; }
    }
}