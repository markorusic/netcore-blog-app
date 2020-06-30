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
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("{id}")]
        public PostResponseDto GetOne(int id)
        {
            return _postService.FindById(id);
        }

        [Authorize]
        [HttpPost]
        public PostResponseDto Post([FromBody] PostRequestDto request)
        {
            return _postService.Create(request);
        }

        [Authorize]
        [HttpPut("{id}")]
        public PostResponseDto Put(int id, [FromBody] PostRequestDto request)
        {
            return _postService.Update(id, request);
        }

        //[Authorize]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _postService.Delete(id);
        }
    }
}
