using Group.Salto.ServiceLibrary.Common.Dtos.Grid;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderColumns;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;
using Group.Salto.SOM.Web.Models.Grid;
using Group.Salto.SOM.Web.Models.WorkOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group.Salto.SOM.Web.Helpers.TagHelpers;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class RootGridExtensions
    {
        public static RootGrid ToRootGrid(this WorkOrderGridRequestViewModel model, WorkOrderResultDto alldata)
        {
            RootGrid rootObject = new RootGrid();
            rootObject.Pos = model.PosStart;
            rootObject.Total_count = alldata.TotalRegs;
            if (model.Dhx_no_header == 0)
            {
                foreach (WorkOrderColumnsDto col in alldata.Columns)
                {
                    rootObject.Head.Add(new Head() { Width = col.With.ToString(), Align = col.Align, Type = col.Type, Sort = col.Sort, Value = col.TranslatedName ?? string.Empty });
                }
            }

            foreach (GridDataDto row in alldata.Data)
            {
                List<string> data = new List<string>();
                foreach (string d in row.Data)
                {
                    data.Add(d);
                }
                rootObject.Rows.Add(new Row() { Id = row.Id, Data = data });
            }

            return rootObject;
        }
    }
}