using CoreWebAPI.Models;
using CoreWebAPI.Services;
using CoreWebAPI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections;
using CoreWebAPI.Filters;
using CoreWebAPI.Helpers;

namespace CoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        CoreDBContext db = new CoreDBContext();

        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        // [HttpPost("[action]")]
        public IActionResult Authenticate([FromBody]vmUser userParam)
        {
           // var user = _userService.Authenticate(userParam["username"].ToString(), userParam["password"].ToString());
            var user = _userService.Authenticate(userParam.UserName, userParam.Password );

            if (user == null)
                return Unauthorized();
            //  return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult GetAll()
        {
            var idClaim = User.Claims.FirstOrDefault(x => x.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));
            if (idClaim != null)
            {
                return Ok($"This is your Id: {idClaim.Value}");
            }
            var users = _userService.GetAll();
            return Ok(users);
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult GetAllPublic()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }


        [HttpGet("api/user")]
        public IActionResult Get()
        {
            return Ok(db.SecUsers.ToList());
        }


        #region Added By Kashan

       [AuthenicationFilter]
       [HttpPost("GetUsers")]
        public async Task<IActionResult> GetUsers([FromBody]vmUser userParam)
        {
            List<SecUsers> users = new List<SecUsers>();
            try
            {
                users = await _userService.GetUsers(userParam);
                if (users == null)
                {
                    return NotFound();
                }
                return Ok(users);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Some Error Acquired: ", ex.StackTrace);
            }

            return BadRequest();
        }


       
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser(int? userId)
        {
            if (userId == null)
            {
                return BadRequest();
            }

            try
            {
                var user = await _userService.GetUser(userId);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody]SecUsers model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Channel = "website";
                    model.Status = 1;
                    model.CreatedOn = DateFormatter.ConvertStringToDate( DateTime.Now.ToString("dd/MM/yyyy"));
                    var userId = await _userService.CreateUser(model);
                    if (userId > 0)
                    {
                        return Ok(userId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Some Error AAcquired: " + ex.StackTrace);
                    return BadRequest();
                }
            }

            return BadRequest();
        }

        public static DateTime ConvertStringToDate(string date)
        {
            string[] dateParts = date.Split('/');
            int day = Convert.ToInt32(dateParts[0]);
            int month = Convert.ToInt32(dateParts[1]);
            int year = Convert.ToInt32(dateParts[2]);

            DateTime currentDate = new DateTime(year, month, day);
           
            return currentDate;
        }


        [HttpPost("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int? userId)
        {
            int result = 0;

            if (userId == null)
            {
                return BadRequest();
            }

            try
            {
                result = await _userService.DeleteUser(userId);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }


        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody]SecUsers model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.UpdatedOn = UsersController.ConvertStringToDate(DateTime.Now.ToString("dd/MM/yyyy"));
                    await _userService.UpdateUser(model);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }
    }

    #endregion
}
