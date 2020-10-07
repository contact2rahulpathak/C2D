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
using WebApiIdentity.Interface;
using WebApiIdentity.Models;

namespace WebApiIdentity.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _singInManager;
        private readonly ApplicationSettings _appSettings;
        private ITokenService _tokenservice;
        public ApplicationUserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<ApplicationSettings> appSettings, ITokenService tokenservice)
        {
            _userManager = userManager;
            _singInManager = signInManager;
            _appSettings = appSettings.Value;
            _tokenservice = tokenservice;
        }

        [HttpPost]
        [Route("Register")]
        //POST : /api/ApplicationUser/Register
        public async Task<Object> PostApplicationUser(ApplicationUserModel model)
        {
            model.Role = "Admin";
            var applicationUser = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName
            };

            try
            {
                var result = await _userManager.CreateAsync(applicationUser, model.Password);
                await _userManager.AddToRoleAsync(applicationUser, model.Role);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost]
        [Route("Login")]
        //POST : /api/ApplicationUser/Login
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                //for Role
                var role = await _userManager.GetRolesAsync(user);
                IdentityOptions _options = new IdentityOptions();

                //var tokenDescriptor = new SecurityTokenDescriptor
                //{
                //    Subject = new ClaimsIdentity(new Claim[]
                //    {
                //        new Claim("UserID",user.Id.ToString()),
                //        new Claim(_options.ClaimsIdentity.RoleClaimType,role.FirstOrDefault())
                //    }),
                //    Expires = DateTime.UtcNow.AddDays(1),
                //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                //};
                //var tokenHandler = new JwtSecurityTokenHandler();
                //var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                //var token = tokenHandler.WriteToken(securityToken);

                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Id.ToString()),
            new Claim(ClaimTypes.Role, role.FirstOrDefault())
        };

                var token = _tokenservice.GenerateAccessToken(claims);
                var refreshToken = _tokenservice.GenerateRefreshToken();

                 user.RefreshToken = refreshToken;
                 user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                return Ok(new { token, refreshToken });
               // return Ok(new { token });
            }
            else
                return BadRequest(new { message = "Username or password is incorrect." });
        }


    [HttpPost]
    [Route("Loginrt")]
    //POST : /api/ApplicationUser/Loginrt
    public async Task<IActionResult> Loginrt(LoginModel model)
    {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
              //  for Role
                var role = await _userManager.GetRolesAsync(user);
                IdentityOptions _options = new IdentityOptions();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID",user.Id.ToString()),
                        new Claim(_options.ClaimsIdentity.RoleClaimType,role.FirstOrDefault())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                //var user = await _userManager.FindByNameAsync(model.UserName);
                //if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                //{
                //var role = await _userManager.GetRolesAsync(user);


                //    var claims = new List<Claim>
                //{   
                //   new Claim(ClaimTypes.Name, user.UserName.ToString()),
                //   new Claim(ClaimTypes.Role, role.FirstOrDefault())
                //  };


                //  var token = _tokenservice.GenerateAccessToken(claims);
                //  var refreshToken = _tokenservice.GenerateRefreshToken();

                // user.RefreshToken = refreshToken;
                // user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                // return Ok(new { token, refreshToken });
                 return Ok(new { token });
            }
        else
            return BadRequest(new { message = "Username or password is incorrect." });
    }
}
}