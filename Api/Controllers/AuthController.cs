using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dao;
using Domain;
using Dto.Request;
using Dto.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _userService;

        private readonly AppDb _db;

        public AuthController(IAuthService userService, AppDb db)
        {
            _userService = userService;
            _db = db;
        }

        [HttpPost]
        public AuthResponseDto Post([FromBody] AuthRequestDto request)
        {
            return _userService.Authenticate(request);
        }

        // TODO: remove in prod env
        [HttpGet("seed")]
        public IActionResult Seed()
        {
            _db.Users.AddRange(
                new User
                {
                    Id = 1,
                    Email = "marko@gmail.com",
                    Username = "markorusic",
                    Password = "123456",
                    Role = Domain.Role.User
                },
                new User
                {
                    Id = 2,
                    Email = "marko.admin@gmail.com",
                    Username = "markorusic",
                    Password = "123456",
                    Role = Domain.Role.Admin
                },
                new User
                {
                    Id = 3,
                    Email = "marko2@gmail.com",
                    Username = "markorusic",
                    Password = "123456",
                    Role = Domain.Role.User
                }
            );
            _db.SaveChanges();
            return Ok();
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("admin")]
        public IActionResult TestAdmin()
        {
            return Ok("admin");
        }

        [Authorize(Roles = Role.User)]
        [HttpGet("user")]
        public IActionResult TestUser()
        {
            return Ok("user");
        }
    }
}
