using System;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrder;
using Group.Salto.ServiceLibrary.Common.Dtos.Grid;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Group.Salto.Common.Helpers;

namespace Group.Salto.ServiceLibrary.Implementations.WorkOrder
{
    public class WorkOrderQueryWhere : IWorkOrderQueryWhere
    {
        private short position = 0;
        public string CreateWhere(GridDto gridConfig, List<SqlParameter> parameterList)
        {
            StringBuilder where = null;
            string subContracts = string.Empty;
            position = 0;

            SqlParameter userParameter = new SqlParameter("@PeopleId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = gridConfig.PeopleId
            };
            parameterList.Add(userParameter);

            where = new StringBuilder("WHERE");
            if (!string.IsNullOrEmpty(gridConfig.WorkOrderFilters.WorkOrderSearch?.SearchString))
            {
                switch (gridConfig.WorkOrderFilters.WorkOrderSearch?.SearchType)
                {
                    case Salto.Common.Enums.WorkOrderSearchEnum.WorkOrder:
                        int wo;
                        if (!int.TryParse(gridConfig.WorkOrderFilters.WorkOrderSearch.SearchString, out wo))
                        {
                            wo = int.MinValue;
                        }
                        where.Append(" And ([wo] [identifier])");

                        if (wo != int.MinValue)
                        {
                            parameterList.Add(new SqlParameter("@SearchWorkOrderId", SqlDbType.Int)
                            {
                                Direction = ParameterDirection.Input,
                                Value = gridConfig.WorkOrderFilters.WorkOrderSearch.SearchString
                            });
                            where.Replace("[wo]", "wo.Id = @SearchWorkOrderId Or ");
                        }
                        else where.Replace("[wo]", string.Empty);

                        parameterList.Add(new SqlParameter("@SearchIdentifier", SqlDbType.VarChar, 50)
                        {
                            Direction = ParameterDirection.Input,
                            Value = gridConfig.WorkOrderFilters.WorkOrderSearch.SearchString
                        });
                        where.Replace("[identifier]", "wo.InternalIdentifier = @SearchIdentifier Or wo.ExternalIdentifier = @SearchIdentifier");

                        break;

                    case Salto.Common.Enums.WorkOrderSearchEnum.Active:
                        where.Append(" And (assets.SerialNumber = @SearchAssets Or assets.AssetNumber = @SearchAssets Or assets.StockNumber = @SearchAssets)");
                        parameterList.Add(new SqlParameter("@SearchAssets", SqlDbType.VarChar, 50)
                        {
                            Direction = ParameterDirection.Input,
                            Value = gridConfig.WorkOrderFilters.WorkOrderSearch.SearchString
                        });

                        break;

                    case Salto.Common.Enums.WorkOrderSearchEnum.Location:
                        where.Append(" And (loc.Code = @SearchLoc Or loc.Name = @SearchLocName Or loc.Phone1 = @SearchLoc Or loc.Phone2 = @SearchLoc Or loc.Phone3 = @SearchLoc)");
                        parameterList.Add(new SqlParameter("SearchLoc", SqlDbType.VarChar, 50)
                        {
                            Direction = ParameterDirection.Input,
                            Value = gridConfig.WorkOrderFilters.WorkOrderSearch.SearchString
                        });

                        parameterList.Add(new SqlParameter("@SearchLocName", SqlDbType.VarChar, 100)
                        {
                            Direction = ParameterDirection.Input,
                            Value = gridConfig.WorkOrderFilters.WorkOrderSearch.SearchString
                        });

                        break;
                }
            }

            if (!string.IsNullOrEmpty(gridConfig.WorkOrderFilters.WorkOrderId))
            {
                where.Append(" And wo.Id = @WorkOrderId");
                parameterList.Add(new SqlParameter("@WorkOrderId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = gridConfig.WorkOrderFilters.WorkOrderId
                });
            }

            if (!string.IsNullOrEmpty(gridConfig.WorkOrderFilters.InternalIdentifier))
            {
                where.Append(" And wo.InternalIdentifier = @InternalIdentifier");
                parameterList.Add(new SqlParameter("@InternalIdentifier", SqlDbType.VarChar, 50)
                {
                    Direction = ParameterDirection.Input,
                    Value = gridConfig.WorkOrderFilters.InternalIdentifier
                });
            }

            if (!string.IsNullOrEmpty(gridConfig.WorkOrderFilters.SerialNumber))
            {
                where.Append(" And assets.SerialNumber = @SerialNumber");
                parameterList.Add(new SqlParameter("@SerialNumber", SqlDbType.VarChar, 50)
                {
                    Direction = ParameterDirection.Input,
                    Value = gridConfig.WorkOrderFilters.SerialNumber
                });
            }

            if (!string.IsNullOrEmpty(gridConfig.WorkOrderFilters.LocationCode))
            {
                where.Append(" And loc.Code = @LocationCode");
                parameterList.Add(new SqlParameter("@LocationCode", SqlDbType.VarChar, 50)
                {
                    Direction = ParameterDirection.Input,
                    Value = gridConfig.WorkOrderFilters.LocationCode
                });
            }

            where.Append(InFilterAnd(parameterList, new KeyValuePair<string, string>("wo.WorkOrderStatusId", gridConfig.WorkOrderFilters.WorkOrderStatusIds)));
            where.Append(InFilterAnd(parameterList, new KeyValuePair<string, string>("wo.QueueId", gridConfig.WorkOrderFilters.WorkOrderQueueIds)));
            where.Append(InFilterAnd(parameterList, new KeyValuePair<string, string>("wo.ProjectId", gridConfig.WorkOrderFilters.ProjectIds)));
            where.Append(InFilterAnd(parameterList, new KeyValuePair<string, string>("wo.WorkOrderCategoryId", gridConfig.WorkOrderFilters.WorkOrderCategoryIds)));
            where.Append(InFilterAnd(parameterList, new KeyValuePair<string, string>("loc.StateId", gridConfig.WorkOrderFilters.StateIds)));
            where.Append(InFilterAnd(parameterList, new KeyValuePair<string, string>("wo.PeopleIntroducedById", gridConfig.WorkOrderFilters.InsertedBy)));
            where.Append(InFilterAnd(parameterList, new KeyValuePair<string, string>("wo.PeopleManipulatorId", gridConfig.WorkOrderFilters.Manipulator)));
            where.Append(InFilterAnd(parameterList, new KeyValuePair<string, string>("wo.WorkOrderTypesId", gridConfig.WorkOrderFilters.WorkOrderType)));
            where.Append(InFilterAnd(parameterList, new KeyValuePair<string, string>("wo.ExternalWorOrderStatusId", gridConfig.WorkOrderFilters.WorkOrderType)));
            where.Append(InFilterAnd(parameterList, new KeyValuePair<string, string>("client.id", gridConfig.WorkOrderFilters.SaltoClient)));
            where.Append(InFilterAnd(parameterList, new KeyValuePair<string, string>("finalClient.Id", gridConfig.WorkOrderFilters.EndClient)));

            if (gridConfig.WorkOrderFilters.CreationDate.HasValue)
            {
                where.Append(DateTodayFilter(parameterList, new KeyValuePair<string, DateTime>("wo.CreationDate between", gridConfig.WorkOrderFilters.CreationDate.Value)));
            }

            if (gridConfig.WorkOrderFilters.ActionDateDate.HasValue)
            {
                where.Append(DateTodayFilter(parameterList, new KeyValuePair<string, DateTime>("wo.ActionDate between", gridConfig.WorkOrderFilters.ActionDateDate.Value)));
            }

            if (gridConfig.WorkOrderFilters.ResolutionDateSla.HasValue)
            {
                where.Append(DateTodayFilter(parameterList, new KeyValuePair<string, DateTime>("wo.ResolutionDateSla between", gridConfig.WorkOrderFilters.ResolutionDateSla.Value)));
            }

            //CreationDate
            if (gridConfig.WorkOrderFilters.CreationStartDate.HasValue && gridConfig.WorkOrderFilters.CreationEndDate.HasValue)
            {
                DateTime startDate = gridConfig.WorkOrderFilters.CreationStartDate.Value;
                DateTime endDate = gridConfig.WorkOrderFilters.CreationEndDate.Value;
                where.Append($" And wo.CreationDate between @Param{++position} and @Param{position + 1}");

                StartDateParameter(parameterList, startDate);
                EndDatePamrameter(parameterList, endDate);
            }
            else if (gridConfig.WorkOrderFilters.CreationStartDate.HasValue && !gridConfig.WorkOrderFilters.CreationEndDate.HasValue)
            {
                DateTime startDate = gridConfig.WorkOrderFilters.CreationStartDate.Value;
                where.Append($" And wo.CreationDate >= @Param{++position} ");

                StartDateParameter(parameterList, startDate);
            }
            else if (!gridConfig.WorkOrderFilters.CreationStartDate.HasValue && gridConfig.WorkOrderFilters.CreationEndDate.HasValue)
            {
                DateTime endDate = gridConfig.WorkOrderFilters.CreationEndDate.Value;
                where.Append($" And wo.CreationDate <= @Param{++position}");
                --position;
                EndDatePamrameter(parameterList, endDate);
            }

            //PickUpTime
            if (gridConfig.WorkOrderFilters.PickUpTimeStartDate.HasValue && gridConfig.WorkOrderFilters.PickUpTimeEndDate.HasValue)
            {
                DateTime startDate = gridConfig.WorkOrderFilters.PickUpTimeStartDate.Value;
                DateTime endDate = gridConfig.WorkOrderFilters.PickUpTimeEndDate.Value;
                where.Append($" And wo.PickUpTime between @Param{++position} and @Param{position + 1}");

                StartDateParameter(parameterList, startDate);
                EndDatePamrameter(parameterList, endDate);
            }
            else if (gridConfig.WorkOrderFilters.PickUpTimeStartDate.HasValue && !gridConfig.WorkOrderFilters.PickUpTimeEndDate.HasValue)
            {
                DateTime startDate = gridConfig.WorkOrderFilters.PickUpTimeStartDate.Value;
                where.Append($" And wo.PickUpTime >= @Param{++position} ");

                StartDateParameter(parameterList, startDate);
            }
            else if (!gridConfig.WorkOrderFilters.PickUpTimeStartDate.HasValue && gridConfig.WorkOrderFilters.PickUpTimeEndDate.HasValue)
            {
                DateTime endDate = gridConfig.WorkOrderFilters.PickUpTimeEndDate.Value;
                where.Append($" And wo.PickUpTime <= @Param{++position}");
                --position;
                EndDatePamrameter(parameterList, endDate);
            }

            //FinalClientClosingTime
            if (gridConfig.WorkOrderFilters.FinalClientClosingTimeStartDate.HasValue && gridConfig.WorkOrderFilters.FinalClientClosingTimeEndDate.HasValue)
            {
                DateTime startDate = gridConfig.WorkOrderFilters.FinalClientClosingTimeStartDate.Value;
                DateTime endDate = gridConfig.WorkOrderFilters.FinalClientClosingTimeEndDate.Value;
                where.Append($" And wo.FinalClientClosingTime between @Param{++position} and @Param{position + 1}");

                StartDateParameter(parameterList, startDate);
                EndDatePamrameter(parameterList, endDate);
            }
            else if (gridConfig.WorkOrderFilters.FinalClientClosingTimeStartDate.HasValue && !gridConfig.WorkOrderFilters.FinalClientClosingTimeEndDate.HasValue)
            {
                DateTime startDate = gridConfig.WorkOrderFilters.FinalClientClosingTimeStartDate.Value;
                where.Append($" And wo.FinalClientClosingTime >= @Param{++position} ");

                StartDateParameter(parameterList, startDate);
            }
            else if (!gridConfig.WorkOrderFilters.FinalClientClosingTimeStartDate.HasValue && gridConfig.WorkOrderFilters.FinalClientClosingTimeEndDate.HasValue)
            {
                DateTime endDate = gridConfig.WorkOrderFilters.FinalClientClosingTimeEndDate.Value;
                where.Append($" And wo.FinalClientClosingTime <= @Param{++position}");
                --position;
                EndDatePamrameter(parameterList, endDate);
            }

            //InternalClosingTime
            if (gridConfig.WorkOrderFilters.InternalClosingTimeStartDate.HasValue && gridConfig.WorkOrderFilters.InternalClosingTimeEndDate.HasValue)
            {
                DateTime startDate = gridConfig.WorkOrderFilters.InternalClosingTimeStartDate.Value;
                DateTime endDate = gridConfig.WorkOrderFilters.InternalClosingTimeEndDate.Value;
                where.Append($" And wo.InternalClosingTime between @Param{++position} and @Param{position + 1}");

                StartDateParameter(parameterList, startDate);
                EndDatePamrameter(parameterList, endDate);
            }
            else if (gridConfig.WorkOrderFilters.InternalClosingTimeStartDate.HasValue && !gridConfig.WorkOrderFilters.InternalClosingTimeEndDate.HasValue)
            {
                DateTime startDate = gridConfig.WorkOrderFilters.InternalClosingTimeStartDate.Value;
                where.Append($" And wo.InternalClosingTime >= @Param{++position} ");

                StartDateParameter(parameterList, startDate);
            }
            else if (!gridConfig.WorkOrderFilters.InternalClosingTimeStartDate.HasValue && gridConfig.WorkOrderFilters.InternalClosingTimeEndDate.HasValue)
            {
                DateTime endDate = gridConfig.WorkOrderFilters.InternalClosingTimeEndDate.Value;
                where.Append($" And wo.InternalClosingTime <= @Param{++position}");
                --position;
                EndDatePamrameter(parameterList, endDate);
            }

            //AssignmentTime
            if (gridConfig.WorkOrderFilters.AssignmentTimeStartDate.HasValue && gridConfig.WorkOrderFilters.AssignmentTimeEndDate.HasValue)
            {
                DateTime startDate = gridConfig.WorkOrderFilters.AssignmentTimeStartDate.Value;
                DateTime endDate = gridConfig.WorkOrderFilters.AssignmentTimeEndDate.Value;
                where.Append($" And wo.AssignmentTime between @Param{++position} and @Param{position + 1}");

                StartDateParameter(parameterList, startDate);
                EndDatePamrameter(parameterList, endDate);
            }
            else if (gridConfig.WorkOrderFilters.AssignmentTimeStartDate.HasValue && !gridConfig.WorkOrderFilters.AssignmentTimeEndDate.HasValue)
            {
                DateTime startDate = gridConfig.WorkOrderFilters.AssignmentTimeStartDate.Value;
                where.Append($" And wo.AssignmentTime >= @Param{++position} ");

                StartDateParameter(parameterList, startDate);
            }

            else if (!gridConfig.WorkOrderFilters.AssignmentTimeStartDate.HasValue && gridConfig.WorkOrderFilters.AssignmentTimeEndDate.HasValue)
            {
                DateTime endDate = gridConfig.WorkOrderFilters.AssignmentTimeEndDate.Value;
                where.Append($" And wo.AssignmentTime <= @Param{++position}");
                --position;
                EndDatePamrameter(parameterList, endDate);
            }

            if (gridConfig.WorkOrderFilters.ActionDateStartDate.HasValue && gridConfig.WorkOrderFilters.ActionDateEndDate.HasValue)
            {
                DateTime startDate = gridConfig.WorkOrderFilters.ActionDateStartDate.Value;
                DateTime endDate = gridConfig.WorkOrderFilters.ActionDateEndDate.Value;
                where.Append($" And wo.ActionDate between @Param{++position} and @Param{position + 1}");

                StartDateParameter(parameterList, startDate);
                EndDatePamrameter(parameterList, endDate);
            }
            else if (gridConfig.WorkOrderFilters.ActionDateStartDate.HasValue && !gridConfig.WorkOrderFilters.ActionDateEndDate.HasValue)
            {
                DateTime startDate = gridConfig.WorkOrderFilters.ActionDateStartDate.Value;
                where.Append($" And wo.ActionDate >= @Param{++position} ");

                StartDateParameter(parameterList, startDate);
            }
            else if (!gridConfig.WorkOrderFilters.ActionDateStartDate.HasValue && gridConfig.WorkOrderFilters.ActionDateEndDate.HasValue)
            {
                DateTime endDate = gridConfig.WorkOrderFilters.ActionDateEndDate.Value;
                where.Append($" And wo.ActionDate <= @Param{++position}");
                --position;
                EndDatePamrameter(parameterList, endDate);
            }

            where.Append($@" And (
    [Technicians]
    EXISTS(SELECT 1 FROM @QueueTable As qt WHERE(wo.QueueId = qt.QueueId))
    AND
    (
        (
            (wo.PeopleResponsibleId = @PeopleId) { GetSubContracts(gridConfig)}
		) 
		OR
        (
            EXISTS(SELECT 1 FROM @ProjectTable As pt WHERE(wo.ProjectId = pt.ProjectId))
            AND EXISTS(SELECT 1 FROM @WOCTable As wt WHERE(wo.WorkOrderCategoryId = wt.WorkOrderCategoryId))
        )
	)
)");
            position++;
            string technicians = TechniciansFilter(parameterList, new KeyValuePair<string, string>("wo.PeopleResponsibleId", gridConfig.WorkOrderFilters.ResponsiblesIds));
            where = where.Replace("[Technicians]", technicians);
            where = where.Replace("WHERE And", "WHERE ");

            return where.ToString();
        }

