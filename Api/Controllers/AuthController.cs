using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public AuthController(IAuthService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public AuthResponseDto Post([FromBody] AuthRequestDto request)
        {
            return _userService.Authenticate(request);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("/admin-test")]
        public IActionResult TestAdmin()
        {
            return Ok("admin");
        }

        [Authorize(Roles = Role.User)]
        [HttpGet("/user-test")]
        public IActionResult TestUser()
        {
            return Ok("user");
        }
    }
}
