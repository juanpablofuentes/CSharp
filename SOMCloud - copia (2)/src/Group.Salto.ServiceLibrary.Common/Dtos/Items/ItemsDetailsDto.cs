using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Items
{
    public class ItemsDetailsDto : ItemsListDto
    {
        public bool TrackSerialNumber { get; set; }
        public bool InDepot { get; set; }
        public byte[] Picture { get; set; }
        public int AvailableQuantity { get; set; }
        public KeyValuePair<int?,string> SelectedSubFamily { get; set; }
        public KeyValuePair<int?,string> SelectedFamily { get; set; }
        public IList<RateDto> ItemPurchaseRates { get; set; }
        public IList<RateDto> ItemPointsRates { get; set; }
        public IList<RateDto> ItemSalesRates { get; set; }
        public IList<RateDto> AvailablePurchaseRates { get; set; }
        public IList<RateDto> AvailablePointsRates { get; set; }
        public IList<RateDto> AvailableSalesRates { get; set; }
        public IList<ItemsSerialNumberDto> ItemsSerialNumbers { get; set; } 
    }
}