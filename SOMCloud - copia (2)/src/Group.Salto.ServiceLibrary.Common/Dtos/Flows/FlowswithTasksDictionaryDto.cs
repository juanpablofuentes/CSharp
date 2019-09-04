using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System.Collections.Generic;
namespace Group.Salto.ServiceLibrary.Common.Dtos.Flows
{
    public class FlowsWithTasksDictionaryDto : FlowsDto
    {
        private IList<BaseNameIdDto<int>> flowTasksDictionary { get; set; }
        public IList<BaseNameIdDto<int>> FlowTasksDictionary
        {
            get
            {
                return flowTasksDictionary;
            }
            set
            {
                flowTasksDictionary = value ?? new List<BaseNameIdDto<int>>();
            }
        }
    }
}