using System.Collections.Generic;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class TechnicianDtoExtensions
    {
        public static TechnicianDto ToTechnicianDto(this People dbModel)
        {
            var dto = new TechnicianDto
            {
                Id = dbModel.Id,
                Dni = dbModel.Dni,
                Name = dbModel.Name,
                Surname = dbModel.FisrtSurname,
                SecondSurname = dbModel.SecondSurname
            };

            return dto;
        }

        public static IEnumerable<TechnicianDto> ToTechnicianDto(this IEnumerable<People> dbModelList)
        {
            var dtoList = new List<TechnicianDto>();

            foreach (var dbModel in dbModelList)
            {
                dtoList.Add(dbModel.ToTechnicianDto());
            }

            return dtoList;
        }
    }
}
