using Microsoft.AspNetCore.Mvc;
using OAuthRestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OAuthRestAPI.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        

        //[HttpPost("Login")]
        //public AuthResponse Login(AuthRequest loginDetalhes)
        //{
        //    AuthResponse token = jWTAuthManager.Authenticate(loginDetalhes);

        //    if (token == null)
        //    {
        //        token.Token = Unauthorized().ToString();
        //    }

        //    return token;
        //}


    }
}
