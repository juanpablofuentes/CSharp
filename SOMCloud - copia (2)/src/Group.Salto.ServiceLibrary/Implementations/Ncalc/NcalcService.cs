using System;
using Group.Salto.Common.Constants.Ncalc;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Ncalc;
using NCalc;

namespace Group.Salto.ServiceLibrary.Implementations.Ncalc
{
    public class NcalcService : INcalcService
    {
        private readonly ILoggingService _logginingService;

        public NcalcService(ILoggingService logginingService)
        {
            _logginingService = logginingService;
        }

        public object EvaluateExpression(string expressionToEvaluate)
        {
            object result;
            try
            {
                result = new Expression(expressionToEvaluate).Evaluate();
            }
            catch (Exception ex)
            {
                _logginingService.LogException(ex);
                throw;
            }
            
            return result;
        }

        public object EvaluateBillRuleItemExpression(string billingRuleExpressionToEvaluate)
        {
            object result;
            try
            {
                var e = new Expression(billingRuleExpressionToEvaluate);
                e.EvaluateFunction += delegate (string name, FunctionArgs args)
                {
                    if (name == NcalcConstants.HoursDiff)
                    {
                        var date1 = (DateTime)args.Parameters[0].Evaluate();
                        var date2 = (DateTime)args.Parameters[1].Evaluate();
                        var timespan = date2 - date1;
                        args.Result = timespan.TotalHours;
                    }
                };

                result = e.Evaluate();
            }
            catch (Exception ex)
            {
                _logginingService.LogException(ex);
                throw;
            }

            return result;
        }
    }
}
