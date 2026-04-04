using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TLA_BackendExtended.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class AuthController : ControllerBase
    {



        [HttpPost("admin-login")]
        public IActionResult AdminLogin([FromBody] string password)
        {
            if (password != "adminpass123")
            {
                return Unauthorized("Fel lösenord.");
            }

            var claims = new[]
            {
        new Claim(ClaimTypes.Name, "AdminUser"),
        new Claim(ClaimTypes.Role, "Admin") 
    };

            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("adminSecurityKey123thisIsASuperSafeKeyOnGod"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            
            var token = new JwtSecurityToken(
                issuer: "TLA_Backend_App",
                audience: "TLA_Frontend",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

   
            return Ok(new { Token = tokenString });
        }
    }
}
