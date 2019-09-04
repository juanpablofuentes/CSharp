using System;
using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Contracts.Postcondition;
using Group.Salto.ServiceLibrary.Common.Contracts.PostconditionExecute;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums;

using Microsoft.Extensions.DependencyInjection;

namespace Group.Salto.ServiceLibrary.Implementations.Postconditions
{
    public class PostconditionFactory : IPostconditionFactory
    {
        private readonly IDictionary<PostconditionActionTypeEnum, Func<IPostconditionExecution>> _servicePostcondition;

        public PostconditionFactory(IServiceProvider services)
        {
            _servicePostcondition = new Dictionary<PostconditionActionTypeEnum, Func<IPostconditionExecution>>
            {
                { PostconditionActionTypeEnum.EstatOTExtern, services.GetService<IExternalWoStatusPostconditionExecution> },
                { PostconditionActionTypeEnum.ActuationEndDate, services.GetService<IActuationEndDatePostconditionExecution> },
                { PostconditionActionTypeEnum.DataTancamentClient, services.GetService<IClientClosingDatePostconditionExecution> },
                { PostconditionActionTypeEnum.Cua, services.GetService<IQueuePostconditionExecution> },
                { PostconditionActionTypeEnum.Manipulador, services.GetService<IManipulatorPostconditionExecution> },
                { PostconditionActionTypeEnum.DataRecollida, services.GetService<IPickupDatePostconditionExecution> },
                { PostconditionActionTypeEnum.DataResolucio, services.GetService<IResolutionDatePostconditionExecution> },
                { PostconditionActionTypeEnum.Tecnic, services.GetService<ITechnicianPostconditionExecution> },
                { PostconditionActionTypeEnum.ParentWOExternalStatus, services.GetService<IParentWoExternalStatusPostconditionExecution> },
                { PostconditionActionTypeEnum.DataTancamentSalto, services.GetService<IClosingDatePostconditionExecution> },
                { PostconditionActionTypeEnum.ObservacionsOT, services.GetService<IObservationsPostconditionExecution> },
                { PostconditionActionTypeEnum.DataActuacio, services.GetService<IActionDatePostconditionExecution> },
                { PostconditionActionTypeEnum.EstatOT, services.GetService<IWoStatusPostconditionExecution> },
                { PostconditionActionTypeEnum.ParentWOInternalStatus, services.GetService<IParentWoStatusPostconditionExecute> },
                { PostconditionActionTypeEnum.ParentWOQueue, services.GetService<IParentWoQueuePostconditionExecute> },
                { PostconditionActionTypeEnum.DataAssignacio, services.GetService<IAssignmentDatePostconditionExecution> },
                { PostconditionActionTypeEnum.TipusOTN1, services.GetService<IOtnTypePostconditionExecution> },
                { PostconditionActionTypeEnum.TipusOTN2, services.GetService<IOtnTypePostconditionExecution> },
                { PostconditionActionTypeEnum.TipusOTN3, services.GetService<IOtnTypePostconditionExecution> },
                { PostconditionActionTypeEnum.TipusOTN4, services.GetService<IOtnTypePostconditionExecution> },
                { PostconditionActionTypeEnum.TipusOTN5, services.GetService<IOtnTypePostconditionExecution> },
            };
        }

        public IPostconditionExecution GetPostconditionExecution(PostconditionActionTypeEnum postconditionType)
        {
            IPostconditionExecution postConditionExecution = null;
            if (_servicePostcondition.ContainsKey(postconditionType))
            {
                postConditionExecution = _servicePostcondition[postconditionType]();
            }
            return postConditionExecution;
        }
    }
}
