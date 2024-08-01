using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace task2_restfulapi.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class Authorize : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userService = context.HttpContext.RequestServices.GetService(typeof(task2_restfulapi.Services.IUserService)) as task2_restfulapi.Services.IUserService;
            var user = userService?.GetCurrentUser();

            if (string.IsNullOrEmpty(user))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
