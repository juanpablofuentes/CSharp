using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Common.Constants.ErpSystemInstanceQuery;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.Analysis;
using Group.Salto.ServiceLibrary.Common.Contracts.ErpSystemInstanceQuery;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Analysis;

namespace Group.Salto.ServiceLibrary.Implementations.Analysis
{
    public class BillAnalysisService : IBillAnalysisService
    {
        private readonly IErpSystemInstanceQueryService _erpSystemInstanceQueryService;

        public BillAnalysisService(IErpSystemInstanceQueryService erpSystemInstanceQueryService)
        {
            _erpSystemInstanceQueryService = erpSystemInstanceQueryService;
        }

        public ResultDto<bool> AddAllBillsToAnalize(WorkOrders currentWorkOrder)
        {
            var result = new ResultDto<bool> { Data = true };

            try
            {
                var billLines = currentWorkOrder.Bill.SelectMany(b => b.BillLine);
                foreach (var billLine in billLines)
                {
                    if (billLine.DnAndMaterialsAnalysis != null && billLine.Item != null)
                    {
                        UpdateBillLineToAnalize(billLine);
                    }
                    else if (billLine.Item != null)
                    {
                        AddBillLineToAnalize(billLine);
                    }
                }
            }
            catch (Exception e)
            {
                result.Data = false;
                result.Errors = new ErrorsDto { Errors = new List<ErrorDto> { new ErrorDto { ErrorType = ErrorType.ValidationError, ErrorMessageKey = e.ToString() } } };
            }

            return result;
        }

        private void AddBillLineToAnalize(BillLine billLine)
        {
            var billAnalysisValues = GetBillAnalysisValues(billLine);

            var newAnalysis = new DnAndMaterialsAnalysis
            {
                BillLine = billLine,
                BillEntity = billLine.Bill,
                WorkOrderEntity = billLine.Bill.Workorder,
                PeopleEntity = billLine.Bill.People,
                ExternalSystemNumber = billLine.Bill.ExternalSystemNumber,
                Status = billLine.Bill.Status,
                Item = billLine.Item,
                ItemName = billLine.Item.Name,
                ItemSerialNumber = billLine.SerialNumber,
                Units = (decimal?)billLine.Units,
                PVP = billAnalysisValues.SalePrice ?? 0,
                PurchaseCost = billAnalysisValues.PurchasePrice ?? 0,
                TotalDeliveryNoteCost = (decimal?)billLine.Units * billAnalysisValues.PurchasePrice,
                TotalDeliveryNoteSalePrice = (decimal?)billLine.Units * billAnalysisValues.SalePrice,
                UpdateDate = DateTime.UtcNow
            };

            billLine.DnAndMaterialsAnalysis = newAnalysis;
        }

        private void UpdateBillLineToAnalize(BillLine billLine)
        {
            var billAnalysisValues = GetBillAnalysisValues(billLine);

            billLine.DnAndMaterialsAnalysis.ExternalSystemNumber = billLine.Bill.ExternalSystemNumber;
            billLine.DnAndMaterialsAnalysis.Status = billLine.Bill.Status;
            billLine.DnAndMaterialsAnalysis.Item = billLine.Item;
            billLine.DnAndMaterialsAnalysis.ItemName = billLine.Item.Name;
            billLine.DnAndMaterialsAnalysis.ItemSerialNumber = billLine.SerialNumber;
            billLine.DnAndMaterialsAnalysis.Units = (decimal?)billLine.Units;
            billLine.DnAndMaterialsAnalysis.PVP = billAnalysisValues.SalePrice;
            billLine.DnAndMaterialsAnalysis.PurchaseCost = billAnalysisValues.PurchasePrice;
            billLine.DnAndMaterialsAnalysis.TotalDeliveryNoteCost = (decimal?)billLine.Units * billAnalysisValues.PurchasePrice;
            billLine.DnAndMaterialsAnalysis.TotalDeliveryNoteSalePrice = (decimal?)billLine.Units * billAnalysisValues.SalePrice;
            billLine.DnAndMaterialsAnalysis.UpdateDate = DateTime.UtcNow;
        }

        private BillAnalysisValuesDto GetBillAnalysisValues(BillLine billLine)
        {
            var salePrice = _erpSystemInstanceQueryService.GetErpPriceFromItem(billLine.ItemId, ErpSystemInstanceQueryConstants.SalePrice);
            var purchasePrice = _erpSystemInstanceQueryService.GetErpPriceFromItem(billLine.ItemId, ErpSystemInstanceQueryConstants.PurchasePrice);

            return new BillAnalysisValuesDto
            {
                SalePrice = salePrice,
                PurchasePrice = purchasePrice
            };
        }
    }
}
