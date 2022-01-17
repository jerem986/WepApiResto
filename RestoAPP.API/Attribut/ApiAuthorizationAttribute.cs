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
        
    }
}
