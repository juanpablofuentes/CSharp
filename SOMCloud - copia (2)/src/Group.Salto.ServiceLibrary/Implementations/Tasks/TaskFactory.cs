using System;
using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Contracts.TaskExecution;
using Group.Salto.ServiceLibrary.Common.Contracts.Tasks;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace Group.Salto.ServiceLibrary.Implementations.Tasks
{
    public class TaskFactory : ITaskFactory
    {
        private readonly IDictionary<TaskActionTypeEnum, Func<ITaskExecution>> _serviceTasks;

        public TaskFactory(IServiceProvider services)
        {
            _serviceTasks = new Dictionary<TaskActionTypeEnum, Func<ITaskExecution>>
            {
                { TaskActionTypeEnum.DataTancamentClient, services.GetService<IWoClosingClientDateTaskExecution> },
                { TaskActionTypeEnum.ReopenOT, services.GetService<IWoReopenTaskExecution> },
                { TaskActionTypeEnum.TechnicianAndActuationDate, services.GetService<IWoTechnicianAndActDateTaskExecution> },
                { TaskActionTypeEnum.DataAssignacio, services.GetService<IWoAssignmentDateTaskExecution> },
                { TaskActionTypeEnum.Creacio, services.GetService<IWoCreationTaskExecution> },
                { TaskActionTypeEnum.AccountingClosingDate, services.GetService<IWoAccountingClosingDateExecution> },
                { TaskActionTypeEnum.DataTancamentOTClient, services.GetService<IWoClosingDateTaskExecution> },
                { TaskActionTypeEnum.Tecnic, services.GetService<IWoTechnicianTaskExecution> },
                { TaskActionTypeEnum.IdServeiPredefinit, services.GetService<IWoServiceExecution> },
                { TaskActionTypeEnum.RestartSLAWatch, services.GetService<IWoRestartSlaTaskExecution> },
                { TaskActionTypeEnum.StopSLAWatch, services.GetService<IWoStopSlaTaskExecution> },
                { TaskActionTypeEnum.DataActuacio, services.GetService<IWoActionDateTaskExecution> },
                { TaskActionTypeEnum.DataRecollida, services.GetService<IWoPickupDateTaskExecution> },
                { TaskActionTypeEnum.DataTancamentSalto, services.GetService<IWoInternalClosingDateTaskExecute> },
                { TaskActionTypeEnum.AddAuditory, services.GetService<IWoAuditoryExecution> },
                { TaskActionTypeEnum.CreateDerivedServices, services.GetService<IWoDerivedServicesTaskExecution> },
                { TaskActionTypeEnum.CreateDerivedWorkOrder, services.GetService<IWoDerivedWorkOrderTaskExecution> },
                { TaskActionTypeEnum.Analysis, services.GetService<IAnalysisTaskExecution> },
                { TaskActionTypeEnum.ApplyBillableRules, services.GetService<IWoBillableRulesTaskExecution> },
                { TaskActionTypeEnum.NotifySubscribers, services.GetService<IWoSubscribersNotificationsTaskExecution> }
            };
        }

        public ITaskExecution GetTaskExecution(TaskActionTypeEnum taskType)
        {
            ITaskExecution serviceExecution = null;
            if (_serviceTasks.ContainsKey(taskType))
            {
                serviceExecution = _serviceTasks[taskType]();
            }
            return serviceExecution;
        }
    }
}
