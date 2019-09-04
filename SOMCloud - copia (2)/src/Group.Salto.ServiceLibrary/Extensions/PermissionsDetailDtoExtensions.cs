using System.Linq;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Permisions;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class PermissionsDetailDtoExtensions
    {
        public static PermissionDetailDto ToDetailDto(this Permissions source)
        {
            PermissionDetailDto result = null;
            if (source != null)
            {
                result = new PermissionDetailDto();
                source.ToDto(result);
                result.TasksChecked = source.Tasks;
                result.CanBeDeleted = source.CanBeDeleted;
                result.People = source.PeoplePermission?.Select(x => x.PeopleId)?.ToList();
                result.Tasks = source.PermissionTask?.Select(x => x.TaskId)?.ToList();
                result.PredefinedServices =
                    source.PredefinedServicesPermission?.Select(x => x.PredefinedServiceId)?.ToList();
                result.Projects = source.ProjectPermission?.Select(x => x.ProjectId)?.ToList();
                result.Queues = source.PermissionQueue?.Select(x => x.QueueId)?.ToList();
                result.WorkOrdersCategories = source.WorkOrderCategoryPermission?.Select(x => x.WorkOrderCategoryId)?.ToList();
            }

            return result;
        }

        public static Permissions ToEntity(this PermissionDetailDto source)
        {
            Permissions result = null;
            if (source != null)
            {
                result = new Permissions()
                {
                    Name = source.Name,
                    CanBeDeleted = source.CanBeDeleted,
                    Description = source.Description,
                    Observations = source.Observations,
                    Tasks = source.TasksChecked,

            };
            }

            return result;
        }

        public static void Update(this Permissions target, PermissionDetailDto source)
        {
            if (target != null && source != null)
            {
                target.Name = source.Name;
                target.CanBeDeleted = source.CanBeDeleted;
                target.Tasks = source.TasksChecked;
                target.Description = source.Description;
                target.Observations = source.Observations;
            }
        }

        public static void Clear(this Permissions target)
        {
            if (target != null)
            {
                target.WorkOrderCategoryPermission?.Clear();
                target.PredefinedServicesPermission?.Clear();
                target.PermissionTask?.Clear();
                target.PermissionQueue?.Clear();
                target.PeopleCollectionPermission?.Clear();
                target.ProjectPermission?.Clear();
                target.PeoplePermission?.Clear();
            }
        }
    }
}