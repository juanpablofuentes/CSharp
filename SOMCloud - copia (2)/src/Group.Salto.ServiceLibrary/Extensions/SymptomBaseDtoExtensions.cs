using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Symptoms;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class SymptomBaseDtoExtensions
    {
        public static SymptomBaseDto ToBaseDto(this Symptom source)
        {
            SymptomBaseDto result = null;
            if (source != null)
            {
                result = new SymptomBaseDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description
                };
            }
            return result;
        }

        public static IList<SymptomBaseDto> ToDto(this IList<Symptom> source)
        {
            return source?.MapList(c => c.ToBaseDto());
        }

        public static Symptom ToEntity(this SymptomBaseDto source)
        {
            Symptom result = null;
            if (source != null)
            {
                result = new Symptom()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description                 
                };
            }
            return result;
        }

        public static Symptom Update(this Symptom target, SymptomBaseDto source)
        {
            if (target != null && source != null)
            {
                target.Name = source.Name;
                target.Description = source.Description;             
            }
            return target;
        }
    }
}