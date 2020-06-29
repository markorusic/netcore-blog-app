using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dao;
using Dao.Utils;
using Domain;
using Dto.Request;
using Dto.Response;
using Dto.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostController : ControllerBase
    {

        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public Page<PostResponseDto> Get([FromQuery] PostSearchRequestDto request)
        {
            return _postService.FindAll(request);
        }

        [HttpPost]
        public PostResponseDto Post([FromBody] PostRequestDto request)
        {
            return _postService.Create(request);
        }
    }
}
