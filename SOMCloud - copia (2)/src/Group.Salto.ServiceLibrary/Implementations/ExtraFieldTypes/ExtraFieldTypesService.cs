using Group.Salto.Common;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ExtraFieldTypes;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.ExtraFieldTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.ServiceLibrary.Extensions;

namespace Group.Salto.ServiceLibrary.Implementations.ExtraFieldTypes
{
    public class ExtraFieldTypesService : BaseService, IExtraFieldTypesService
    {
        private readonly IExtraFieldTypesRepository _extraFieldsTypesRepository;

        public ExtraFieldTypesService(ILoggingService logginingService, IExtraFieldTypesRepository extraFieldsTypesRepository) : base(logginingService)
        {
            _extraFieldsTypesRepository = extraFieldsTypesRepository ?? throw new ArgumentNullException($"{nameof(IExtraFieldTypesRepository)} is null");
        }

        public ResultDto<ExtraFieldsTypesDto> GetById(int id)
        {
            LogginingService.LogInfo($"Get ExtraFieldsType by id {id}");
            var result = _extraFieldsTypesRepository.GetById(id)?.ToDto();

            return ProcessResult(result, result != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<IList<ExtraFieldsTypesDto>> GetAll()
        {
            LogginingService.LogInfo($"Get All ExtraFieldsType");
            var data = _extraFieldsTypesRepository.GetAll().ToList();
            return ProcessResult(data.ToDto());
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues()
        {
            LogginingService.LogInfo($"Get all ExtraFieldsType Key Values");
            var query = _extraFieldsTypesRepository.GetAllKeyValues();
            var data = query.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
            return data;
        }
    }
}