using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApiIdentity.Models;
using WebApiIdentity.Models.Company;

namespace WebApiIdentity.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        AuthenticationContext dbContext;
        //UserManager<IdentityUser> userManager;
        public DepartmentsController(AuthenticationContext dbContext)
        {
            this.dbContext = dbContext;
            //userManager = _userManager;
        }

        //#1 Using Lamda Expression
        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    var Depts = await dbContext.Departments.Include(x=>x.Employees)
        //        .Select(x => new Department
        //        {
        //            Did = x.Did,
        //            DName = x.DName,
        //            Description = x.Description,
        //            Employees = x.Employees.Select(y=>new Employee
        //                        {
        //                            Eid=y.Eid,
        //                            Name=y.Name,
        //                            Gender=y.Gender
        //                        })
        //        })
        //        .ToListAsync();

        //    if (Depts.Count != 0)
        //        return Ok(Depts);
        //    else
        //        return NotFound();
        //}

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var Depts = await dbContext.Departments.Include(x => x.Employees).ToListAsync();

                if (Depts.Count != 0)
                {
                    var jsonResult = JsonConvert.SerializeObject(
                        Depts,
                        Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
                    return Ok(jsonResult);
                }
                else
                    return NotFound();
            }
            catch (Exception E)
            {
                //E.Message
                return StatusCode(500, "Something went wrong! We are working on it" + E.Message);
            }
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Dept = await dbContext.Departments.Where(x => x.Did == id).FirstOrDefaultAsync();
            if (Dept != null)
            {
                return Ok(Dept);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("getByName/{dName}")]
        public async Task<IActionResult> GetByName(string dName)
        {
            var Dept = await dbContext.Departments.Where(x => x.DName == dName).FirstOrDefaultAsync();
            if (Dept != null)
            {
                return Ok(Dept);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("getByIdAndName/{id}/{dName}")]
        public async Task<IActionResult> GetByIdAndName(int id, string dName)
        {
            var Dept = await dbContext.Departments.Where(x => x.Did == id && x.DName == dName).FirstOrDefaultAsync();
            if (Dept != null)
            {
                return Ok(Dept);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Dept = await dbContext.Departments.Where(x => x.Did == id).FirstOrDefaultAsync();
            if (Dept != null)
            {
                dbContext.Remove(Dept);
                await dbContext.SaveChangesAsync();
                return Ok(Dept);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Department D)
        {
            if (ModelState.IsValid)
            {
                var claims = User.Claims.ToList();

                //var id = User.Claims.Where(x => x.Type.Contains("claims/nameidentifier")).FirstOrDefault().Value;
                //var name = User.Claims.Where(x => x.Type.Contains("claims/name")).FirstOrDefault().Value;
                //var user = await userManager.FindByNameAsync(User.Identity.Name);
                D.Id = User.Claims.First().Value;//Logged In UserId
                dbContext.Add(D);
                await dbContext.SaveChangesAsync();
                return CreatedAtAction("Get", new { id = D.Did }, D);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Department D)
        {
            if (ModelState.IsValid)
            {
                var Dept = await dbContext.Departments
                            .Where(x => x.Did == D.Did)
                            .AsNoTracking().FirstOrDefaultAsync();
                if (Dept != null)
                {
                    dbContext.Update(D);
                    await dbContext.SaveChangesAsync();
                    return NoContent();//or Ok(D);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
