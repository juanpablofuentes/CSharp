using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Actions;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkForm;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkForm;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group.Salto.ServiceLibrary.Implementations.WorkForm
{
    public class WorkFormService : BaseService, IWorkFormService
    {
        private readonly IWorkFormRepository _workFormRepository;

        public WorkFormService(ILoggingService logginingService,
        IWorkFormRepository workFormRepository,
        IActionService actionService) : base(logginingService)
        {
            _workFormRepository = workFormRepository ?? throw new ArgumentNullException(nameof(IWorkFormRepository));
        }

        public ResultDto<IList<WorkFormDto>> GetAllWorkForm()
        {
            var data = _workFormRepository.GetAll().ToList();
            return ProcessResult(data.ToListDto());
        }
    }
}