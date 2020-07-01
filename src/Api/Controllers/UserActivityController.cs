using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dao.Utils;
using Dto.Response;
using Dto.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/user-activity")]
    public class UserActivityController : ControllerBase
    {
        private readonly IUserActivityService _userActivityService;

        public UserActivityController(IUserActivityService userActivityService)
        {
            _userActivityService = userActivityService;
        }

        [HttpGet]
        public Page<UserActivityResponseDto> Get([FromQuery] UserActivitySearchDto request)
        {
            return _userActivityService.FindAll(request);
        }
    }
}
