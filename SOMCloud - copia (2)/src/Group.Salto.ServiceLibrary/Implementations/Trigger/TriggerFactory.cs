using Group.Salto.ServiceLibrary.Common.Contracts.ExternalWorkOrderStatus;
using Group.Salto.ServiceLibrary.Common.Contracts.People;
using Group.Salto.ServiceLibrary.Common.Contracts.PredefinedServices;
using Group.Salto.ServiceLibrary.Common.Contracts.Queue;
using Group.Salto.ServiceLibrary.Common.Contracts.Trigger;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderStatus;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.Trigger
{
    public class TriggerFactory : ITriggerFactory
    {
        private IDictionary<string, Func<ITriggerResult>> _servicesQuery;
        private readonly IServiceProvider _services;

        public TriggerFactory(IServiceProvider services)
        {
            _services = services ?? throw new ArgumentNullException($"{nameof(services)} is null ");
            _servicesQuery = new Dictionary<string, Func<ITriggerResult>>();
            _servicesQuery.Add(nameof(TaskTypeEnum.EstatOTExtern), () => _services.GetService<IExternalWoTriggerQueryResult>());
            _servicesQuery.Add(nameof(TaskTypeEnum.Cua), () => _services.GetService<IQueueTriggerQueryResult>());
            _servicesQuery.Add(nameof(TaskTypeEnum.EstatOT), () => _services.GetService<IWOStatusTriggerQueryResult>());
            _servicesQuery.Add(nameof(TaskTypeEnum.Tecnic), () => _services.GetService<IPeopleTechnicianTriggerQueryResult>());
            _servicesQuery.Add(nameof(TaskTypeEnum.IdServeiPredefinit), () => _services.GetService<IPredefinedServiceTriggerQueryResult>());
        }

        public ITriggerResult GetQuery(string triggerType)
        {
            return _servicesQuery[triggerType]() ?? throw new NotImplementedException();
        }
    }
}