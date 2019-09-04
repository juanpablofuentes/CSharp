using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Log;
using System;
using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderDefaultColumns;

namespace Group.Salto.ServiceLibrary.Implementations.WorkOrderDefaultColumns
{
    public class WorkOrderDefaultColumnsService : BaseService, IWorkOrderDefaultColumnsService
    {
        private readonly IWorkOrderDefaultColumnsRepository _workOrderDefaultColumnsRepository;

        public WorkOrderDefaultColumnsService(ILoggingService logginingService,
                                              IWorkOrderDefaultColumnsRepository workOrderDefaultColumnsRepository)
           : base(logginingService)
        {
            _workOrderDefaultColumnsRepository = workOrderDefaultColumnsRepository ?? throw new ArgumentNullException($"{nameof(IWorkOrderDefaultColumnsRepository)} is null ");
        }

        public IEnumerable<Entities.WorkOrderDefaultColumns> GetAll()
        {
            LogginingService.LogInfo($"Get All Default WorkOrderColumns");
            return _workOrderDefaultColumnsRepository.GetAll();
        }
    }
}