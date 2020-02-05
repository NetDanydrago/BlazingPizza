using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlazingPizza.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BlazingPizza.Server.Controllers
{

    [ApiController]
    public class UserController : ControllerBase
    {
        private static UserInfo LoggedOutUser =
                                    new UserInfo { IsAuthenticated = false };
        /// <summary>
        /// Devolver el Estado de la Autenticacion.
        /// </summary>
        /// <returns></returns>
        [HttpGet("user")]
        public UserInfo GetUser()
        {
            return User.Identity.IsAuthenticated ?
                new UserInfo
                {
                    Name = User.Identity.Name,
                    IsAuthenticated = true
                }
                : LoggedOutUser;
        }


        /// <summary>
        /// Realizar Proceso de Aunteticacion del Usuario
        /// </summary>
        /// <param name="reditectUri"></param>
        /// <returns></returns>
        [HttpGet("user/signin")]
        public async Task SignIn(string reditectUri)
        {
            if (string.IsNullOrEmpty(reditectUri) ||
                !Url.IsLocalUrl(reditectUri))
            {
                reditectUri = "/";
            }
            await HttpContext.ChallengeAsync(
                TwitterDefaults.AuthenticationScheme,
                new AuthenticationProperties
                { RedirectUri = reditectUri });
        }

        /// <summary>
        /// Realizar Proceso de Cierre.
        /// </summary>
        /// <returns></returns>
        [HttpGet("user/signout")]
        public async Task<IActionResult> SingOut()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("~/");
        }
    }
}