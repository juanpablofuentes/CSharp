using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Grid;
using Group.Salto.ServiceLibrary.Common.Dtos.People;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class PeopleListDtoExtensions
    {
        public static PeopleListDto ToListDto(this People source)
        {
            PeopleListDto result = null;
            if (source != null)
            {
                result = new PeopleListDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    FisrtSurname = source.FisrtSurname,
                    SecondSurname = source.SecondSurname,
                    Dni = source.Dni,
                    Email = source.Email,
                    Telephone = source.Telephone,
                    IsActive = source.IsActive.HasValue ? source.IsActive.Value : false,
                    IsClientWorker = (source.IsClientWorker == 1) ? true : false,
                    UserConfigurationId = source.UserConfigurationId,
                };
            }

            return result;
        }

        public static IList<PeopleListDto> ToListDto(this IList<People> source)
        {
            return source?.MapList(c => c.ToListDto());
        }

        public static IEnumerable<PeopleListDto> ToListDto(this IEnumerable<People> source)
        {
            return source?.MapList(c => c.ToListDto());
        }

        public static IList<GridDataDto> ToExcelListDto(this IEnumerable<PeopleListDto> source)
        {
            List<GridDataDto> data = new List<GridDataDto>();
            foreach (PeopleListDto people in source)
            {
                GridDataDto row = new GridDataDto();
                row.Id = people.Id;
                row.Data.Add(people.Name);
                row.Data.Add(people.FisrtSurname);
                row.Data.Add(people.SecondSurname);
                row.Data.Add(people.Dni);
                row.Data.Add(people.Email);
                row.Data.Add(people.Telephone);
                row.Data.Add(people.IsClientWorker.ToString());
                row.Data.Add(people.IsActive.ToString());
                row.Data.Add(people.UserName);
                data.Add(row);
            }
            return data;
        }
    }
}