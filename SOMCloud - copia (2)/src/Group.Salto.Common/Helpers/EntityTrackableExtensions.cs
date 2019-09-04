using Group.Salto.Common.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Group.Salto.Common.Helpers
{
    public static class EntityTrackableExtensions
    {
        public static List<Tracker> GetPropertiesTrackablesModified(this EntityEntry entityTrackable, string ownerId)
        {
            List<Tracker> trackerList = new List<Tracker>();
            var test = entityTrackable.Entity.GetType().GetProperties();
            var propertiesTrackable = entityTrackable.Entity.GetType().GetProperties().Where(p => p.GetCustomAttribute(typeof(TrackableAttribute)) != null);
            if (propertiesTrackable.Any())
            {
                string entityType = entityTrackable.Entity.GetType().Name;
                string timeStamp = DateTime.UtcNow.ToString();
                string entityId = entityTrackable.Property("Id").OriginalValue.ToString();
                Guid transactionId = Guid.NewGuid();

                PropertyValues oldValues = entityTrackable.GetDatabaseValuesAsync().Result;
                foreach (PropertyInfo prop in propertiesTrackable)
                {
                    string propName = prop.Name;
                    Tracker trackerNew = new Tracker(entityType, propName, timeStamp, entityId, ownerId, transactionId);

                    string originalValue = oldValues.GetValue<object>(propName).ToString();
                    string currentValue = entityTrackable.Property(propName).CurrentValue.ToString();
                    if (!Equals(originalValue, currentValue))
                    {
                        trackerNew.PropertyName = propName;
                        trackerNew.OldValue = originalValue ?? string.Empty;
                        trackerNew.NewValue = currentValue ?? string.Empty;
                        trackerList.Add(trackerNew);
                    }
                }
            }
            return trackerList;
        }
    }
}
