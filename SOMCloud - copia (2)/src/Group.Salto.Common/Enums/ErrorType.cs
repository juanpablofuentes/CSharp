namespace Group.Salto.Common
{
    public enum ErrorType
    {
        SaveChangesException = 30000,
        SaveChangesNoRows = 30001,
        EntityNotExists = 30002,
        CannotAccessDatabase = 40001,
        DatabaseAlredyExists = 40002,
        EntityAlredyExists = 40003,
        ValidationError = 40004,
        DateRangeValidationError = 40005,
        FormFieldsValidationError = 40006,
        TaskExecutionProcessError = 40007,
    }
}
