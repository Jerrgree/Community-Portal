using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityPortal.Models;
using Domain;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace CommunityPortal.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        CommunityContext context;

        public UsersController()
        {
            context = new CommunityContext();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
        {
            var hash = SHA256.Create();
            context.Users.Add(new User()
            {
                UserName = request.UserName,
                Password = Encoding.ASCII.GetString(hash.ComputeHash(Encoding.ASCII.GetBytes(request.Password))),
                Role_ID = 1,
                CreateDateUtc = DateTime.UtcNow
            });

            var response = await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("CheckUserName")]
        public bool CheckUserName([FromBody] string UserName)
        {
            return context.Users
                .Where(u => u.UserName == UserName)
                .Count() > 0;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] CreateUserRequest request)
        {
            return Ok();
        }
    }
}