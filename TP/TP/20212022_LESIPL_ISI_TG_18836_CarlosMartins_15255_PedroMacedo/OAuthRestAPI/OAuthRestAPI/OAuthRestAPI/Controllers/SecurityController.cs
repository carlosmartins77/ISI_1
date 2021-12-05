using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OAuthRestAPI.Controllers
{
    public class SecurityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

		//classe que gera o JWT
		private readonly IJWTAuthManager jWTAuthManager;

		public SecurityController(IJWTAuthManager jWTAuthManager)
		{
			this.jWTAuthManager = jWTAuthManager;

		}


		//private string GerarTokenJWT()
		//{
		//	var issuer = _config["Jwt:Issuer"];
		//	var audience = _config["Jwt:Audience"];
		//	var expiry = DateTime.Now.AddMinutes(120);  //válido por 2 horas
		//	var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
		//	var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

		//	var token = new JwtSecurityToken(issuer: issuer, audience: audience, expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials);

		//	var tokenHandler = new JwtSecurityTokenHandler();
		//	var stringToken = tokenHandler.WriteToken(token);
		//	return stringToken;
		//}
    }
}
