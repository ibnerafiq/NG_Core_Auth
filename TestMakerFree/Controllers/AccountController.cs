using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TestMakerFree.Helpers;
using TestMakerFree.Models;

namespace TestMakerFree.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly AppSettings appSettings;

        public AccountController(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager, IOptions<AppSettings> appSettings)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.appSettings = appSettings.Value;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel registerViewModel)
        {
            List<string> errorList = new List<string>();

            var user = new IdentityUser
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.UserName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await userManager.CreateAsync(user, registerViewModel.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Customer");
                return Ok(new
                {
                    userName = user.UserName,
                    email = user.Email, status = 1,
                    message = "Registration successfully"
                });
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    errorList.Add(error.Description);
                }
            }

            return BadRequest(new JsonResult(errorList));

            //sending confirmation email.......


        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel formData)
        {
            var user = await userManager.FindByNameAsync(formData.UserName);
            var roles = await userManager.GetRolesAsync(user);
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Secret));
            double tokenExpiryTime = Convert.ToDouble(appSettings.ExpiryTime);
            if (user != null && await userManager.CheckPasswordAsync(user, formData.Password))
            {
               
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                            new Claim (JwtRegisteredClaimNames.Sub , formData.UserName ),
                            new Claim (JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
                            new Claim (ClaimTypes.NameIdentifier , user.Id),
                            new Claim (ClaimTypes.Role, roles.FirstOrDefault()),
                            new Claim ("LoggedOn", DateTime.Now.ToString())

                        }),
                    SigningCredentials =
                    new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                    Issuer = appSettings.Site,
                    Audience = appSettings.Audience,
                    Expires = DateTime.UtcNow.AddMinutes(tokenExpiryTime)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok(new
                {
                    token = tokenHandler.WriteToken(token),
                    expiration = token.ValidTo,
                    userName = user.UserName,
                    userRole = roles.FirstOrDefault()
                });
            }
            ModelState.AddModelError("", "User Name / Password was not found.");
            //new
            //{
            //    LogInError = "Please check the login credentials - Invalid user / password."
            //}
            return Unauthorized();
        }
    }
}
