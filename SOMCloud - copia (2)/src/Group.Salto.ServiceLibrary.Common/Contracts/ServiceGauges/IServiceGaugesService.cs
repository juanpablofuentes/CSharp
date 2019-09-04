using Group.Salto.ServiceLibrary.Common.Dtos.ServiceGauges;

namespace Group.Salto.ServiceLibrary.Common.Contracts.ServiceGauges
{
    public interface IServiceGaugesService
    {
        ServiceGaugesResultFilterDto ServiceAnalysis(ServiceGaugesFilterDto data);
        ServiceGaugesProjectReportDto GetProjectReportByMonth(ServiceGaugesFilterDto data);
    }
}