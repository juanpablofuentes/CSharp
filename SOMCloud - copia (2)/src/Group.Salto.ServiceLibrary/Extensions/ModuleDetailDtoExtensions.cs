using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos.Modules;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ModuleDetailDtoExtensions
    {
        public static Module ToEntity(this ModuleDetailDto source)
        {
            Module result = null;
            if (source != null)
            {
                result = new Module()
                {
                    Key = source.Key,
                    Description = source.Description
                };
            }
            return result;
        }

        public static ModuleDetailDto ToDetailDto(this Module source)
        {
            ModuleDetailDto result = null;
            if (source != null)
            {
                result = new ModuleDetailDto
                {
                    Id = source.Id,
                    Key = source.Key,
                    Description = source.Description,
                    ActionGroupsSelected = source.ModuleActionGroups?.Select(x => x.ActionGroupsId)?.ToList()
                };
            }
            return result;
        }

        public static void Update(this Module target, ModuleDetailDto source)
        {
            if (target != null && source != null)
            {
                target.Key = source.Key;
                target.Description = source.Description;
            }
        }
    }
}