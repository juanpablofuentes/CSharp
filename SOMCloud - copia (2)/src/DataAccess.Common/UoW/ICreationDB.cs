using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Common.UoW
{
    public interface ICreationDB
    {
        bool EnsureCreatedDB();
    }
}
