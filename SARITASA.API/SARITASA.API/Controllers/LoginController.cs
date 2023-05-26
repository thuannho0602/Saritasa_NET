using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SARITASA.Entity;
using SARITASA.Model;
using SARITASA.Sevices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SARITASA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {
        private readonly UserManager<User> userManager;
        private readonly IUserServices userService;
        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        public LoginController(UserManager<User> userManager, IUserServices userService, IConfiguration configuration, RoleManager<IdentityRole<Guid>> roleManager)
        {
            this.userManager = userManager;
            this.userService = userService;
            this.configuration = configuration;
            _roleManager = roleManager;
        }

        // create User

        [HttpPost("createUser")]

        //[Authorize]
        public async Task<IActionResult> CreateUser(UserCreatRequest newUserDTO)
        {
            var newUser = new User();
            {
                newUser.UserName = newUserDTO.UserName;
                newUser.FirstName = newUserDTO.FirstName;
                newUser.LastName = newUserDTO.LastName;
                newUser.PhoneNumber = newUserDTO.PhoneNumber;
                newUser.Email = newUserDTO.UserName;
                newUser.DateOfBirth = newUserDTO.DateOfBirth;

                newUser.Address = newUserDTO.Address;

                //EmailConfirmed = true,

            };
            var result = await userManager.CreateAsync(newUser, newUserDTO.Password);
            var resultRole = new IdentityResult();
            var userResult = new IdentityResult();

            if (result.Succeeded)
            {
                bool roleExists = await _roleManager.RoleExistsAsync(newUserDTO.Role);
                if (!roleExists)
                {

                    resultRole = await _roleManager.CreateAsync(new IdentityRole<Guid>(newUserDTO.Role));
                }

                // Select the user, and then add the admin role to the user
                if (!await userManager.IsInRoleAsync(newUser, newUserDTO.Role))
                {

                    userResult = await userManager.AddToRoleAsync(newUser, newUserDTO.Role);
                }
                if (userResult.Succeeded)
                {
                    return Ok(new { MsgCode = "200", Message = "Create user successfuly!" });
                }
                return BadRequest(new { MsgCode = "403", Message = "Create fail" });


            }
            return BadRequest(new { MsgCode = "403", Message = "Create fail" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Login model)
        {
            if (!ModelState.IsValid)
            {
                return InvalidModel();
            }
            var user = await userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return Failure("Password và Email Không Hợp Lệ");
            }
            var verigyResult = userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);
            if (verigyResult == PasswordVerificationResult.Failed)
            {
                return Failure("Password và Email Không Hợp Lệ");
            }
            var token = await CreatedToken(user);
            return Success(token);
        }

        private async Task<string> CreatedToken(User user)
        {
            var username = user.UserName != null ? user.UserName : "";
            var email = user.Email != null ? user.Email : "";
            var firstname = user.FirstName != null ? user.FirstName : "";
            var lastname = user.LastName != null ? user.LastName : "";
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName,username),
                new Claim(JwtRegisteredClaimNames.Email,email),
                new Claim(JwtRegisteredClaimNames.GivenName,firstname),
                new Claim(JwtRegisteredClaimNames.FamilyName,lastname)
            };
            var config = configuration.GetSection("Jwt").Get<JwtTokenSetting>();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.SecretKey));
            var token = new JwtSecurityToken(
                issuer: config.SecretKey,
                audience: config.SecretKey,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(config.ExpiryMinutes),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }

}
