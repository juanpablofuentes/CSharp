using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos.Trigger;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class TriggerTypeDtoExtension
    {
        public static TriggerTypeDto ToDto(this TriggerTypes source)
        {
            TriggerTypeDto result = null;
            if (source != null)
            {
                result = new TriggerTypeDto() {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description
                };
            }
            return result;
        }
    }
}