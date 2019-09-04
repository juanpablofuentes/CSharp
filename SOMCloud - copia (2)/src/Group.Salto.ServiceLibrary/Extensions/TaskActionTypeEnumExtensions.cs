using System;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class TaskActionTypeEnumExtensions
    {
        public static TaskActionTypeEnum ToTaskAction(this TaskTypeEnum taskTypeEnum)
        {
            var result = TaskActionTypeEnum.NotImplemented;
            if (Enum.TryParse(taskTypeEnum.ToString(), out TaskActionTypeEnum taskActionType))
            {
                result = taskActionType;
            }
            return result;
        }
    }
}
