using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PractAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static readonly List<User> users = [new User("user", "user", "User"), new User("admin", "admin", "Admin")];

        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public ActionResult<string> Login([FromBody]User loginData)
        {
            User? user = users.FirstOrDefault(u => u.Login == loginData.Login && u.Password == loginData.Password);

            if (user != null)
            {
                List<Claim> claims = [new Claim(ClaimTypes.Role, user.Role!)];

                var jwt = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromHours(1)),
                    signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtOptions")["SecretKey"]!)), SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
            }

            return NotFound("User with these login and password doesn't exist");
        }
    }
}
