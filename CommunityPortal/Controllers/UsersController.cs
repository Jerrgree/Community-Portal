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
        public async Task<IActionResult> Register([FromBody] UserRequest request)
        {
            var hash = SHA256.Create();
            var password = Encoding.ASCII.GetString(hash.ComputeHash(Encoding.ASCII.GetBytes(request.Password)));

            var userExists = context.Users
                .Where(u => u.UserName == request.UserName)
                .Count() > 0;

            if (userExists)
            {
                return BadRequest
                    (
                        String.Format("User Name \"{0}\" is already taken", request.UserName)
                    );
            }

            context.Users.Add(new User()
            {
                UserName = request.UserName,
                Password = password,
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

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var hash = SHA256.Create();
            var password = Encoding.ASCII.GetString(hash.ComputeHash(Encoding.ASCII.GetBytes(request.CurrentPassword)));

            User user;

            try
            {
                user = context.Users
                        .Where(u => u.UserName == request.UserName && u.Password == password)
                        .Select(r => r)
                        .Single();
            }
            catch (Exception)
            {
                return BadRequest();
            }


            user.Password = Encoding.ASCII.GetString(hash.ComputeHash(Encoding.ASCII.GetBytes(request.NewPassword)));

            context.Users.Update(user);

            var result = await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("Authenticate")]
        public bool Authenticate([FromBody] UserRequest request)
        {
            var hash = SHA256.Create();
            var password = Encoding.ASCII.GetString(hash.ComputeHash(Encoding.ASCII.GetBytes(request.Password)));

            return context.Users
                .Where(u => u.UserName == request.UserName && u.Password == password && u.IsActive == true)
                .Count() > 0;
        }
    }
}