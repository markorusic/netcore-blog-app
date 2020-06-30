using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dao.Utils;
using Domain;
using Dto.Request;
using Dto.Response;
using Dto.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class RateController : ControllerBase
    {
        private readonly IRateService _rateService;

        public RateController(IRateService rateService)
        {
            _rateService = rateService;
        }

        [HttpGet("posts/{postId}/rates")]
        public Page<RateResponseDto> Get(int postId, [FromQuery] PageableUserSearchDto request)
        {
            return _rateService.FindAll(postId, request);
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("posts/{postId}/rates")]
        public RateResponseDto Post(int postId, [FromBody] RateRequestDto request)
        {
            return _rateService.Create(postId, request);
        }

        [Authorize(Roles = Role.User)]
        [HttpPut("posts/{postId}/rates")]
        public RateResponseDto Put(int postId, [FromBody] RateRequestDto request)
        {
            return _rateService.Update(postId, request);
        }

        [Authorize(Roles = Role.User)]
        [HttpDelete("posts/{postId}/rates")]
        public void Delete(int postId)
        {
            _rateService.Delete(postId);
        }
    }
}
