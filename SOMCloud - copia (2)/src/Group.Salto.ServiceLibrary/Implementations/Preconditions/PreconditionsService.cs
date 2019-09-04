using Group.Salto.Common;
using Group.Salto.Common.Constants.LiteralPrecondition;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.LiteralPreconditions;
using Group.Salto.ServiceLibrary.Common.Contracts.Precondition;
using Group.Salto.ServiceLibrary.Common.Contracts.States;
using Group.Salto.ServiceLibrary.Common.Contracts.Trigger;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Preconditions;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Preconditions
{
    public class PreconditionsService : BaseService, IPreconditionsService
    {
        private readonly IPreconditionsRepository _preconditionsRepository;
        private readonly IPreconditionTypesService _preconditionTypesService;
        private readonly IStateService _stateService;
        private readonly ILiteralPreconditionsService _literalPreconditionsService;
        private readonly ILiteralQueryFactory _literalQueryFactory;
        private readonly IPreconditionsLiteralValuesRepository _literalValuesRepository;

        public PreconditionsService(ILoggingService logginingService,
                                IPreconditionTypesService preconditionTypesService,
                                IStateService stateService,
                                ILiteralPreconditionsService literalPreconditionsService,
                                ILiteralQueryFactory literalQueryFactory,
                                IPreconditionsLiteralValuesRepository literalValuesRepository,
                                IPreconditionsRepository preconditionsRepository) : base(logginingService)
        {
            _preconditionsRepository = preconditionsRepository ?? throw new ArgumentNullException(nameof(IPreconditionsRepository));
            _preconditionTypesService = preconditionTypesService ?? throw new ArgumentNullException(nameof(IPreconditionTypesService));
            _stateService = stateService ?? throw new ArgumentNullException(nameof(IStateService));
            _literalQueryFactory = literalQueryFactory ?? throw new ArgumentNullException(nameof(ILiteralQueryFactory));
            _literalValuesRepository = literalValuesRepository ?? throw new ArgumentNullException(nameof(IPreconditionsLiteralValuesRepository));
            _literalPreconditionsService = literalPreconditionsService ?? throw new ArgumentNullException(nameof(ILiteralPreconditionsService));
        }

        public ResultDto<IList<PreconditionsDto>> GetAllByTaskId(int id)
        {
            LogginingService.LogInfo($"Get all preconditions by taskid");
            var preconditionTypes = _preconditionTypesService.GetAll();
            var preconditions = _preconditionsRepository.GetAllByTaskId(id).ToList().ToDto(preconditionTypes);
            preconditions.OrderBy(x => x.LiteralsPreconditions.Select(y => y.PreconditionsTypeName));
            foreach (var precondition in preconditions)
            {
                foreach (var literalPre in precondition.LiteralsPreconditions)
                {
                    if (literalPre.PreconditionsTypeName == LiteralPreconditionConstants.State)
                    {
                        var states = _stateService.GetAllKeyValues();
                        foreach (var literalValue in literalPre.PreconditionsLiteralValues)
                        {
                            literalValue.TypeName = states.Where(x => x.Id == literalValue.TypeId).Select(x => x.Name).FirstOrDefault();
                        }
                    }
                }
            }

            return ProcessResult(preconditions);
        }

        public PreconditionsDto GetById(int id)
        {
            LogginingService.LogInfo($"Get Precondition By Id");
            var preconditionTypes = _preconditionTypesService.GetAll();
            var result = _preconditionsRepository.GetById(id).ToDto(preconditionTypes);
            return result;
        }

        public PreconditionsDto CreatePrecondition(int id, int? postconditionCollectionId)
        {
            LogginingService.LogInfo($"Create new Precondition");
            PreconditionsDto precondition = new PreconditionsDto()
            {
                TaskId = id,
                PostconditionCollectionId = postconditionCollectionId,
            };
            var preconditionTypes = _preconditionTypesService.GetAll();
            var result = _preconditionsRepository.CreatePrecondition(precondition.ToEntity()).ToDto(preconditionTypes);
            return result;
        }

        public ResultDto<bool> Delete(int id)
        {
            LogginingService.LogInfo($"Delete Precondition by id {id}");
            ResultDto<bool> result = null;
            var localPrecondition = _preconditionsRepository.GetById(id);
            if (localPrecondition != null)
            {
                result = DeleteSinglePrecondition(localPrecondition);
            }
            return result ?? new ResultDto<bool>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = false,
            };
        }

        public ResultDto<bool> DeleteAllPreconditionsByTask(int taskId)
        {
            LogginingService.LogInfo($"Delete All Precondition by taskId {taskId}");
            ResultDto<bool> result = null;
            var localPreconditions = _preconditionsRepository.GetAllByTaskId(taskId).ToList();
            
            if (localPreconditions?.Any() == true)
            {
                foreach (var precondition in localPreconditions)
                {
                    result = DeleteSinglePrecondition(precondition);
                    if (result.Errors != null)
                    {
                        return result;
                    }
                }
            }
            return result;
        }

        private ResultDto<bool> DeleteSinglePrecondition(Entities.Tenant.Preconditions precondition) {
            ResultDto<bool> result = null;
            if (precondition != null)
            {
                if (precondition.LiteralsPreconditions?.Any() == true)
                {
                    foreach (var literalPrecondition in precondition.LiteralsPreconditions)
                    {
                        _literalPreconditionsService.DeleteOnContext(literalPrecondition.Id);
                    }
                }

                var resultSave = _preconditionsRepository.DeletePreconditions(precondition);
                result = ProcessResult(resultSave.IsOk, resultSave);
            }
            return result;
        }
    }
}