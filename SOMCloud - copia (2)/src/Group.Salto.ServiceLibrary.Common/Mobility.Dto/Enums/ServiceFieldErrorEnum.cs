namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums
{
    public enum ServiceFieldErrorEnum
    {
        Default = 0,
        StartDatBeforeWorkOrderDate = 1,
        StartDatBeforeEndDate = 2,
        WaitTimeBiggerThanDuration = 3,
        InterventionTimeMoreThanOneDay = 4,
        NegativeTravelOrKilometers = 5
    }
}
