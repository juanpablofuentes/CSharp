using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class FromToDtoExtensions
    {
        public static FromToDto ToFromToDto(this WorkOrderStatuses source)
        {
            var dto = new FromToDto
            {
                Id = source.Id,
                Name = source.Name
            };

            return dto;
        }

        public static FromToDto ToFromToDto(this ExternalWorOrderStatuses source)
        {
            var dto = new FromToDto
            {
                Id = source.Id,
                Name = source.Name
            };

            return dto;
        }

        public static FromToDto ToFromToDto(this Queues source)
        {
            var dto = new FromToDto
            {
                Id = source.Id,
                Name = source.Name
            };

            return dto;
        }
    }
}