        private StringBuilder DateTodayFilter(List<SqlParameter> parameterList, KeyValuePair<string, DateTime> param)
        {
            StringBuilder filter = new StringBuilder();
            DateTime creationDate = param.Value;
            filter.Append($" And {param.Key} @Param{position} and @Param{position + 1}");
            StartDateParameter(parameterList, creationDate);
            EndDatePamrameter(parameterList, creationDate);
            position++;
            return filter;
        }

        private StringBuilder InFilterAnd(List<SqlParameter> parameterList, KeyValuePair<string, string> param)
        {
            StringBuilder filter = new StringBuilder();
            if (!string.IsNullOrEmpty(param.Value))
            {
                filter.Append($" And {param.Key} in (");
                string[] values = param.Value.Split(",");
                foreach (string value in values)
                {
                    filter.Append($"@Param{position},");
                    parameterList.Add(new SqlParameter($"@Param{position++}", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = Convert.ToInt32(value)
                    });
                }
                filter.RemoveLastComa();
                filter.Append(")");
            }
            return filter;
        }

        private string TechniciansFilter(List<SqlParameter> parameterList, KeyValuePair<string, string> param)
        {
            StringBuilder filter = new StringBuilder(string.Empty);
            if (!string.IsNullOrEmpty(param.Value))
            {
                filter.Append($" ({param.Key} in (");
                string[] values = param.Value.Split(",");
                foreach (string value in values)
                {
                    filter.Append($"@Param{position},");
                    parameterList.Add(new SqlParameter($"@Param{position++}", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = value
                    });
                }
                filter.RemoveLastComa();
                filter.Append(")) or");
            }
            return filter.ToString();
        }

        private void StartDateParameter(List<SqlParameter> parameterList, DateTime creationDateStart)
        {
            parameterList.Add(new SqlParameter($"@Param{position}", SqlDbType.DateTime)
            {
                Direction = ParameterDirection.Input,
                Value = new DateTime(creationDateStart.Year, creationDateStart.Month, creationDateStart.Day, 0, 0, 0)
            });
        }

        private void EndDatePamrameter(List<SqlParameter> parameterList, DateTime creationDateEnd)
        {
            parameterList.Add(new SqlParameter($"@Param{++position}", SqlDbType.DateTime)
            {
                Direction = ParameterDirection.Input,
                Value = new DateTime(creationDateEnd.Year, creationDateEnd.Month, creationDateEnd.Day, 23, 59, 59)
            });
        }

        private string GetSubContracts(GridDto gridConfig)
        {
            string subContract = string.Empty;
            if (gridConfig.SubContracts != null && gridConfig.SubContracts.Length > 0)
            {
                subContract = $"OR (wo.PeopleResponsibleId IN({gridConfig.SubContracts.ToString(",")} ))";
            }
            return subContract;
        }
    }
}