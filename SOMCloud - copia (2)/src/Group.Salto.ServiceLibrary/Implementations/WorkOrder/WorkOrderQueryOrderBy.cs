using Group.Salto.Common.Enums;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrder;
using Group.Salto.ServiceLibrary.Common.Dtos.Grid;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderColumns;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System;

namespace Group.Salto.ServiceLibrary.Implementations.WorkOrder
{
    public class WorkOrderQueryOrderBy : IWorkOrderQueryOrderBy
    {
        private readonly IDictionary<WorkOrderColumnsEnum, string> orderFields = null;

        public WorkOrderQueryOrderBy()
        {
            orderFields = new Dictionary<WorkOrderColumnsEnum, string>()
            {
                { WorkOrderColumnsEnum.Id, "wo.Id" },
                { WorkOrderColumnsEnum.InternalIdentifier, "wo.InternalIdentifier" },
                { WorkOrderColumnsEnum.ActionDate, "wo.ActionDate" },
                { WorkOrderColumnsEnum.ResolutionDateSla, "wo.ResolutionDateSla" },
                { WorkOrderColumnsEnum.Project, "project.Name" },
                { WorkOrderColumnsEnum.WorkOrderCategory, "woc.Name" },
                { WorkOrderColumnsEnum.ResponsiblePersonName, "responsible.Name" },
                { WorkOrderColumnsEnum.Province, "s.[Name]" },
            };
        }

        public string CreateOrderBy(IList<WorkOrderColumnsDto> columns, GridDto gridConfig)
        {
            StringBuilder order = new StringBuilder($"{Environment.NewLine} ORDER BY ");
            string defaultOrder = "wo.id Desc";
            if (gridConfig.Sort.DefaultOrder)
            {
                order.Append(defaultOrder);
            }
            else
            {
                WorkOrderColumnsDto colum = columns.FirstOrDefault(x => x.ColumnOrder == gridConfig.Sort.ColumnOrder);
                if (orderFields.TryGetValue((WorkOrderColumnsEnum)colum.Id, out string action))
                {
                    string direction = gridConfig.Sort.IsAscending ? " Asc" : " Desc";
                    order.Append($"{action} {direction}");
                }
                else
                {
                    order.Append(defaultOrder);
                }
            }
            return order.ToString();
        }
    }
}