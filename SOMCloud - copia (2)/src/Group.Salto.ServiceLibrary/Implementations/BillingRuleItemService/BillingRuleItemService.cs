using Group.Salto.Common;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.BillingRuleItem;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.BillingRule;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.BillingRuleItemService
{
    public class BillingRuleItemService : BaseService, IBillingRuleItemService
    {
        private readonly IBillingRuleItemRepository _billingRuleItemRepository;
           
        public BillingRuleItemService(ILoggingService logginingService,
                                IBillingRuleItemRepository billingRuleItemRepository) : base(logginingService)
        {
          _billingRuleItemRepository = billingRuleItemRepository ?? throw new ArgumentNullException($"{nameof(IBillingRuleItemRepository)} is null");
        }

        public ResultDto<BillingRuleItemDto> Create(BillingRuleItemDto billingRuleItem)
        {                        
            var result = _billingRuleItemRepository.CreateBillingRuleItem(billingRuleItem.ToEntity());
            return ProcessResult(result.Entity?.ToDto(), result);
        }

        public ResultDto<BillingRuleItemDto> Update(BillingRuleItemDto billingRuleItem)
        {
            var entity = _billingRuleItemRepository.GetById(billingRuleItem.Id);
            entity.UpdateBillingRuleItem(billingRuleItem);
            var result = _billingRuleItemRepository.UpdateBillingRuleItem(entity);
            return ProcessResult(result.Entity?.ToDto(), result);                     
        }

        public ResultDto<bool> Delete(int id) 
        {            
            var entity = _billingRuleItemRepository.GetById(id);
            if (entity == null)
            {
                return new ResultDto<bool>()
                {
                    Errors = new ErrorsDto()
                    {
                        Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                    },
                    Data = false,
                };
            }
            var result = _billingRuleItemRepository.DeleteBillingRuleItem(entity);
            return ProcessResult(result.IsOk, result);
        }
    }
}