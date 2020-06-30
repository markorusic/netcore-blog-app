using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Dao.Utils;
using Domain;
using Dto.Request;
using Dto.Response;
using Dto.Search;
using Service;

namespace Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class CommentController : ControllerBase
    {

        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("posts/{postId}/comments")]
        public Page<CommentResponseDto> Get(int postId, [FromQuery] PageableUserSearchDto request)
        {
            return _commentService.FindAll(postId, request);
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("posts/{postId}/comments")]
        public CommentResponseDto Post(int postId, [FromBody] CommentRequestDto request)
        {
            return _commentService.Create(postId, request);
        }

        [Authorize(Roles = Role.User)]
        [HttpPut("comments/{id}")]
        public CommentResponseDto Put(int id, [FromBody] CommentRequestDto request)
        {
            return _commentService.Update(id, request);
        }

        [Authorize(Roles = Role.User)]
        [HttpDelete("comments/{id}")]
        public void Delete(int id)
        {
            _commentService.Delete(id);
        }
    }
}
