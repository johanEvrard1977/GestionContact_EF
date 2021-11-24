using GestionContactEF.Dal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionContact.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthRequiredAttribute : Attribute, IAuthorizationFilter
    {
        private string _role;
        public AuthRequiredAttribute(string role = null)
        {
            _role = role;
        }

        public virtual void OnAuthorization(AuthorizationFilterContext context)
        {
            ITokenService tokenService = (ITokenService)context.HttpContext.RequestServices.GetService(typeof(ITokenService));
            
            context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues authorizations);
            string token = authorizations.SingleOrDefault(authorization => authorization.StartsWith("Bearer "));

            if (token is null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            LoginSuccessDto user = tokenService.ValidateToken(token);

            if (user is null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!(_role is null))
            {
                if (!user.Role.Equals(_role))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }

        }
    }
}