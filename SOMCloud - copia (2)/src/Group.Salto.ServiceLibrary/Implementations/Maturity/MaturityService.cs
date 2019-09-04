using System.Collections.Generic;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Maturity;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.ServiceLibrary.Implementations.Maturity
{
    public class MaturityService : BaseService, IMaturityService
    {
        public MaturityService(ILoggingService logginingService) : base(logginingService)
        {
        }

        public IList<BaseNameIdDto<int>> GetBaseMaturities()
        {
            LogginingService.LogInfo("Getting Maturities");
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