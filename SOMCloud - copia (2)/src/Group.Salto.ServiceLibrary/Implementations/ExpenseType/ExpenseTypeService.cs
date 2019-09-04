using Group.Salto.Common;
using Group.Salto.Common.Helpers;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ExpenseType;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Expenses;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group.Salto.ServiceLibrary.Implementations.ExpenseType
{
    public class ExpenseTypeService: BaseService, IExpenseTypeService
    {
        private readonly IExpenseTypeRepository _expensetypeRepository;
        public ExpenseTypeService(ILoggingService logginingService, IExpenseTypeRepository expensetypeRepository) : base(logginingService)
        {
            _expensetypeRepository = expensetypeRepository ?? throw new ArgumentNullException($"{nameof(IExpenseTypeRepository)} is null ");
        }

        public ResultDto<ExpenseTypeDto> GetById(int id)
        {
            LogginingService.LogInfo($"Get By Id expense type");
            var data = _expensetypeRepository.GetById(id);
            return ProcessResult(data.ToDto());
        }

        public ResultDto<IList<ExpenseTypeDto>> GetAllFiltered(ExpenseTypeFilterDto filter)
        {
            LogginingService.LogInfo($"Get All expense type Filtered");
            var query = _expensetypeRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Type, au => au.Type.Contains(filter.Type));
            var data = query.ToList().MapList(c => c.ToDto()).AsQueryable();
            data = OrderBy(data, filter);
            return ProcessResult<IList<ExpenseTypeDto>>(data.ToList());
        }

        public ResultDto<ExpenseTypeDto> UpdateExpenseType(ExpenseTypeDto source)
        {
            LogginingService.LogInfo($"Update Expense type");
            ResultDto<ExpenseTypeDto> result = null;
            var findExpenseType = _expensetypeRepository.GetById(source.Id);
            if (findExpenseType != null)
            {
                var updatedExpenseType = findExpenseType.Update(source);
                var resultRepository = _expensetypeRepository.UpdateExpenseType(updatedExpenseType);
                result = ProcessResult(resultRepository.Entity?.ToDto(), resultRepository);
            }

            return result ?? new ResultDto<ExpenseTypeDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = source,
            };
        }

        public ResultDto<bool> DeleteExpenseType(int id)
        {
            LogginingService.LogInfo($"Delete expense type by id {id}");
            ResultDto<bool> result = null;
            var expensetype = _expensetypeRepository.GetById(id);
           if (expensetype == null)
            {
                return result ?? new ResultDto<bool>()
                {
                    Errors = new ErrorsDto()
                    {
                        Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                    },
                    Data = false,
                };
            }
            if (expensetype.Expenses.Count == 0)
            {
                var resultSave = _expensetypeRepository.DeleteExpenseType(expensetype);
                result = ProcessResult(resultSave.IsOk, resultSave);
            }
            return result ?? new ResultDto<bool>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.ValidationError } }
                },
                Data = false,
            };
        }

        public ResultDto<ExpenseTypeDto> CreateExpenseType(ExpenseTypeDto source)
        {
            LogginingService.LogInfo($"Create expense type");
            var newExpenseType = source.ToEntity();
            var result = _expensetypeRepository.CreateExpenseType(newExpenseType);
            return ProcessResult(result.Entity?.ToDto(), result);
        }

        private IQueryable<ExpenseTypeDto> OrderBy(IQueryable<ExpenseTypeDto> query, ExpenseTypeFilterDto filter)
        {
            LogginingService.LogInfo($"Order By expense type");
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Type);
            return query;
        }
    }
}