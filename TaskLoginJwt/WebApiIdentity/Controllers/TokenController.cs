using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiIdentity.Service;
using WebApiIdentity.Models;
using WebApiIdentity.Interface;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApiIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        //readonly AuthenticationContext _usercontext;
        readonly ITokenService _tokenservice;

        public TokenController(UserManager<ApplicationUser> usercontext, ITokenService tokenservice)
        {
            _userManager = usercontext;//?? throw new argumentnullexception(nameof(usercontext));
            _tokenservice = tokenservice;//?? throw new argumentnullexception(nameof(tokenservice));
        }
        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh(TokenApiModel tokenApiModel)
        {
            if (tokenApiModel is null)
            {
                return BadRequest("Invalid client request");
            }
            //string accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYThhMzllOTUtMGJmOS00OTUyLWFiZDgtNWM5ZWY3ZGFhZDdlIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQ3VzdG9tZXIiLCJleHAiOjE1OTQ1MjEyNTksImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTY2NjMiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjU2NjYzIn0.eYl_ZrHoLR78fM2eyNNIFtcq2TusrwM-yu0xyyK9mZY";
            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;
           // var a = _tokenservice.GetPrincipalFromExpiredToken()
            var principal = _tokenservice.GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity.Name; //this is mapped to the Name claim by default

            //var user = _userManager.Users.SingleOrDefault(u => u.UserName == username);
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == username);

            //if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            if(user == null )
            {
                return BadRequest("Invalid client request");
            }

            var newAccessToken = _tokenservice.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenservice.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            var applicationUser = new ApplicationUser()
            {
                RefreshToken = newRefreshToken,
                RefreshTokenExpiryTime = DateTime.UtcNow
            };
            var result = _userManager.UpdateAsync(applicationUser);
            // userContext.SaveChanges();
            //_userManager.u


           // stackflow ref but not working
           //var store = new UserStore<ApplicationUser>(new DbContext());
           //var manager = new UserManager(store);

            //manager.UpdateAsync(user);
            //var ctx = store.context;
            //ctx.saveChanges();


            return new ObjectResult(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            });
        }

        [HttpPost]
        [Authorize]
        [Route("revoke")]
        public IActionResult Revoke()
        {
            var username = User.Identity.Name;

            var user = _userManager.Users.SingleOrDefault(u => u.UserName == username);
            if (user == null) return BadRequest();

            user.RefreshToken = null;

            var applicationUser = new ApplicationUser()
            {
                RefreshToken = null,
                RefreshTokenExpiryTime = DateTime.Now
            };
            // userContext.SaveChanges();

            var result = _userManager.UpdateAsync(applicationUser);
           // userContext.SaveChanges();

            return NoContent();
        }
        // GET: api/Token
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Token/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Token
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Token/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
