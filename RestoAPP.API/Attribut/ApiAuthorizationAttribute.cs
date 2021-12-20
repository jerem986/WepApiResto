using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ToolBox.Security.Services;

namespace RestoAPP.API.Attribut
{
    public class ApiAuthorizationAttribute : Attribute
    {
        private string[] _roles { get; set; }
        public ApiAuthorizationAttribute(params string[] lvlUsers)
        {
            _roles = lvlUsers;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            foreach (string role in _roles)
            {
                if (context.HttpContext.User.IsInRole(role)) //vérification du role de l'user
                {
                    return; //ok pour se connecter et effectuer le controller
                }
            }
            context.Result = new UnauthorizedResult(); //refus de rentrer dans le controller
        }  
        
        
        //VERSION FURFOOZ
        //private string[] roles;

        //public ApiAuthorizationAttribute(params string[] roles)
        //{
        //    this.roles = roles;
        //}

        //public void OnAuthorization(AuthorizationFilterContext context)
        //{
        //    context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues values);

        //    string token = values.FirstOrDefault(h => h.StartsWith("Bearer "))?.Replace("Bearer ", "");

        //    if (token == null)
        //    {
        //        context.Result = new UnauthorizedResult();
        //    }
        //    else
        //    {
        //        JwtService service = (JwtService)context.HttpContext.RequestServices.GetService(typeof(JwtService));
        //        ClaimsPrincipal claims = service.Decode(token);
        //        if (claims == null)
        //        {
        //            context.Result = new UnauthorizedResult();
        //        }
        //        else
        //        {
        //            foreach (string role in roles)
        //            {
        //                if (claims.IsInRole(role))
        //                {
        //                    context.HttpContext.User = claims;
        //                    return;
        //                }
        //            }
        //            context.Result = new UnauthorizedResult();
        //        }
        //    }

        //}

    }
}
