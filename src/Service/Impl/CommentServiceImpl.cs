using System.Linq;
using AutoMapper;
using Dao;
using Dao.Utils;
using Domain;
using Dto.Request;
using Dto.Response;
using Dto.Search;
using Common;

namespace Service.Impl
{
    public class CommentServiceImpl : ICommentService
    {

        private readonly IMapper _mapper;

        private readonly AppDb _db;

        private readonly IAuthService _userService;

        public CommentServiceImpl(IMapper mapper, AppDb db, IAuthService userService)
        {
            _mapper = mapper;
            _db = db;
            _userService = userService;
        }


        public CommentResponseDto Create(int postId, CommentRequestDto request)
        {
            var userId = _userService.GetCurrentUserId();
            var post = _db.Posts.Find(postId);

            if (post == null)
            {
                throw new ResourceNotFoundException("Post");
            }
            var comment = new Comment
            {
                Content = request.Content,
                Post = post,
                UserId = userId
            };
            _db.Comments.Add(comment);
            _db.SaveChanges();

            return _mapper.Map<CommentResponseDto>(comment);
        }

        public void Delete(int id)
        {
            var userId = _userService.GetCurrentUserId();
            var comment = _db.Comments.Find(id);
            if (comment == null)
            {
                throw new ResourceNotFoundException("Comment");
            }
            if (comment.UserId != userId)
            {
                throw new ForbiddenActionException();
            }
            _db.Comments.Remove(comment);
            _db.SaveChanges();
        }

        public Page<CommentResponseDto> FindAll(int postId, PageableUserSearchDto request)
        {
            var query = _db.Comments.AsQueryable()
                .Where(comment => comment.PostId == postId);

            if (request.UserId != null)
            {
                query = query.Where(comment => comment.UserId == request.UserId);
            }

            return query
                .Select(comment => _mapper.Map<CommentResponseDto>(comment))
                .GetPaged(request.Page, request.Size);
        }

        public CommentResponseDto Update(int commentId, CommentRequestDto request)
        {
            var userId = _userService.GetCurrentUserId();
            var comment = _db.Comments.FirstOrDefault(comment => comment.Id == commentId);

            if (comment == null)
            {
                throw new ResourceNotFoundException("Comment");
            };
            if (comment.UserId != userId)
            {
                throw new ForbiddenActionException();
            }

            comment.Content = request.Content;

            _db.SaveChanges();

            return _mapper.Map<CommentResponseDto>(comment);
        }
    }
}