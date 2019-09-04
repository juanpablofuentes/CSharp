namespace Group.Salto.ServiceLibrary.Common.Contracts.Ncalc
{
    public interface INcalcService
    {
        object EvaluateExpression(string billingRuleProcessorResult);
        object EvaluateBillRuleItemExpression(string billingRuleItemProcessorResult);
    }
}
