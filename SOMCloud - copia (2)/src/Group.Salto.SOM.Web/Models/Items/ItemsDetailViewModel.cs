namespace Group.Salto.SOM.Web.Models.Items
{
    public class ItemsDetailViewModel
    {
        public ItemGeneralViewModel GeneralViewModel {get;set;}
        public ItemPointsViewModel PointsViewModel {get;set;}
        public ItemPurchasesViewModel PurchasesViewModel {get;set;}
        public ItemSalesViewModel SalesViewModel {get;set;}
        public SerialNumbersTrackingViewModel TrackingViewModel { get; set; }
    }
}