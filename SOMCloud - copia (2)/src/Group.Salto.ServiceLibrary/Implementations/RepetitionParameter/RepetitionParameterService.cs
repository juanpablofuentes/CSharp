using Group.Salto.ServiceLibrary.Common.Contracts.RepetitionParameters;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System;
using System.Collections.Generic;
using System.Text;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.RepetitionParameter;
using System.Linq;
using Group.Salto.ServiceLibrary.Extensions;
using Group.Salto.Common;
using Group.Salto.ServiceLibrary.Common.Dtos.PeopleVisible;
using Group.Salto.Extensions;
using Group.Salto.Entities.Tenant;
using Group.Salto.Common.Helpers;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.ServiceLibrary.Implementations.RepetitionParameter
{
    public class RepetitionParameterService : BaseService, IRepetitionParameterService
    {
        private readonly IRepetitionParametersRepository _repetitionParametersRepository; 
        private readonly ICalculationTypeRepository _calculationTypeRepository;
        private readonly IDamagedEquipmentRepository _damagedEquipmentRepository;
        private readonly IDaysTypeRepository _daysTypeRepository;
        public RepetitionParameterService(ILoggingService logginingService,
                               IRepetitionParametersRepository repetitionParametersRepository,
                               ICalculationTypeRepository calculationTypeRepository,
                               IDamagedEquipmentRepository damagedEquipmentRepository,
                               IDaysTypeRepository daysTypeRepository)
            : base(logginingService)
        {
            _repetitionParametersRepository = repetitionParametersRepository ?? throw new ArgumentNullException($"{nameof(repetitionParametersRepository)} is null ");
            _calculationTypeRepository = calculationTypeRepository ?? throw new ArgumentNullException($"{nameof(calculationTypeRepository)} is null ");
            _damagedEquipmentRepository = damagedEquipmentRepository ?? throw new ArgumentNullException($"{nameof(damagedEquipmentRepository)} is null");
            _daysTypeRepository = daysTypeRepository ?? throw new ArgumentNullException($"{nameof(daysTypeRepository)} is null");
        }

        public ResultDto<IList<RepetitionParameterDto>> GetAll()
        {
            LogginingService.LogInfo($"Get All RepetitionParameters");
            var data = _repetitionParametersRepository.GetAll().ToList();
            return ProcessResult(data.ToDto());
        }

        public ResultDto<RepetitionParametersDetailDto> GetFirst()
        {
            LogginingService.LogInfo($"Get Id RepetitionParameter");
            var data = _repetitionParametersRepository.GetAll().FirstOrDefault();
            return ProcessResult(data.ToDetailDto(), data != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });

        }
        
        public ResultDto<RepetitionParametersDetailDto> Update(RepetitionParametersDetailDto model)
        {
            ResultDto<RepetitionParametersDetailDto> result = null;

            var entity = _repetitionParametersRepository.GetFirst();
            if (entity != null)
            {
                entity.Update(model);
                var resultRepository = _repetitionParametersRepository.UpdateRepetitionParameters(entity);
                result = ProcessResult(resultRepository?.Entity?.ToDetailDto(), resultRepository);
            }
            return result ?? new ResultDto<RepetitionParametersDetailDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = model,
            };
        }
    }
}