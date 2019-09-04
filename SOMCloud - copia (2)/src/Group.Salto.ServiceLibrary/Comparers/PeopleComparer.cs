using Group.Salto.ServiceLibrary.Common.Dtos.People;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Comparers
{
    public class PeopleComparer : IEqualityComparer<PeopleListDto>
    {
        public bool Equals(PeopleListDto x, PeopleListDto y)
        {
            if (Object.ReferenceEquals(x, y)) return true;

            return x != null && y != null && x.UserConfigurationId.Equals(y.UserConfigurationId);
        }

        public int GetHashCode(PeopleListDto obj)
        {
            int hashValue = obj.UserConfigurationId == null ? 0 : obj.UserConfigurationId.GetHashCode();

            return hashValue;
        }
    }
}