using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Excel;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Grid;
using Group.Salto.ServiceLibrary.Extensions;
using Group.Salto.ServiceLibrary.Implementations.Excel;

namespace Group.Salto.ServiceLibrary.Implementations
{
    public class BaseService
    {
        protected ILoggingService LogginingService { get; }

        public BaseService(ILoggingService logginingService)
        {
            LogginingService = logginingService ?? throw new ArgumentNullException($"{nameof(ILoggingService)} is null");
        }

        public ResultDto<TDto> ProcessResult<TDto>(TDto data, ErrorDto error = null)
        {
            return new ResultDto<TDto>()
            {
                Data = data,
                Errors = error != null ? new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { error },
                } : null,
            };
        }

        public ResultDto<TDto> ProcessResult<TDto>(TDto data, List<ErrorDto> errors)
        {
            return new ResultDto<TDto>()
            {
                Data = data,
                Errors = new ErrorsDto()
                {
                    Errors = errors,
                },
            };
        }

        public ResultDto<TDto> ProcessResult<TDto, TEntity>(TDto data, SaveResult<TEntity> repositoryResult)
        {
            LogErrors(repositoryResult.Error);
            return new ResultDto<TDto>()
            {
                Data = data,
                Errors = !repositoryResult.IsOk ? repositoryResult.Error?.ToErrorsDto() : null,
            };
        }

        private void LogErrors(SaveError repositoryResultError)
        {
            if (repositoryResultError != null)
            {
                LogginingService.LogError($"{repositoryResultError.ErrorType.ToString()} : {repositoryResultError.ErrorMessage}");
            }
        }

        protected void InstanceError<T>(ResultDto<T> result)
        {
            if (result.Errors == null)
            {
                result.Errors = new ErrorsDto();
            }
        }

        protected byte[] ExportToExcel(IList<GridDataDto> listGridData, IList<string> columns)
        {
            IImportToExcel _importToExcel = new ImportToExcel();
            return _importToExcel.GenerateFile(listGridData, columns);
        }

        protected IEnumerable<T> GetPageIEnumerable<T>(IEnumerable<T> source, Common.Dtos.BaseFilterDto filterDto)
        {
            if (filterDto.Size == 0)
            {
                filterDto.Page = 1;
                return source.ToList();
            }
            return source.Skip((filterDto.Page - 1) * filterDto.Size).Take(filterDto.Size).ToList();
        }

        protected ResultDto<List<MultiSelectItemDto>> GetMultiSelect(IEnumerable<BaseNameIdDto<int>> items, List<int> selectItems)
        {
            LogginingService.LogInfo($"GetMultiSelect");

            List<MultiSelectItemDto> multiSelectItemDto = new List<MultiSelectItemDto>();
            foreach (BaseNameIdDto<int> item in items)
            {
                bool isCheck = selectItems.Any(x => x == item.Id);
                multiSelectItemDto.Add(new MultiSelectItemDto()
                {
                    LabelName = item.Name,
                    Value = item.Id.ToString(),
                    IsChecked = isCheck
                });
            }

            return ProcessResult(multiSelectItemDto, multiSelectItemDto != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }
    }
}