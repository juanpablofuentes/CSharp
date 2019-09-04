using System.Collections.Generic;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Expenses;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ExpenseTypeDtoExtensions
    {
        public static ExpenseTypeDto ToDto(this ExpenseTypes dbModel)
        {
            var dto = new ExpenseTypeDto
            {
                Id = dbModel.Id,
                Type = dbModel.Type,
                Unit = dbModel.Unit
            };
            
            return dto;
        }
        
        public static IEnumerable<ExpenseTypeDto> ToDto(this IEnumerable<ExpenseTypes> dbModelList)
        {
            var dtoList = new List<ExpenseTypeDto>();
            foreach (var dbModel in dbModelList)
            {
                dtoList.Add(dbModel.ToDto());
            }

            return dtoList;
        }

        public static ExpenseTypes ToEntity(this ExpenseTypeDto source)
        {
            ExpenseTypes result = null;
            if (source != null)
            {
                result = new ExpenseTypes()
                {
                    Id = source.Id,
                    Type = source.Type,
                    Unit = source.Unit    
                };
            }
            return result;
        }

        public static ExpenseTypes Update(this ExpenseTypes target, ExpenseTypeDto source)
        {
            if (target != null && source != null)
            {
                target.Id = source.Id;
                target.Type = source.Type;
                target.Unit = source.Unit;
            }
            return target;
        }
    }
}