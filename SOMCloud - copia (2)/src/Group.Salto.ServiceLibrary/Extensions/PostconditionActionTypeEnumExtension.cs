using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums;
using System;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class PostconditionActionTypeEnumExtension
    {
        public static PostconditionActionTypeEnum ToPostconditionAction(this string actionString)
        {
            var result = PostconditionActionTypeEnum.NotImplemented;
            if (Enum.TryParse(actionString, out PostconditionActionTypeEnum postconditionAction))
            {
                result = postconditionAction;
            }
            return result;
        }
    }
}
