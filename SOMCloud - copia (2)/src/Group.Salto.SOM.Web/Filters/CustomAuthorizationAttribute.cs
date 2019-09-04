using Group.Salto.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Filters
{
    public class CustomAuthorizationAttribute : TypeFilterAttribute
    {
        public CustomAuthorizationAttribute(ActionGroupEnum actionGroup, ActionEnum action) : base(typeof(CustomAuthorizationFilter))
        {
            Arguments = new object[] { new Claim(actionGroup.ToString(), action.ToString()) };
        }
    }
}