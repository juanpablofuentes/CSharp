using Group.Salto.Common;
using Group.Salto.Common.Helpers;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.PaymentMethod;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Expenses;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group.Salto.ServiceLibrary.Implementations.PaymentMethod
{
    public  class PaymentMethodService : BaseService, IPaymentMethodService
    {
        private readonly IPaymentMethodRepository _paymentmethodRepository;
        public PaymentMethodService(ILoggingService logginingService,
            IPaymentMethodRepository paymentmethodRepository) : base( logginingService)
        {
            _paymentmethodRepository = paymentmethodRepository ?? throw new ArgumentNullException($"{nameof(IPaymentMethodRepository)} is null ");
        }

        public ResultDto<PaymentMethodDto> GetById(int id)
        {
            LogginingService.LogInfo($"Get By Id Payment method");
            var data = _paymentmethodRepository.GetById(id);
            return ProcessResult(data.ToDto());
        }

        public ResultDto<IList<PaymentMethodDto>> GetAllFiltered(PaymentMethodFilterDto filter)
        {
            LogginingService.LogInfo($"Get All paymentmethod Filtered");
            var query = _paymentmethodRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Name, au => au.Mode.Contains(filter.Name));
            query = OrderBy(query, filter);
            return ProcessResult(query.ToList().MapList(c => c.ToDto()));
        }

        public ResultDto<PaymentMethodDto> CreatePaymentMethod(PaymentMethodDto source)
        {
            LogginingService.LogInfo($"Create payment method");
            var newPaymentMethod = source.ToEntity();
            var result = _paymentmethodRepository.CreatePaymentMethod(newPaymentMethod);
            return ProcessResult(result.Entity?.ToDto(), result);
        }

        public ResultDto<PaymentMethodDto> UpdatePaymentMethod(PaymentMethodDto source)
        {
            LogginingService.LogInfo($"Update paymentmethod");
            ResultDto<PaymentMethodDto> result = null;
            var findPaymentMethod = _paymentmethodRepository.GetById(source.Id);
            if (findPaymentMethod != null)
            {
                var updatedBrands = findPaymentMethod.Update(source);
                var resultRepository = _paymentmethodRepository.UpdatePaymentMethod(updatedBrands);
                result = ProcessResult(resultRepository.Entity?.ToDto(), resultRepository);
            }

            return result ?? new ResultDto<PaymentMethodDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = source,
            };
        }

        public ResultDto<bool> DeletePaymentMethod(int id)
        {
            LogginingService.LogInfo($"Delete paymentmethod by id {id}");
            ResultDto<bool> result = null;
            var paymentmethod = _paymentmethodRepository.GetById(id);
            if (paymentmethod == null)
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
            if (paymentmethod.Expenses.Count == 0)
            {
                var resultSave = _paymentmethodRepository.DeletePaymentMethod(paymentmethod);
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

        private IQueryable<Entities.Tenant.PaymentMethods> OrderBy(IQueryable<Entities.Tenant.PaymentMethods> query, PaymentMethodFilterDto filter)
        {
            LogginingService.LogInfo($"Order By Payment method");
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Mode);
            return query;
        }
    }
}