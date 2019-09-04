using System.Collections.Generic;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Priority;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.ServiceLibrary.Implementations.Priority
{
    public class PriorityService : BaseService, IPriorityService
    {
        public PriorityService(ILoggingService logginingService) : base(logginingService)
        {
        }


        public IList<BaseNameIdDto<int>> GetBasePriorities()
        {
            LogginingService.LogInfo("Getting Priorities");
            //TODO Review if data hardcoded or new table;
            var list = new List<BaseNameIdDto<int>>();
            for (int i = 1; i <= 10; i++)
            {
                list.Add(new BaseNameIdDto<int>()
                {
                    Id = i,
                    Name = i.ToString("00"),
                });
            }

            return list;
        }
    }
}