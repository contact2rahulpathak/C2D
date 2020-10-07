using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiIdentity.Models;

namespace WebApiIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserProfileController : ControllerBase
    {


        private UserManager<ApplicationUser> _userManager;
        public UserProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        // [Authorize]
       // [Route("UserProfile")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //GET : /api/UserProfile/GetUserProfile
        public async Task<Object> GetUserProfile()
        {
            string userName= User.Claims.First(c => c.Type == "Name").Value;
            var user = await _userManager.FindByIdAsync(userName);
            return new
            {
                user.FullName,
                user.Email,
                user.UserName
            };
        }
        //[HttpGet]
        //[Authorize(Roles = "Admin")]
        //[Route("ForAdmin")]
        //public string GetForAdmin()
        //{
        //    return "Web method for Admin";
        //}

        //[HttpGet]
        //[Authorize(Roles = "Customer")]
        //[Route("ForCustomer")]
        //public string GetCustomer()
        //{
        //    return "Web method for Customer";
        //}

        //[HttpGet]
        //[Authorize(Roles = "Admin,Customer")]
        //[Route("ForAdminOrCustomer")]
        //public string GetForAdminOrCustomer()
        //{
        //    return "Web method for Admin or Customer";
        //}

        [HttpGet, Authorize(Roles = "Manager,Admin, customer")]
        [Route("GetName")]
        public IEnumerable<string> GetName()
        {
            return new string[] { "John Doe", "Jane Doe" };
        }
    }
}