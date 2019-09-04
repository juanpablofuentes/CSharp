using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.PushNotifications;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class PushNotificationUserDtoExtensions
    {
        public static PushNotificationUserDto ToDto(this PeopleNotification source)
        {
            PushNotificationUserDto result = null;
            if (source != null)
            {
                result = new PushNotificationUserDto()
                {
                    Id = source.Id,
                    Title = source.Title,
                    PushMessage = source.Message,
                    SendDate = source.UpdateDate
                };
            }
            return result;
        }

        public static IList<PushNotificationUserDto> ToDto(this IQueryable<PeopleNotification> source)
        {
            return source?.MapList(x => x.ToDto());
        }
    }
}
