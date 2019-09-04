using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ServiceGauges;
using Group.Salto.ServiceLibrary.Common.Dtos.ServiceGauges;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Analysis;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Group.Salto.ServiceLibrary.Implementations.ServiceGauges
{
    public class ServiceGaugesService : BaseService, IServiceGaugesService
    {
        private readonly IServiceGaugesRepository _analysisRepository;
        public ServiceGaugesService(ILoggingService logginingService,
            IServiceGaugesRepository analysisRepository) : base(logginingService)
        {
            _analysisRepository = analysisRepository ?? throw new ArgumentNullException($"{nameof(IServiceGaugesRepository)} is null");
        }

        public ServiceGaugesResultFilterDto ServiceAnalysis(ServiceGaugesFilterDto data)
        {
            var result = new ServiceGaugesResultFilterDto();
            var date1 = DateTime.UtcNow;
            var date2 = DateTime.UtcNow.AddDays(1);
            var month1 = DateTime.UtcNow;
            var month2 = DateTime.UtcNow.AddMonths(-2);
            string dateformat = "yyy-MM-dd H:mm:ss";

            var MultiInfo = "SELECT AVG(CAST(NumberOfVisitsToClient AS DECIMAL(10,2))) AS AVERAGE_VISITS, "
                        + "MAX(NumberOfVisitsToClient) AS MAX_VISITS, "
                        + "AVG(CAST(OnSiteTime AS DECIMAL(10, 2))) AS AVERAGE_ONSITETIME, "
                        + "MAX(OnSiteTime) AS MAX_ONSITETIME, "
                        + "AVG(CAST(TravelTime AS DECIMAL(10, 2))) AS AVERAGE_TRAVELTIME, "
                        + "MAX(TravelTime) AS MAX_TRAVELTIME, "
                        + "AVG(Kilometers) AS AVERAGE_KILOMETERS, "
                        + "MAX(Kilometers) AS MAX_KILOMETERS, "
                        + "AVG(CAST(WaitTime AS DECIMAL(10, 2))) AS AVERAGE_WAITTIME, "
                        + "MAX(WaitTime) AS MAX_WAITTIME, "
                        + "COUNT(WorkOrderCode) AS TOTAL_ORDERS "
                        + "FROM WorkOrderAnalysis w ";

            var ClosedToday = "SELECT COUNT(WorkOrderCode) AS CLOSED_TODAY "
                        + "FROM WorkOrderAnalysis w ";

            var OpenToday = "SELECT COUNT(w.id) AS OPENED_TODAY "
                        + "FROM WorkOrders w ";

            var Assets = "SELECT TOP 4 e.SerialNumber AS EQUIPO, COUNT(w.Id) AS AVISOS "
                        + "FROM Assets e INNER JOIN WorkOrders w ON e.Id=w.Id ";

            var GetMonthlyProjectCost = "SELECT WorkOrderCategory, SUM(ISNULL(TotalWOExpensesCost,0)) + SUM(ISNULL(TotalWOProductionCost,0)) + SUM(ISNULL(TotalWOTravelTimeCost, 0)) + SUM(ISNULL(TotalWOSubcontractorCost, 0)) + SUM(ISNULL(TotalWOMaterialsCost, 0)) Cost,"
                        + " FORMAT(ClosingWODate, 'yyyy - M') as NumberMonth, DATEPART(MONTH, ClosingWODate), DATEPART(YEAR, ClosingWODate) "
                        + " FROM WorkOrderAnalysis ";

            var GetMonthlyProjectRevenue = "SELECT WorkOrderCategory, SUM(ISNULL(TotalWOSalesAmount, 0)) Revenue, "
                        + " FORMAT(ClosingWODate, 'yyyy - M') NumberMonth , DATEPART(MONTH, ClosingWODate), DATEPART(YEAR, ClosingWODate) "
                        + " FROM WorkOrderAnalysis ";

            var GetMonthlyProjectMargin = "SELECT WorkOrderCategory, SUM(ISNULL(GrossMargin, 0)) Margin, " 
                        + " FORMAT(ClosingWODate, 'yyyy - M') NumberMonth , DATEPART(MONTH, ClosingWODate), DATEPART(YEAR, ClosingWODate) " 
                        + " FROM WorkOrderAnalysis ";

            var ResponseSlaOk = "SELECT COUNT(WorkOrderCode) AS ResponseSlaOk From WorkOrderAnalysis ";

            var ResolutionSlaOk = "SELECT COUNT(WorkOrderCode) AS ResolutionSlaOk From WorkOrderAnalysis ";
            var TotalSla = "SELECT COUNT(WorkOrderCode) AS total From WorkOrderAnalysis ";


            if (data.ClientId != 0)
            {
                MultiInfo = MultiInfo + " Left Join Contracts c ON c.ClientId = " + data.ClientId.ToString() + " Left Join Projects p ON p.ContractId = c.Id where p.Id = w.ProjectCode AND ";
                ClosedToday = ClosedToday + " Left Join Contracts c ON c.ClientId = " + data.ClientId.ToString() + " Left Join Projects p ON p.ContractId = c.Id where p.Id = w.ProjectCode AND ";
                OpenToday = OpenToday + " Left Join Contracts c ON c.ClientId = " + data.ClientId.ToString() + " Left Join Projects p ON p.ContractId = c.Id where p.Id = w.ProjectId AND ";
                Assets = Assets + " Left Join Contracts c ON c.ClientId = " + data.ClientId.ToString() + " Left Join Projects p ON p.ContractId = c.Id where p.Id = w.ProjectId AND ";
                GetMonthlyProjectCost = GetMonthlyProjectCost + " Left Join Contracts c ON c.ClientId = " + data.ClientId.ToString() + " Left Join Projects p ON p.ContractId = c.Id where p.Id = WorkOrderAnalysis.ProjectCode AND ";
                GetMonthlyProjectRevenue = GetMonthlyProjectRevenue + " Left Join Contracts c ON c.ClientId = " + data.ClientId.ToString() + " Left Join Projects p ON p.ContractId = c.Id where p.Id = WorkOrderAnalysis.ProjectCode AND ";
                GetMonthlyProjectMargin = GetMonthlyProjectMargin + " Left Join Contracts c ON c.ClientId = " + data.ClientId.ToString() + " Left Join Projects p ON p.ContractId = c.Id where p.Id = WorkOrderAnalysis.ProjectCode AND ";
                ResponseSlaOk = ResponseSlaOk + " Left Join Contracts c ON c.ClientId = " + data.ClientId.ToString() + " Left Join Projects p ON p.ContractId = c.Id where p.Id = ProjectCode AND ";
                ResolutionSlaOk = ResolutionSlaOk + " Left Join Contracts c ON c.ClientId = " + data.ClientId.ToString() + " Left Join Projects p ON p.ContractId = c.Id where p.Id = ProjectCode AND ";
                TotalSla = TotalSla + " Left Join Contracts c ON c.ClientId = " + data.ClientId.ToString() + " Left Join Projects p ON p.ContractId = c.Id where p.Id = ProjectCode AND ";
            }
            else
            {
                MultiInfo = MultiInfo + " Where ";
                ClosedToday = ClosedToday + " Where ";
                OpenToday = OpenToday + " Where ";
                Assets = Assets + " Where ";
                GetMonthlyProjectCost = GetMonthlyProjectCost + " Where ";
                GetMonthlyProjectRevenue = GetMonthlyProjectRevenue + " Where ";
                GetMonthlyProjectMargin = GetMonthlyProjectMargin + " Where ";
                ResponseSlaOk = ResponseSlaOk + " Where ";
                ResolutionSlaOk = ResolutionSlaOk + " Where ";
                TotalSla = TotalSla + " Where ";
            }

            ClosedToday = ClosedToday + " w.ClosingWODate BETWEEN '" + date1.ToString(dateformat) + "' AND '" + date2.ToString(dateformat) + "' AND ";
            OpenToday = OpenToday + " w.CreationDate BETWEEN '" + date1.ToString(dateformat) + "' AND '" + date2.ToString(dateformat) + "' AND ";
            ResponseSlaOk = ResponseSlaOk + " MeetResponseSLA = 1 AND ";
            ResolutionSlaOk = ResolutionSlaOk + "  MeetResolutionSLA = 1 AND ";   

            if (data.WoId != null)
            {
                MultiInfo = MultiInfo + " w.WorkOrderCode = " + data.WoId + " AND ";
                ClosedToday = ClosedToday + " w.WorkOrderCode  = " + data.WoId + " AND ";
                OpenToday = OpenToday + " w.id  = " + data.WoId + " AND ";
                Assets = Assets + " w.id  = " + data.WoId + " AND ";
                ResponseSlaOk = ResponseSlaOk + " WorkOrderCode = " + data.WoId + " AND ";
                ResolutionSlaOk = ResolutionSlaOk + " WorkOrderCode = " + data.WoId + " AND ";
                TotalSla = TotalSla + " WorkOrderCode = " + data.WoId + " AND ";
                GetMonthlyProjectCost = GetMonthlyProjectCost + "WorkOrderCode =" + data.WoId + " AND ";
                GetMonthlyProjectRevenue = GetMonthlyProjectRevenue + "WorkOrderCode =" + data.WoId + " AND ";
                GetMonthlyProjectMargin = GetMonthlyProjectMargin + "WorkOrderCode =" + data.WoId + " AND ";
            }

            if (data.EndDate != null && data.StartDate != null && data.EndDate >= data.StartDate)
            {
                MultiInfo = MultiInfo + " [ClosingWODate] BETWEEN '" + data.StartDate.Value.ToString(dateformat) + "' AND '" + data.EndDate.Value.ToString(dateformat) + "' AND ";
                Assets = Assets + " w.CreationDate BETWEEN '" + data.StartDate.Value.ToString(dateformat) + "' AND '" + data.EndDate.Value.ToString(dateformat) + "' AND ";
                GetMonthlyProjectCost = GetMonthlyProjectCost + " [ClosingWODate] BETWEEN '" + data.StartDate.Value.ToString(dateformat) + "' AND '" + data.EndDate.Value.ToString(dateformat) + "' AND ";
                GetMonthlyProjectRevenue = GetMonthlyProjectRevenue + " [ClosingWODate] BETWEEN '" + data.StartDate.Value.ToString(dateformat) + "' AND '" + data.EndDate.Value.ToString(dateformat) + "' AND ";
                GetMonthlyProjectMargin = GetMonthlyProjectMargin + " [ClosingWODate] BETWEEN '" + data.StartDate.Value.ToString(dateformat) + "' AND '" + data.EndDate.Value.ToString(dateformat) + "' AND ";
                ResponseSlaOk = ResponseSlaOk + " [ClosingWODate] BETWEEN '" + data.StartDate.Value.ToString(dateformat) + "' AND '" + data.EndDate.Value.ToString(dateformat) + "' AND ";
                ResolutionSlaOk = ResolutionSlaOk + " [ClosingWODate] BETWEEN '" + data.StartDate.Value.ToString(dateformat) + "' AND '" + data.EndDate.Value.ToString(dateformat) + "' AND ";
                TotalSla = TotalSla + " [ClosingWODate] BETWEEN '" + data.StartDate.Value.ToString(dateformat) + "' AND '" + data.EndDate.Value.ToString(dateformat) + "' AND ";
            }

            if (data.EndDate == null && data.StartDate == null)
            {
                GetMonthlyProjectCost = GetMonthlyProjectCost + " ClosingWODate BETWEEN '" + month1.ToString(dateformat) + "' AND '" + month2.ToString(dateformat) + "' AND ";
                GetMonthlyProjectRevenue = GetMonthlyProjectRevenue + " ClosingWODate BETWEEN '" + month1.ToString(dateformat) + "' AND '" + month2.ToString(dateformat) + "' AND ";
                GetMonthlyProjectMargin = GetMonthlyProjectMargin + " ClosingWODate BETWEEN '" + month1.ToString(dateformat) + "' AND '" + month2.ToString(dateformat) + "' AND ";
            }

            if (data.ProjectId != 0)
            {
                MultiInfo = MultiInfo + " w.ProjectCode = " + data.ProjectId + " AND ";
                ClosedToday = ClosedToday + " w.ProjectCode = " + data.ProjectId + " AND ";
                OpenToday = OpenToday + " w.ProjectId = " + data.ProjectId + " AND ";
                Assets = Assets + " w.ProjectId = " + data.ProjectId + " AND ";
                GetMonthlyProjectCost = GetMonthlyProjectCost + " [ProjectCode] = " + data.ProjectId + " AND ";
                GetMonthlyProjectRevenue = GetMonthlyProjectRevenue + " [ProjectCode] = " + data.ProjectId + " AND ";
                GetMonthlyProjectMargin = GetMonthlyProjectMargin + " [ProjectCode] = " + data.ProjectId + " AND ";
                ResponseSlaOk = ResponseSlaOk + " ProjectCode = " + data.ProjectId + " AND ";
                ResolutionSlaOk = ResolutionSlaOk + " ProjectCode = " + data.ProjectId + " AND ";
                TotalSla = TotalSla + " ProjectCode = " + data.ProjectId + " AND ";
            }

            if (data.WoCategory != 0)
            {
                MultiInfo = MultiInfo + " w.WorkOrderCategory = " + data.WoCategory + " AND ";
                ClosedToday = ClosedToday + " w.WorkOrderCategory = " + data.WoCategory + " AND ";
                OpenToday = OpenToday + " w.WorkOrderCategoryId = " + data.WoCategory + " AND ";
                Assets = Assets + " w.WorkOrderCategoryId = " + data.WoCategory + " AND ";
                ResponseSlaOk = ResponseSlaOk + " WorkOrderCategory = " + data.WoCategory + " AND ";
                ResolutionSlaOk = ResolutionSlaOk + " WorkOrderCategory = " + data.WoCategory + " AND ";
                TotalSla = TotalSla + " WorkOrderCategory = " + data.WoCategory + " AND ";
                GetMonthlyProjectCost = GetMonthlyProjectCost + " WorkOrderCategory = " + data.WoCategory + " AND ";
                GetMonthlyProjectRevenue = GetMonthlyProjectRevenue + " WorkOrderCategory = " + data.WoCategory + " AND ";
                GetMonthlyProjectMargin = GetMonthlyProjectMargin + " WorkOrderCategory = " + data.WoCategory + " AND ";
            }

            MultiInfo = MultiInfo + "w.WorkOrderCode IS NOT NULL";
            ClosedToday = ClosedToday + "w.WorkOrderCode IS NOT NULL";
            OpenToday = OpenToday + "w.id IS NOT NULL";
            Assets = Assets + "e.id IS NOT NULL "
                        + "GROUP BY e.Name, e.SerialNumber "
                        + "ORDER BY AVISOS DESC  ";
            GetMonthlyProjectCost = GetMonthlyProjectCost + "[WorkOrderCode] IS NOT NULL GROUP BY FORMAT(ClosingWODate, 'yyyy - M'), WorkOrderCategory, DATEPART(MONTH, ClosingWODate), DATEPART(YEAR, ClosingWODate) " +
                 "ORDER BY NumberMonth, Cost DESC";
            GetMonthlyProjectRevenue = GetMonthlyProjectRevenue + "[WorkOrderCode] IS NOT NULL GROUP BY FORMAT(ClosingWODate, 'yyyy - M'), WorkOrderCategory, DATEPART(MONTH, ClosingWODate), DATEPART(YEAR, ClosingWODate) " +
                 "ORDER BY NumberMonth, Revenue DESC";
            GetMonthlyProjectMargin = GetMonthlyProjectMargin + "[WorkOrderCode] IS NOT NULL GROUP BY FORMAT(ClosingWODate, 'yyyy - M'), WorkOrderCategory, DATEPART(MONTH, ClosingWODate), DATEPART(YEAR, ClosingWODate) " +
                 "ORDER BY NumberMonth, Margin DESC";
            ResponseSlaOk = ResponseSlaOk + "[WorkOrderCode] IS NOT NULL ";
            ResolutionSlaOk = ResolutionSlaOk + "[WorkOrderCode] IS NOT NULL ";
            TotalSla = TotalSla +  "[WorkOrderCode] IS NOT NULL ";

            List <SqlParameter> parameterList = new List<SqlParameter>();
            var ds = _analysisRepository.GetSelectSql(MultiInfo, parameterList);
            var ds2 = _analysisRepository.GetSelectSql(ClosedToday, parameterList);
            var ds3 = _analysisRepository.GetSelectSql(OpenToday, parameterList);
            var ds4 = _analysisRepository.GetSelectSql(Assets, parameterList);
            var dsGetMonthlyProjectCost = _analysisRepository.GetSelectSql(GetMonthlyProjectCost, parameterList);
            var dsGetMonthlyProjectRevenue = _analysisRepository.GetSelectSql(GetMonthlyProjectRevenue, parameterList);
            var dsGetMonthlyProjectMargin = _analysisRepository.GetSelectSql(GetMonthlyProjectMargin, parameterList);
            var dsResponseSla = _analysisRepository.GetSelectSql(ResponseSlaOk, parameterList);
            var dsResolutionSla = _analysisRepository.GetSelectSql(ResolutionSlaOk, parameterList);
            var dsTotalSla = _analysisRepository.GetSelectSql(TotalSla, parameterList);


            var averageVisits = ds[0][0].Value is DBNull ? 0.0 : decimal.ToDouble((decimal)ds[0][0].Value);
            result.AverageVisits = Math.Round(averageVisits, 2);
            result.MaxVisits = ds[0][1].Value is DBNull ? 1 : (int)ds[0][1].Value;
            result.IntervalVisits = new Dictionary<double, double> { { 0.4, 0.8 } };

            var averageOnSiteTime = ds[0][2].Value is DBNull ? 0.0 : decimal.ToDouble((decimal)ds[0][2].Value);
            result.AverageOnSiteTime = Math.Round(averageOnSiteTime, 2);
            result.MaxOnSiteTime = ds[0][3].Value is DBNull ? 1 : (int)ds[0][3].Value;
            result.IntervalOnSite = new Dictionary<double, double> { { 0.3, 0.5 } };

            var averageTravelTime = ds[0][4].Value is DBNull ? 0.0 : decimal.ToDouble((decimal)ds[0][4].Value);
            result.AverageTravelTime = Math.Round(averageTravelTime, 2);
            result.MaxTravelTime = ds[0][5].Value is DBNull ? 1 : (int)ds[0][5].Value;
            result.IntervalTravel = new Dictionary<double, double> { { 0.2, 0.5 } };

            var averageKilometers = ds[0][6].Value is DBNull ? 0.0 : decimal.ToDouble((decimal)ds[0][6].Value);
            result.AverageKilometers = Math.Round(averageKilometers, 2);
            result.MaxKilometers = ds[0][7].Value is DBNull ? 1.0 : decimal.ToDouble((decimal)ds[0][7].Value);
            result.IntervalKilometers = new Dictionary<double, double> { { 0.33, 0.66 } };

            var averageWaitTime = ds[0][8].Value is DBNull ? 0.0 : decimal.ToDouble((decimal)ds[0][8].Value);
            result.AverageWaitTime = Math.Round(averageWaitTime, 2);
            result.MaxWaitTime = ds[0][9].Value is DBNull ? 1 : (int)ds[0][9].Value;
            result.IntervalWaitTime = new Dictionary<double, double> { { 0.2, 0.5 } };

            result.TotalOts = ds[0][10].Value is DBNull ? 0 : (int)ds[0][10].Value;

            result.TotalOpenedToday = ds2[0][0].Value is DBNull ? 0 : (int)ds2[0][0].Value;

            result.TotalClosedToday = ds3[0][0].Value is DBNull ? 0 : (int)ds3[0][0].Value;

            result.Assets = new List<Dictionary<string, int>>();

            if (ds4.Count < 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    Dictionary<string, int> keyvalue = new Dictionary<string, int>();
                    keyvalue.Add("Null", 0);
                    result.Assets.Add(keyvalue);
                }
            }
            else
            {
                for (int i = 0; i < ds4.Count; i++)
                {
                    Dictionary<string, int> keyvalue = new Dictionary<string, int>();
                    var newKey = ds4[i].FirstOrDefault();
                    var newValue = ds4[i].LastOrDefault();
                    keyvalue.Add(newKey.Value is null ? "-" : newKey.Value.ToString(), newValue.Value is null ? 0 : (int)newValue.Value);
                    result.Assets.Add(keyvalue);
                }
            }
            
            result.GetMonthlyProjectCostList = SaveEconomicData(dsGetMonthlyProjectCost);
            result.GetMonthlyProjectRevenueList = SaveEconomicData(dsGetMonthlyProjectRevenue);
            result.GetMonthlyProjectMarginList = SaveEconomicData(dsGetMonthlyProjectMargin);

            result.ResponseSla = dsResponseSla[0][0].Value is DBNull ? 0 : (int)dsResponseSla[0][0].Value;
            result.ResolutionSla = dsResolutionSla[0][0].Value is DBNull ? 0 : (int)dsResolutionSla[0][0].Value;
            result.TotalSla = dsTotalSla[0][0].Value is DBNull ? 0 : (int)dsTotalSla[0][0].Value;

            return result;
        }

        public List<ServiceGaugesEconomicDto> SaveEconomicData(List<List<DataBaseResultDto>> data) { 
            List<ServiceGaugesEconomicDto> ListInfo = new List<ServiceGaugesEconomicDto>();
            if (data.Count == 0)
            {
                ServiceGaugesEconomicDto info = new ServiceGaugesEconomicDto
                {
                    Data = 0.0,
                    Name = "Null",
                };
                ListInfo.Add(info);
            }
            else
            {
                for (int i = 0; i < data.Count; i++)
                {
                    ServiceGaugesEconomicDto info = new ServiceGaugesEconomicDto
                    {
                        Data = data[i][1].Value == null ? 0.0 : decimal.ToDouble((decimal)data[i][1].Value),
                        Name = (string)data[i][2].Value,
                    };
                    ListInfo.Add(info);
                }
            }
            return ListInfo;
        }

        public ServiceGaugesProjectReportDto GetProjectReportByMonth(ServiceGaugesFilterDto data)
        {
             string dateformat = "yyy-MM-dd H:mm:ss";

            var sqlSW = " SELECT SUM(W.TotalWOProductionCost) AS ProductionCost, SUM(W.TotalWOProductionCost) AS CostDirectWorkForce , SUM(W.TotalWorkedTime) AS HoursDirectWorkForce, "
                     + " SUM(W.TotalWOMaterialsCost) AS CostMaterials, SUM(W.TotalWOSubcontractorCost)AS CostOutSource, SUM(W.TotalWOTravelTimeCost) AS ExpensesTravel, SUM(W.TotalWOExpensesCost) AS ExpensesOther, SUM(S.Kilometers * S.KmCost)  AS ExpensesKm, "
                     + " SUM(S.WaitTime * S.StandardPersonCost) AS ExpensesWait "
                     + " FROM WorkOrderAnalysis W "
                     + " LEFT Join ServicesAnalysis S on S.WorkOrderCode = W.WorkOrderCode where";

            var sqlRevenueWorkForce = "SELECT SUM(DNA.TotalDeliveryNoteSalePrice) As RevenueWorkForce "
                    + " from DNAndMaterialsAnalysis DNA "
                    + " Left join Items I ON I.Id = DNA.ItemCode "
                    + " left Join WorkOrderAnalysis W ON W.WorkOrderCode = DNA.WorkOrder "
                    + " where I.Type = 1 And ";

            var sqlRevenueMaterials = " SELECT SUM(DNA.TotalDeliveryNoteSalePrice) AS RevenueMaterials"
                    + " from DNAndMaterialsAnalysis DNA"
                    + " Left join Items I ON I.Id = DNA.ItemCode "
                    + " left Join WorkOrderAnalysis W ON W.WorkOrderCode = DNA.WorkOrder "
                    + " where I.Type = 0 and ";

            var GetMonthlyProjectMargin = "SELECT SUM(ISNULL(GrossMargin, 0)) Margin "
                    + " FROM WorkOrderAnalysis where ";

            if (data.EndDate != null && data.StartDate != null && data.EndDate > data.StartDate)
            {
                sqlSW = sqlSW + " W.ClosingWODate BETWEEN '" + data.StartDate.Value.ToString(dateformat) + "' AND '" + data.EndDate.Value.ToString(dateformat) + "' AND ";
                sqlRevenueWorkForce = sqlRevenueWorkForce + " W.ClosingWODate BETWEEN '" + data.StartDate.Value.ToString(dateformat) + "' AND '" + data.EndDate.Value.ToString(dateformat) + "' AND ";
                sqlRevenueMaterials = sqlRevenueMaterials + " W.ClosingWODate BETWEEN '" + data.StartDate.Value.ToString(dateformat) + "' AND '" + data.EndDate.Value.ToString(dateformat) + "' AND ";
                GetMonthlyProjectMargin = GetMonthlyProjectMargin + " [ClosingWODate] BETWEEN '" + data.StartDate.Value.ToString(dateformat) + "' AND '" + data.EndDate.Value.ToString(dateformat) + "' AND ";
            }

            if (data.ProjectId != 0)
            {
                sqlSW = sqlSW + " W.ProjectCode = " + data.ProjectId + " AND ";
                sqlRevenueWorkForce = sqlRevenueWorkForce + " W.ProjectCode = " + data.ProjectId + " AND ";
                sqlRevenueMaterials = sqlRevenueMaterials + " W.ProjectCode = " + data.ProjectId + " AND ";
                GetMonthlyProjectMargin = GetMonthlyProjectMargin + " [ProjectCode] = " + data.ProjectId + " AND ";
            }

            if (data.WoCategory != 0)
            {
                sqlSW = sqlSW + " W.WorkOrderCategory = " + data.WoCategory + " AND ";
                sqlRevenueWorkForce = sqlRevenueWorkForce + " W.WorkOrderCategory = " + data.WoCategory + " AND ";
                sqlRevenueMaterials = sqlRevenueMaterials + " W.WorkOrderCategory = " + data.WoCategory + " AND ";
                GetMonthlyProjectMargin = GetMonthlyProjectMargin + " WorkOrderCategory = " + data.WoCategory + " AND ";
            }

            sqlSW = sqlSW + " W.WorkOrderCode is not null ";
            sqlRevenueWorkForce = sqlRevenueWorkForce + " W.WorkOrderCode is not null ";
            sqlRevenueMaterials = sqlRevenueMaterials + " W.WorkOrderCode is not null ";
            GetMonthlyProjectMargin = GetMonthlyProjectMargin + " [WorkOrderCode] IS NOT NULL ";

            List<SqlParameter> parameterList = new List<SqlParameter>();
            var dsSW = _analysisRepository.GetSelectSql(sqlSW, parameterList);
            var dsRevenueWorkForceW = _analysisRepository.GetSelectSql(sqlRevenueWorkForce, parameterList);
            var dsRevenueMaterials = _analysisRepository.GetSelectSql(sqlRevenueMaterials, parameterList);
            var dsGetMonthlyProjectMargin = _analysisRepository.GetSelectSql(GetMonthlyProjectMargin, parameterList);

            ServiceGaugesProjectReportDto report = new ServiceGaugesProjectReportDto();

            report.RevenueContract = 0;
            report.RevenueWorkForce = dsRevenueWorkForceW[0][0].Value is DBNull ? 0 : decimal.ToDouble((decimal)dsRevenueWorkForceW[0][0].Value);
            report.RevenueMaterials = dsRevenueMaterials[0][0].Value is DBNull ? 0 : decimal.ToDouble((decimal)dsRevenueMaterials[0][0].Value);
            report.CostDirectWorkForce = dsSW[0][1].Value is DBNull ? 0 : decimal.ToDouble((decimal)dsSW[0][1].Value);
            report.HoursDirectWorkForce = dsSW[0][2].Value is DBNull ? 0 : (int)dsSW[0][2].Value;
            report.CostMaterials = dsSW[0][3].Value is DBNull ? 0 : decimal.ToDouble((decimal)dsSW[0][3].Value);
            report.CostOutSource = dsSW[0][4].Value is DBNull ? 0 : decimal.ToDouble((decimal)dsSW[0][4].Value);
            report.ExpensesTravel = dsSW[0][5].Value is DBNull ? 0 : decimal.ToDouble((decimal)dsSW[0][5].Value);
            report.ExpensesOther = dsSW[0][6].Value is DBNull ? 0 : decimal.ToDouble((decimal)dsSW[0][6].Value);
            report.ExpensesKm = dsSW[0][7].Value is DBNull ? 0 : decimal.ToDouble((decimal)dsSW[0][7].Value);
            report.ExpensesWait = dsSW[0][8].Value is DBNull ? 0 : decimal.ToDouble((decimal)dsSW[0][8].Value);
            report.Margin = dsGetMonthlyProjectMargin[0][0].Value is DBNull ? 0 : decimal.ToDouble((decimal)dsGetMonthlyProjectMargin[0][0].Value);

            return report;
        }
    }
}