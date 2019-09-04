using Group.Salto.ServiceLibrary.Common.Contracts.ExternalWorkOrderStatus;
using Group.Salto.ServiceLibrary.Common.Contracts.People;
using Group.Salto.ServiceLibrary.Common.Contracts.Postcondition;
using Group.Salto.ServiceLibrary.Common.Contracts.Queue;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderStatus;
using Group.Salto.ServiceLibrary.Common.Contracts.WOType;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.Postconditions
{
    public class PostconditionServiceFactory : IPostconditionServiceFactory
    {
        private IDictionary<string, Func<IPostconditionResult>> _servicesQuery;
        private readonly IServiceProvider _services;

        public PostconditionServiceFactory(IServiceProvider services)
        {
            _services = services ?? throw new ArgumentNullException($"{nameof(services)} is null ");
            _servicesQuery = new Dictionary<string, Func<IPostconditionResult>>();
            _servicesQuery.Add(nameof(PostconditionActionTypeEnum.Cua), () => _services.GetService<IQueuePostconditionQueryResult>());
            _servicesQuery.Add(nameof(PostconditionActionTypeEnum.EstatOT), () => _services.GetService<IWOStatusPostconditionQueryResult>());
            _servicesQuery.Add(nameof(PostconditionActionTypeEnum.EstatOTExtern), () => _services.GetService<IExternalWoPostconditionQueryResult>());
            _servicesQuery.Add(nameof(PostconditionActionTypeEnum.ParentWOExternalStatus), () => _services.GetService<IExternalWoPostconditionQueryResult>());
            _servicesQuery.Add(nameof(PostconditionActionTypeEnum.ParentWOInternalStatus), () => _services.GetService<IExternalWoPostconditionQueryResult>());
            _servicesQuery.Add(nameof(PostconditionActionTypeEnum.ParentWOQueue), () => _services.GetService<IQueuePostconditionQueryResult>());
            _servicesQuery.Add(nameof(PostconditionActionTypeEnum.Tecnic), () => _services.GetService<IPeopleTechnicianPostconditionQueryResult>());
            _servicesQuery.Add(nameof(PostconditionActionTypeEnum.TipusOTN1), () => _services.GetService<IWOTypePostconditionQueryResult>());
            _servicesQuery.Add(nameof(PostconditionActionTypeEnum.TipusOTN2), () => _services.GetService<IWOTypePostconditionQueryResult>());
            _servicesQuery.Add(nameof(PostconditionActionTypeEnum.TipusOTN3), () => _services.GetService<IWOTypePostconditionQueryResult>());
            _servicesQuery.Add(nameof(PostconditionActionTypeEnum.TipusOTN4), () => _services.GetService<IWOTypePostconditionQueryResult>());
            _servicesQuery.Add(nameof(PostconditionActionTypeEnum.TipusOTN5), () => _services.GetService<IWOTypePostconditionQueryResult>());
        }

        public IPostconditionResult GetQuery(string postconditionType)
        {
            return _servicesQuery[postconditionType]() ?? throw new NotImplementedException();
        }
    }
}