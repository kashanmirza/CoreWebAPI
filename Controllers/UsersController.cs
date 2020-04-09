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
    }
}
