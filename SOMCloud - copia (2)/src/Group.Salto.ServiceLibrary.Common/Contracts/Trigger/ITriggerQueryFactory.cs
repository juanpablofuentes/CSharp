namespace Group.Salto.ServiceLibrary.Common.Contracts.Trigger
{
    public interface ITriggerQueryFactory
    {
        ITriggerResult GetQuery(string triggerType);
    }
}