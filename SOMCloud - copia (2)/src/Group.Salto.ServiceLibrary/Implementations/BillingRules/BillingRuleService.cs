using Group.Salto.Common;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.BillingRule;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.BillingRule;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.BillingRules
{
    public class BillingRuleService: BaseService, IBillingRuleService
    {
        private readonly IBillingRuleRepository _billingRuleRepository;
           
        public BillingRuleService(ILoggingService logginingService,
                                IBillingRuleRepository billingRulesRepository) : base(logginingService)
        {
          _billingRuleRepository = billingRulesRepository ?? throw new ArgumentNullException($"{nameof(IBillingRuleRepository)} is null");
        }

        public ResultDto<IList<BillingRuleDto>> GetAllByTaskId(int id)
        {
            LogginingService.LogInfo($"Get all billing rules by taskid");
            IList<BillingRuleDto> result = new List<BillingRuleDto>();

            var billignRulesList = _billingRuleRepository.GetAllByTaskId(id).ToList();
            result = billignRulesList.ToListDto();

            return ProcessResult(result);
        }

        public ResultDto<BillingRuleBaseDto> Update(BillingRuleBaseDto billingRule) 
        {
            var entity = _billingRuleRepository.GetById(billingRule.Id);
            entity.UpdateBillingRule(billingRule);
            var result = _billingRuleRepository.UpdateBillingRule(entity);
            return ProcessResult(result.Entity?.ToBaseDto(), result);
        }

        public ResultDto<BillingRuleBaseDto> Create(BillingRuleBaseDto billingRule) 
        {            
            var result = _billingRuleRepository.CreateBillingRule(billingRule.ToEntity());
            return ProcessResult(result.Entity?.ToBaseDto(), result);
        }
               
        public ResultDto<bool> Delete(int id) 
        {            
            var entity = _billingRuleRepository.GetById(id);
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
            var result = _billingRuleRepository.DeleteBillingRule(entity);
            return ProcessResult(result.IsOk, result);
        }
    }
}
