using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Core.Security
{
    public class PermissionCheckerByPermissionNamesAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private IUserService _userService;
        private readonly string[] _permissionNames;
        public PermissionCheckerByPermissionNamesAttribute(string[] permissionNames)
        {

            _permissionNames = permissionNames;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _userService = (IUserService)context.HttpContext.RequestServices.GetService(typeof(IUserService));

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string username = context.HttpContext.User.Identity.Name;
                if (!_userService.CheckPermissionByNames(_permissionNames, username))
                {

                    context.Result = new RedirectResult("/AccessDenied");
                }
            }
            else
            {
                context.Result = new RedirectResult("/Login");
            }
        }
    }
}
