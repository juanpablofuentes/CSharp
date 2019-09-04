namespace Group.Salto.ServiceLibrary.Common.Dtos.Assets
{
    public class AssetForWorkOrderDetailsDto
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string StockNumber { get; set; }
        public string AssetNumber { get; set; }
        public string Status { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Maintenance { get; set; }
        public string MaintenanceStartDate { get; set; }
        public string MaintenanceEndDate { get; set; }
        public string StandardWarranty { get; set; }
        public string StandardWarrantyStartDate { get; set; }
        public string StandardWarrantyEndDate { get; set; }
        public string ManufacturerWarranty { get; set; }
        public string ManufacturerWarrantyStartDate { get; set; }
        public string ManufacturerWarrantyEndDate { get; set; }
    }
}