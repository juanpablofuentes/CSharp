using Group.Salto.Common.Constants.WorkOrder;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Billing;
using Group.Salto.ServiceLibrary.Common.Dtos.BillLines;
using Group.Salto.ServiceLibrary.Common.Dtos.Grid;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderColumns;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.Grid;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.WorkOrder;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class WorkOrderDetailViewModelExtensions
    {
        public static WorkOrderDetailViewModel ToDetailViewModel(this WorkOrderDetailDto source)
        {
            WorkOrderDetailViewModel result = null;
            if (source != null)
            {
                result = new WorkOrderDetailViewModel()
                {
                    GenericDetailViewModel = new GenericDetailViewModel
                    {
                        Id = source.Id,
                        InternalIdentifier = source.InternalIdentifier,
                        WorkOrderStatus = source.WorkOrderStatus,
                        ExternalWorOrderStatus = source.ExternalWorOrderStatus,
                        Queue = source.Queue,
                        WorkOrderCategory = source.WorkOrderCategory,
                        WorkOrderCategoryURL = source.WorkOrderCategoryURL,
                        WorkOrderTypes = source.WorkOrderTypes,
                        FinalClientName = source.FinalClient,
                        LocationCode = source.LocationCode,
                        LocationName = source.LocationName,
                        LocationPhone = source.LocationPhone,
                        ProjectName = source.Project,
                        LocationAddress = source.LocationAddress,
                        PeopleResponsibleName = source.PeopleResponsibleName,
                        PeopleResponsiblePhone = source.PeopleResponsiblePhone,
                        ActionDate = source.ActionDate,
                        ResolutionDateSla = source.ResolutionDateSla,
                        TextRepair = source.TextRepair,
                        Observations = source.Observations,
                        BrandURL = source.BrandURL,
                        ExpectedWorkedTime = source.ExpectedWorkedTime,
                        TotalWorkedTime = source.TotalWorkedTime,
                        ExpectedVSTotalTime = source.ExpectedVSTotalTime,
                        Km = source.Km,
                        TravelTime = source.TravelTime,
                        WaitTime = source.WaitTime,
                        OnSiteTime = source.OnSiteTime,
                        ShowHeader = source.ShowHeader,
                        StatusColor = source.StatusColor,
                        AssetId = source.AssetId,
                        FatherId = source.FatherId,
                        ReferenceOtherService = source.ReferenceOtherServices,
                        GeneratedService = source.GeneratedService,
                        GeneratedServiceId = source.GeneratedServiceId,
                    }
                };
            }
            return result;
        }

        public static ResultViewModel<WorkOrderDetailViewModel> ToDetailViewModel(this ResultDto<WorkOrderDetailDto> source)
        {
            var response = source != null ? new ResultViewModel<WorkOrderDetailViewModel>()
            {
                Data = source.Data.ToDetailViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static RootGrid ToRootSubPetitionsGrid(this IList<WorkOrdersSubWODto> source)
        {
            RootGrid rootObject = new RootGrid();
            rootObject.Head.Add(new Head() { Width = "70", Align = WorkOrderConstants.WorkOrderGridAlignLeft, Type = WorkOrderConstants.WorkOrderSubType, Sort = WorkOrderConstants.WorkOrderSubSort, Value = LocalizedExtensions.GetUILocalizedText(WorkOrderConstants.WorkOrderSubId) });
            rootObject.Head.Add(new Head() { Width = "*", Align = WorkOrderConstants.WorkOrderGridAlignLeft, Type = WorkOrderConstants.WorkOrderSubType, Sort = WorkOrderConstants.WorkOrderSubSort, Value = LocalizedExtensions.GetUILocalizedText(WorkOrderConstants.WorkOrderSubResolutionDateSla) });
            rootObject.Head.Add(new Head() { Width = "*", Align = WorkOrderConstants.WorkOrderGridAlignLeft, Type = WorkOrderConstants.WorkOrderSubType, Sort = WorkOrderConstants.WorkOrderSubSort, Value = LocalizedExtensions.GetUILocalizedText(WorkOrderConstants.WorkOrderSubTimingCreation) });
            rootObject.Head.Add(new Head() { Width = "*", Align = WorkOrderConstants.WorkOrderGridAlignLeft, Type = WorkOrderConstants.WorkOrderSubType, Sort = WorkOrderConstants.WorkOrderSubSort, Value = LocalizedExtensions.GetUILocalizedText(WorkOrderConstants.WorkOrderSubInternalIdentifier) });
            rootObject.Head.Add(new Head() { Width = "*", Align = WorkOrderConstants.WorkOrderGridAlignLeft, Type = WorkOrderConstants.WorkOrderSubType, Sort = WorkOrderConstants.WorkOrderSubSort, Value = LocalizedExtensions.GetUILocalizedText(WorkOrderConstants.WorkOrderSubStatus) });
            rootObject.Head.Add(new Head() { Width = "*", Align = WorkOrderConstants.WorkOrderGridAlignLeft, Type = WorkOrderConstants.WorkOrderSubType, Sort = WorkOrderConstants.WorkOrderSubSort, Value = LocalizedExtensions.GetUILocalizedText(WorkOrderConstants.WorkOrderSubActionDate) });
            rootObject.Head.Add(new Head() { Width = "*", Align = WorkOrderConstants.WorkOrderGridAlignLeft, Type = WorkOrderConstants.WorkOrderSubType, Sort = WorkOrderConstants.WorkOrderSubSort, Value = LocalizedExtensions.GetUILocalizedText(WorkOrderConstants.WorkOrderSubPeopleResponsible) });
            rootObject.Head.Add(new Head() { Width = "*", Align = WorkOrderConstants.WorkOrderGridAlignLeft, Type = WorkOrderConstants.WorkOrderSubType, Sort = WorkOrderConstants.WorkOrderSubSort, Value = LocalizedExtensions.GetUILocalizedText(WorkOrderConstants.WorkOrderSubWorkOrderCategory) });
            rootObject.Head.Add(new Head() { Width = "*", Align = WorkOrderConstants.WorkOrderGridAlignLeft, Type = WorkOrderConstants.WorkOrderSubType, Sort = WorkOrderConstants.WorkOrderSubSort, Value = LocalizedExtensions.GetUILocalizedText(WorkOrderConstants.WorkOrderSubProject) });
            rootObject.Head.Add(new Head() { Width = "*", Align = WorkOrderConstants.WorkOrderGridAlignLeft, Type = WorkOrderConstants.WorkOrderSubType, Sort = WorkOrderConstants.WorkOrderSubSort, Value = LocalizedExtensions.GetUILocalizedText(WorkOrderConstants.WorkOrderSubQueue) });
            rootObject.Head.Add(new Head() { Width = "*", Align = WorkOrderConstants.WorkOrderGridAlignLeft, Type = WorkOrderConstants.WorkOrderSubType, Sort = WorkOrderConstants.WorkOrderSubSort, Value = LocalizedExtensions.GetUILocalizedText(WorkOrderConstants.WorkOrderSubTimingAssigned) });

            foreach (WorkOrdersSubWODto subPetitions in source)
            {
                List<string> data = new List<string>
                {
                    subPetitions.Id.ToString(),
                    subPetitions.ResolutionDateSla,
                    subPetitions.TimingCreation,
                    subPetitions.InternalIdentifier,
                    subPetitions.WorkOrderStatus,
                    subPetitions.ActionDate,
                    subPetitions.PeopleResponsible ?? string.Empty,
                    subPetitions.WorkOrderCategory ?? string.Empty,
                    subPetitions.Project,
                    subPetitions.Queue,
                    subPetitions.TimingAssigned
                };
                rootObject.Rows.Add(new Row() { Id = subPetitions.Id, Data = data });
            }
            rootObject.Total_count = source.Count;
            return rootObject;
        }

        public static RootGrid ToRootBillGrid(this IList<BillDto> source)
        {
            RootGrid rootObject = new RootGrid();
            rootObject.Head.Add(new Head() { Width = "*", Align = WorkOrderConstants.WorkOrderGridAlignLeft, Type = WorkOrderConstants.WorkOrderSubType, Sort = WorkOrderConstants.WorkOrderSubSort, Value = LocalizedExtensions.GetUILocalizedText(WorkOrderConstants.WorkOrderDeliveryNotesNumber) });
            rootObject.Head.Add(new Head() { Width = "*", Align = WorkOrderConstants.WorkOrderGridAlignLeft, Type = WorkOrderConstants.WorkOrderSubType, Sort = WorkOrderConstants.WorkOrderSubSort, Value = LocalizedExtensions.GetUILocalizedText(WorkOrderConstants.WorkOrderDeliveryNotesExternalSystemReference) });
            rootObject.Head.Add(new Head() { Width = "*", Align = WorkOrderConstants.WorkOrderGridAlignLeft, Type = WorkOrderConstants.WorkOrderSubType, Sort = WorkOrderConstants.WorkOrderSubSort, Value = LocalizedExtensions.GetUILocalizedText(WorkOrderConstants.WorkOrderDeliveryNotesDate) });
            rootObject.Head.Add(new Head() { Width = "*", Align = WorkOrderConstants.WorkOrderGridAlignLeft, Type = WorkOrderConstants.WorkOrderSubType, Sort = WorkOrderConstants.WorkOrderSubSort, Value = LocalizedExtensions.GetUILocalizedText(WorkOrderConstants.WorkOrderDeliveryNotesTask) });
            rootObject.Head.Add(new Head() { Width = "*", Align = WorkOrderConstants.WorkOrderGridAlignRight, Type = WorkOrderConstants.WorkOrderSubType, Sort = WorkOrderConstants.WorkOrderSubSort, Value = LocalizedExtensions.GetUILocalizedText(WorkOrderConstants.WorkOrderDeliveryNotesLines) });

            foreach (BillDto bill in source)
            {
                List<string> data = new List<string>
                {
                    bill.DeliveryNotesId.ToString(),
                    bill.ExternalSystemNumber,
                    bill.Date,
                    bill.Task,
                    bill.DeliveryNotesLines.ToString()
                };
                rootObject.Rows.Add(new Row() { Id = bill.Id, Data = data });
            }
            rootObject.Total_count = source.Count;
            return rootObject;
        }

        public static RootGrid ToRootBillLinesGrid(this IList<BillLinesDto> source)
        {
            RootGrid rootObject = new RootGrid();
            rootObject.Head.Add(new Head() { Width = "*", Align = WorkOrderConstants.WorkOrderGridAlignLeft, Type = WorkOrderConstants.WorkOrderSubType, Sort = WorkOrderConstants.WorkOrderSubSort, Value = LocalizedExtensions.GetUILocalizedText(WorkOrderConstants.WorkOrderDeliveryNotesId) });
            rootObject.Head.Add(new Head() { Width = "*", Align = WorkOrderConstants.WorkOrderGridAlignLeft, Type = WorkOrderConstants.WorkOrderSubType, Sort = WorkOrderConstants.WorkOrderSubSort, Value = LocalizedExtensions.GetUILocalizedText(WorkOrderConstants.WorkOrderDeliveryNotesName) });
            rootObject.Head.Add(new Head() { Width = "*", Align = WorkOrderConstants.WorkOrderGridAlignRight, Type = WorkOrderConstants.WorkOrderSubType, Sort = WorkOrderConstants.WorkOrderSubSort, Value = LocalizedExtensions.GetUILocalizedText(WorkOrderConstants.WorkOrderDeliveryNotesUnits) });

            foreach (BillLinesDto billLine in source)
            {
                List<string> data = new List<string>
                {
                    billLine.Id.ToString(),
                    billLine.Name,
                    billLine.Units.ToString()
                };
                rootObject.Rows.Add(new Row() { Id = billLine.Id, Data = data });
            }
            rootObject.Total_count = source.Count;
            return rootObject;
        }

        public static RootGrid ToWorkOrderAsset(this IList<WorkOrderAssetsDto> source)
        {
            RootGrid rootObject = new RootGrid();
            rootObject.Head.Add(new Head() { Width = "*", Align = WorkOrderConstants.WorkOrderGridAlignLeft, Type = WorkOrderConstants.WorkOrderSubType, Sort = WorkOrderConstants.WorkOrderSubSort, Value = LocalizedExtensions.GetUILocalizedText(WorkOrderConstants.WorkOrderEditAssetsGridId) });
            rootObject.Head.Add(new Head() { Width = "*", Align = WorkOrderConstants.WorkOrderGridAlignLeft, Type = WorkOrderConstants.WorkOrderSubType, Sort = WorkOrderConstants.WorkOrderSubSort, Value = LocalizedExtensions.GetUILocalizedText(WorkOrderConstants.WorkOrderEditAssetsGridInternal) });
            rootObject.Head.Add(new Head() { Width = "*", Align = WorkOrderConstants.WorkOrderGridAlignLeft, Type = WorkOrderConstants.WorkOrderSubType, Sort = WorkOrderConstants.WorkOrderSubSort, Value = LocalizedExtensions.GetUILocalizedText(WorkOrderConstants.WorkOrderEditAssetsGridCreationDate) });
            rootObject.Head.Add(new Head() { Width = "*", Align = WorkOrderConstants.WorkOrderGridAlignLeft, Type = WorkOrderConstants.WorkOrderSubType, Sort = WorkOrderConstants.WorkOrderSubSort, Value = LocalizedExtensions.GetUILocalizedText(WorkOrderConstants.WorkOrderEditAssetsGridEndingDate) });

            foreach (WorkOrderAssetsDto asset in source)
            {
                List<string> data = new List<string>
                {
                    asset.Id.ToString(),
                    asset.InternalIdentifier,
                    asset.CreationDate,
                    asset.EndingDate
                };
                rootObject.Rows.Add(new Row() { Id = asset.Id, Data = data });
            }
            rootObject.Total_count = source.Count;
            return rootObject;
        }
    }
}