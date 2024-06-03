using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UshtrimeKOL2.Auth;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace UshtrimeKOL2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthenticateController (IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserDto userDto)
        {
            var user = await _userManager.FindByNameAsync(userDto.Username);

            if ( user != null && await _userManager.CheckPasswordAsync(user, userDto.Password))
            {
                string token = GetToken(user);
                return Ok(token);

            }
            return NotFound("User is not found");

        }




        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var userExists = await _userManager.FindByNameAsync(registerDto.Username);
            if (userExists != null)
                return BadRequest();

            var user = new IdentityUser()
            {
                Email = registerDto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerDto.Username
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
                return Ok(result);

            return Ok("User created successfully!");
        }

        [HttpGet]
        [Route("singing-google")]
        public IActionResult SingInWithGoogle()
        {
            try
            {

                var authenticationProperties = new AuthenticationProperties { RedirectUri = Url.Action(nameof(GoogleSignCallback)) };
                return Challenge(authenticationProperties, "Google");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("google-signin-callback")]
        public async Task<IActionResult> GoogleSingInCallback()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync("Google");

            if (!authenticateResult.Succeeded)
                return BadRequest("Error while authenticating");

            // Extract the user info you need from the result
            var claims = authenticateResult.Principal.Identities.FirstOrDefault()?.Claims;
            var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var name = claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var userModel = new UserModel()
            {
                Email = email,
                FullName = name
            };

            // Issue JWT token after successful Google sign-in
            var token = GetToken(userModel);

            // Redirect to front-end with the token or return it as a JSON object
            return Ok(new { Token = token });
        }

        private object GetToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.FullName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                   _configuration["Jwt:Audience"],
                   claims,
                   expires: DateTime.Now.AddMinutes(15),
                   signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GetToken(IdentityUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                   _configuration["Jwt:Audience"],
                   claims,
                   expires: DateTime.Now.AddMinutes(15),
                   signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
