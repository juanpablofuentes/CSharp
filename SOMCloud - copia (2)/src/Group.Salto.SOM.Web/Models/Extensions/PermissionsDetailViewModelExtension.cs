using System.Linq;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Permisions;
using Group.Salto.SOM.Web.Models.Permissions;
using Group.Salto.SOM.Web.Models.Result;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class PermissionsDetailViewModelExtension
    {
        public static ResultViewModel<PermissionDetailViewModel> ToResultViewModel(this ResultDto<PermissionDetailDto> source)
        {
            var response = source != null ? new ResultViewModel<PermissionDetailViewModel>()
            {
                Data = source.Data.ToDetailViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static PermissionDetailViewModel ToDetailViewModel(this PermissionDetailDto source)
        {
            PermissionDetailViewModel permission = null;
            if (source != null)
            {
                permission = new PermissionDetailViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Observations = source.Observations,
                    CanBeDeleted = source.CanBeDeleted == 1,
                    IsLocationResponsible = GetTaskValue(source.TasksChecked, 0),
                    IsDriver = GetTaskValue(source.TasksChecked, 1),
                    IsContact = GetTaskValue(source.TasksChecked, 2),
                    IsComercialTasks = GetTaskValue(source.TasksChecked, 3),
                    IsManagerTasks = GetTaskValue(source.TasksChecked, 4),
                    IsTechnicalTasks = GetTaskValue(source.TasksChecked, 5),
                    IsOperatorTasks = GetTaskValue(source.TasksChecked, 6),
                };
            }
            return permission;
        }

        private static bool GetTaskValue(string sourceTasksChecked, int index)
        {
            return !string.IsNullOrEmpty(sourceTasksChecked) && sourceTasksChecked.Length > index &&
                   int.TryParse(sourceTasksChecked[index].ToString(), out var temp) &&
                   int.Parse(sourceTasksChecked[index].ToString()) == 1;
        }

        private static string GetTasksString(PermissionDetailViewModel source)
        {
            var result = "";
            if (source != null)
            {
                result += source.IsLocationResponsible ? "1" : "0";
                result += source.IsDriver ? "1" : "0";
                result += source.IsContact ? "1" : "0";
                result += source.IsComercialTasks ? "1" : "0";
                result += source.IsManagerTasks ? "1" : "0";
                result += source.IsTechnicalTasks ? "1" : "0";
                result += source.IsOperatorTasks ? "1" : "0";
            }
            return !string.IsNullOrEmpty(result) ? result : "0000000";
        }

        public static void ToDto(this PermissionDetailViewModel source, PermissionsDto target)
        {
            if (source != null && target != null)
            {
                target.Id = source.Id ?? 0;
                target.Name = source.Name;
                target.Description = source.Description;
                target.Observations = source.Observations;
            }
        }

        public static PermissionDetailDto ToDto(this PermissionDetailViewModel source)
        {
            PermissionDetailDto result = null;
            if (source != null)
            {
                result = new PermissionDetailDto();
                source.ToDto(result);
                result.CanBeDeleted = source.CanBeDeleted ? 1 : 0;
                result.People = source.People?.Items.Where(x => x.IsChecked && int.TryParse(x.Value, out var temp))?
                                                    .Select(x => int.Parse(x.Value))?.ToList();
                result.Tasks = source.Tasks?.Items.Where(x => x.IsChecked && int.TryParse(x.Value, out var temp))?
                                                    .Select(x => int.Parse(x.Value))?.ToList();
                result.PredefinedServices = source.PredefinedServices?.Items.Where(x => x.IsChecked && int.TryParse(x.Value, out var temp))?
                                                    .Select(x => int.Parse(x.Value))?.ToList();
                result.Projects = source.Projects?.Items.Where(x => x.IsChecked && int.TryParse(x.Value, out var temp))?
                                                    .Select(x => int.Parse(x.Value))?.ToList();
                result.Queues = source.Queues?.Items.Where(x => x.IsChecked && int.TryParse(x.Value, out var temp))?
                                                    .Select(x => int.Parse(x.Value))?.ToList();
                result.WorkOrdersCategories = source.WorkOrderCategories?.Items.Where(x => x.IsChecked && int.TryParse(x.Value, out var temp))?
                                                    .Select(x => int.Parse(x.Value))?.ToList();
                result.TasksChecked = GetTasksString(source);

            }
            return result;
        }
    }
}