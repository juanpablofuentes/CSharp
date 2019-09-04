using Group.Salto.Common.Constants.LiteralPrecondition;
using Group.Salto.ServiceLibrary.Common.Contracts.Assets;
using Group.Salto.ServiceLibrary.Common.Contracts.ExternalWorkOrderStatus;
using Group.Salto.ServiceLibrary.Common.Contracts.FinalClients;
using Group.Salto.ServiceLibrary.Common.Contracts.LiteralPreconditions;
using Group.Salto.ServiceLibrary.Common.Contracts.People;
using Group.Salto.ServiceLibrary.Common.Contracts.Project;
using Group.Salto.ServiceLibrary.Common.Contracts.Queue;
using Group.Salto.ServiceLibrary.Common.Contracts.SitesFinalClients;
using Group.Salto.ServiceLibrary.Common.Contracts.States;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderCategories;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderStatus;
using Group.Salto.ServiceLibrary.Common.Contracts.WOType;
using Group.Salto.ServiceLibrary.Common.Contracts.Zones;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.AssetStatuses
{
    public class LiteralQueryFactory : ILiteralQueryFactory
    {
        private IDictionary<string, Func<ILiteralResult>> _servicesQuery;
        private readonly IServiceProvider _services;

        public LiteralQueryFactory(IServiceProvider services)
        {
            _services = services ?? throw new ArgumentNullException($"{nameof(services)} is null ");
            _servicesQuery = new Dictionary<string, Func<ILiteralResult>>();
            _servicesQuery.Add(LiteralPreconditionConstants.Asset, () => _services.GetService<IAssetLiteralQueryResult>());
            _servicesQuery.Add(LiteralPreconditionConstants.FinalClient, () => _services.GetService<IFinalClientLiteralQueryResult>());
            _servicesQuery.Add(LiteralPreconditionConstants.FinalClientLocation, () => _services.GetService<ISitesFinalClientLiteralQueryResult>());
            _servicesQuery.Add(LiteralPreconditionConstants.Project, () => _services.GetService<IProjectLiteralQueryResult>());
            _servicesQuery.Add(LiteralPreconditionConstants.Queue, () => _services.GetService<IQueueLiteralQueryResult>());
            _servicesQuery.Add(LiteralPreconditionConstants.State, () => _services.GetService<IStateLiteralQueryResult>());
            _servicesQuery.Add(LiteralPreconditionConstants.Technical, () => _services.GetService<IPeopleTechnicianLiteralQueryResult>());
            _servicesQuery.Add(LiteralPreconditionConstants.WoCategory, () => _services.GetService<IWorkOrderCategoriesLiteralQueryResult>());
            _servicesQuery.Add(LiteralPreconditionConstants.WoExternalState, () => _services.GetService<IExternalWoLiteralQueryResult>());
            _servicesQuery.Add(LiteralPreconditionConstants.WoState, () => _services.GetService<IWoStatusLiteralQueryResult>());
            _servicesQuery.Add(LiteralPreconditionConstants.WoTypeN1, () => _services.GetService<IWOTypeLiteralQueryResult>());
            _servicesQuery.Add(LiteralPreconditionConstants.WoTypeN2, () => _services.GetService<IWOTypeLiteralQueryResult>());
            _servicesQuery.Add(LiteralPreconditionConstants.WoTypeN3, () => _services.GetService<IWOTypeLiteralQueryResult>());
            _servicesQuery.Add(LiteralPreconditionConstants.WoTypeN4, () => _services.GetService<IWOTypeLiteralQueryResult>());
            _servicesQuery.Add(LiteralPreconditionConstants.WoTypeN5, () => _services.GetService<IWOTypeLiteralQueryResult>());
            _servicesQuery.Add(LiteralPreconditionConstants.Zone, () => _services.GetService<IZoneLiteralQueryResult>());
        }

        public ILiteralResult GetQuery(string literalType)
        {
            return _servicesQuery[literalType]() ?? throw new NotImplementedException();
        }
    }
}