using Group.Salto.Common.Constants.WorkOrder;
using Group.Salto.Common.Enums;
using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.Municipality;
using Group.Salto.ServiceLibrary.Common.Contracts.PostalCode;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrder;
using Group.Salto.ServiceLibrary.Common.Dtos.Grid;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderColumns;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.WorkOrder
{
    public class WorkOrderToDataDto : IWorkOrderToDto
    {
        private readonly IMunicipalityService _municipalityService;
        private readonly IPostalCodeService _postalCodeService;
        private readonly IStatesSlaRepository _statesSlaRepository;
        private readonly IDictionary<WorkOrderColumnsEnum, Func<List<DataBaseResultDto>, WorkOrderColumnsDto, string>> dataFields = null;

        public WorkOrderToDataDto(IMunicipalityService municipalityService, IPostalCodeService postalCodeService, IStatesSlaRepository statesSlaRepository)
        {
            _municipalityService = municipalityService ?? throw new ArgumentNullException($"{nameof(IMunicipalityService)} is null ");
            _postalCodeService = postalCodeService ?? throw new ArgumentNullException($"{nameof(IPostalCodeService)} is null ");
            _statesSlaRepository = statesSlaRepository ?? throw new ArgumentNullException($"{nameof(IStatesSlaRepository)} is null ");

            dataFields = new Dictionary<WorkOrderColumnsEnum, Func<List<DataBaseResultDto>, WorkOrderColumnsDto, string>>()
            {
                { WorkOrderColumnsEnum.Id, GetDefault },
                { WorkOrderColumnsEnum.InternalIdentifier, GetDefault},
                { WorkOrderColumnsEnum.ExternalIdentifier, GetDefault },
                { WorkOrderColumnsEnum.CreationDate, GetDate },
                { WorkOrderColumnsEnum.AssignmentTime, GetDate },
                { WorkOrderColumnsEnum.PickUpTime, GetDate },
                { WorkOrderColumnsEnum.FinalClientClosingTime, GetDate },
                { WorkOrderColumnsEnum.InternalClosingTime, GetDate },
                { WorkOrderColumnsEnum.WorkOrderStatusId, GetWorkOrder },
                { WorkOrderColumnsEnum.TextRepair, GetDefault },
                { WorkOrderColumnsEnum.Observations, GetDefault },
                { WorkOrderColumnsEnum.InsertedBy, GetDefault },
                { WorkOrderColumnsEnum.WorkOrderType, GetDefault },
                { WorkOrderColumnsEnum.Project, GetDefault },
                { WorkOrderColumnsEnum.SaltoClient, GetDefault },
                { WorkOrderColumnsEnum.EndClient, GetDefault },
                { WorkOrderColumnsEnum.ResolutionDateSla, GetDate },
                { WorkOrderColumnsEnum.Phone, GetDefault },
                { WorkOrderColumnsEnum.Area, GetDefault },
                { WorkOrderColumnsEnum.Zone, GetDefault },
                { WorkOrderColumnsEnum.Subzona, GetDefault },
                { WorkOrderColumnsEnum.SiteName, GetDefault },
                { WorkOrderColumnsEnum.SitePhone, GetDefault },
                { WorkOrderColumnsEnum.Address, GetAdress },
                { WorkOrderColumnsEnum.ClosingCode, GetClosingCode },
                { WorkOrderColumnsEnum.ActionDate, GetDate },
                { WorkOrderColumnsEnum.ResponsiblePersonName, GetDefault },
                { WorkOrderColumnsEnum.Queue, GetDefault },
                { WorkOrderColumnsEnum.City, GetCity },
                { WorkOrderColumnsEnum.Province, GetDefault },
                { WorkOrderColumnsEnum.Manufacturer, GetDefault },
                { WorkOrderColumnsEnum.WorkOrderCategory, GetDefault },
                { WorkOrderColumnsEnum.ParentWOId, GetDefault },
                { WorkOrderColumnsEnum.ActuationEndDate, GetDate },
                { WorkOrderColumnsEnum.NumWOsInSite, GetNumWOsInSite },
                { WorkOrderColumnsEnum.NumSubWOs, GetEmpty },
                { WorkOrderColumnsEnum.SiteCode, GetDefault },
                { WorkOrderColumnsEnum.AssetSerialNumber, GetDefault },
                { WorkOrderColumnsEnum.AssetNumber, GetDefault },
                { WorkOrderColumnsEnum.Maintenance, GetMaintenance },
                { WorkOrderColumnsEnum.StandardWarranty, GetManufacturerWarranty },
                { WorkOrderColumnsEnum.ManufacturerWarranty,  GetManufacturerWarranty },
                { WorkOrderColumnsEnum.ExternalWorOrderStatus, GetWorkOrder },
                { WorkOrderColumnsEnum.TotalWorkedTime, GetDefault },
                { WorkOrderColumnsEnum.OnSiteTime , GetDefault },
                { WorkOrderColumnsEnum.TravelTime, GetDefault },
                { WorkOrderColumnsEnum.Kilometers, GetDefault },
                { WorkOrderColumnsEnum.NumberOfVisitsToClient, GetDefault },
                { WorkOrderColumnsEnum.NumberOfIntervention , GetDefault },
                { WorkOrderColumnsEnum.MeetResolutionSLA , GetDefault },
                { WorkOrderColumnsEnum.MeetResponseSLA , GetDefault },
                { WorkOrderColumnsEnum.ClosingWODate , GetDate }
            };
        }

        public List<GridDataDto> ToGridDataDtos(GridDataParams gridDataParams)
        {
            List<GridDataDto> listGridData = new List<GridDataDto>();
            foreach (List<DataBaseResultDto> workOrder in gridDataParams.Data)
            {
                GridDataDto gridData = new GridDataDto();
                foreach (WorkOrderColumnsDto col in gridDataParams.Columns)
                {
                    Func<List<DataBaseResultDto>, WorkOrderColumnsDto, string> action;
                    if (dataFields.TryGetValue((WorkOrderColumnsEnum)col.Id, out action))
                    {
                        DataBaseResultDto workOrderCell = workOrder.Where(x => x.Name.StartsWith(col.Name)).FirstOrDefault();
                        if (workOrderCell != null)
                        {
                            col.IsExcelMode = gridDataParams.IsExcelMode;
                            string result = action.Invoke(workOrder, col);
                            gridData.Data.Add(result);
                        }
                        else
                        {
                            gridData.Data.Add(string.Empty);
                        }
                    }
                }

                DataBaseResultDto id = workOrder.Find(x => x.Name == WorkOrderConstants.WorkOrderId);
                gridData.Id = Convert.ToInt32(id.Value);
                if (!gridDataParams.IsExcelMode)
                {
                    DataBaseResultDto slaId = workOrder.Find(x => x.Name == WorkOrderConstants.SlaId);
                    if (slaId != null)
                    {
                        gridData.Data.Add(GetSLAColor(workOrder, WorkOrderConstants.SlaId));
                    }
                    gridData.Data.Insert(0, "0");
                }
                listGridData.Add(gridData);
            }
            return listGridData;
        }

        private string GetDefault(List<DataBaseResultDto> data, WorkOrderColumnsDto column)
        {
            DataBaseResultDto result = data.Find(x => x.Name == column.Name);
            return result?.Value.ToString() ?? string.Empty;
        }

        private string GetDate(List<DataBaseResultDto> data, WorkOrderColumnsDto column)
        {
            DataBaseResultDto result = data.Find(x => x.Name == column.Name);
            return result?.Value.ToString() ?? string.Empty;
        }

        private string GetWorkOrder(List<DataBaseResultDto> data, WorkOrderColumnsDto column)
        {
            DataBaseResultDto resultName = data.Find(x => x.Name == column.Name + "_Name");
            if (!column.IsExcelMode)
            {
                DataBaseResultDto resultColor = data.Find(x => x.Name == column.Name + "_Color");
                return $"<span class='button expand tiny x-padding no-margin badge badge-pill' style='background-color:{resultColor.Value};color:white;'>{resultName.Value}</span>";
            }
            else
            {
                return resultName.Value.ToString();
            }
        }

        private string GetEmpty(List<DataBaseResultDto> data, WorkOrderColumnsDto column)
        {
            return string.Empty;
        }

        private string GetCity(List<DataBaseResultDto> data, WorkOrderColumnsDto column)
        {
            string city = string.Empty;
            DataBaseResultDto result = data.Find(x => x.Name == column.Name);
            if (result.Value != null)
            {
                var municipalities = _municipalityService.GetByIdWithStatesRegionsCountriesIncludes(Convert.ToInt32(result.Value));
                city = municipalities?.Name;
            }

            return city;
        }

        private string GetProvince(List<DataBaseResultDto> data, WorkOrderColumnsDto column)
        {
            string province = string.Empty;
            DataBaseResultDto result = data.Find(x => x.Name == column.Name);
            if (result.Value != null)
            {
                var municipalities = _municipalityService.GetByIdWithStatesRegionsCountriesIncludes(Convert.ToInt32(result.Value));
                province = municipalities?.State?.Name;
            }

            return province;
        }

        private string GetAdress(List<DataBaseResultDto> data, WorkOrderColumnsDto column)
        {
            string adr = string.Empty, adr2 = string.Empty, adr3 = string.Empty;
            try
            {
                DataBaseResultDto resultStreetType = data.Find(x => x.Name == column.Name + "_StreetType");
                DataBaseResultDto resultStreet = data.Find(x => x.Name == column.Name + "_Street");
                DataBaseResultDto resultNumber = data.Find(x => x.Name == column.Name + "_Number");
                DataBaseResultDto resultEscala = data.Find(x => x.Name == column.Name + "_Escala");
                DataBaseResultDto resultGateNumber = data.Find(x => x.Name == column.Name + "_GateNumber");
                DataBaseResultDto resultMunicipalityId = data.Find(x => x.Name == column.Name + "_MunicipalityId");
                DataBaseResultDto resultPostalCodeId = data.Find(x => x.Name == column.Name + "_PostalCodeId");

                if (resultStreetType.Value != null && !string.IsNullOrWhiteSpace(resultStreetType.Value.ToString())) adr += resultStreetType.Value;
                if (resultStreet.Value != null && !string.IsNullOrWhiteSpace(resultStreet.Value.ToString())) adr += " " + resultStreet.Value;
                if (resultNumber.Value != null && !string.IsNullOrWhiteSpace(resultNumber.Value.ToString()) && Convert.ToInt32(resultNumber.Value) > 0) adr += " " + resultNumber.Value;
                if (resultEscala.Value != null && !string.IsNullOrWhiteSpace(resultEscala.Value.ToString())) adr += ", " + resultEscala.Value;
                if (resultGateNumber.Value != null && !string.IsNullOrWhiteSpace(resultGateNumber.Value.ToString())) adr += " " + resultGateNumber.Value;

                if (resultMunicipalityId.Value != null)
                {
                    var municipalities = _municipalityService.GetByIdWithStatesRegionsCountriesIncludes(Convert.ToInt32(resultMunicipalityId.Value));
                    var postalCode = _postalCodeService.GetById(Convert.ToInt32(resultPostalCodeId.Value));
                    if (municipalities != null)
                    {
                        adr2 = municipalities.Cities?.FirstOrDefault()?.Name ?? string.Empty;
                        if (postalCode != null)
                        {
                            adr2 += $"({postalCode.PostalCode})";
                        }
                        adr2 += " - " + municipalities.Name;

                        adr3 += municipalities.State?.Name;
                        adr3 += " - " + municipalities.State?.Region?.Name;
                        adr3 += " - " + municipalities.State?.Region?.Country?.Name;
                    }
                }
            }
            catch (Exception ex) { }

            return string.Join(" ", new List<string> { adr, adr2, adr3 });
        }

        private string GetClosingCode(List<DataBaseResultDto> data, WorkOrderColumnsDto column)
        {
            return string.Empty;
        }

        private string GetMaintenance(List<DataBaseResultDto> data, WorkOrderColumnsDto column)
        {
            DataBaseResultDto startDate = data.Find(x => x.Name == column.Name + "_StdStartDate");
            DataBaseResultDto endDate = data.Find(x => x.Name == column.Name + "_StdEndDate");
            return CompareGuaranteeDates(startDate, endDate);
        }

        private string GetStandardWarranty(List<DataBaseResultDto> data, WorkOrderColumnsDto column)
        {
            DataBaseResultDto startDate = data.Find(x => x.Name == column.Name + "_BlnStartDate");
            DataBaseResultDto endDate = data.Find(x => x.Name == column.Name + "_BlnEndDate");
            return CompareGuaranteeDates(startDate, endDate);
        }

        private string GetManufacturerWarranty(List<DataBaseResultDto> data, WorkOrderColumnsDto column)
        {
            DataBaseResultDto startDate = data.Find(x => x.Name == column.Name + "_ProStartDate");
            DataBaseResultDto endDate = data.Find(x => x.Name == column.Name + "_ProEndDate");
            return CompareGuaranteeDates(startDate, endDate);
        }

        private string GetNumWOsInSite(List<DataBaseResultDto> data, WorkOrderColumnsDto column)
        {
            DataBaseResultDto result = data.Find(x => x.Name == column.Name);
            if (result.Value.ToString() == "0")
            {
                return string.Empty;
            }
            return (!column.IsExcelMode) ? $"<span class='button expand tiny x-padding no-margin badge badge-pill' style='background-color:#0bba9b;color:black;'>{result?.Value.ToString()}</span>" : result?.Value.ToString();
        }

        private string CompareGuaranteeDates(DataBaseResultDto startDate, DataBaseResultDto endDate)
        {
            if (startDate.Value != null && endDate.Value != null)
            {
                var today = DateTime.Now;
                return (today <= Convert.ToDateTime(endDate.Value) && today >= Convert.ToDateTime(startDate.Value)).ToString();
            }
            return "false";
        }

        private string GetSLAColor(List<DataBaseResultDto> data, string colName)
        {
            string color = string.Empty;
            DataBaseResultDto dateSLA = data.Find(x => x.Name == WorkOrderColumnsEnum.ResolutionDateSla.ToString());
            if (dateSLA != null && dateSLA.Value != DBNull.Value)
            {
                DataBaseResultDto slaId = data.Find(x => x.Name == colName);

                double minutes = (Convert.ToDateTime(dateSLA.Value) - DateTime.Now).TotalMinutes;
                color = _statesSlaRepository.GetColor(Convert.ToInt32(minutes), Convert.ToInt32(slaId.Value));

                return color ?? "#ffffff";
            }
            return color;
        }
    }
}