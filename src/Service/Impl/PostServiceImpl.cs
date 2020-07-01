using AutoMapper;
using Dao;
using Dao.Utils;
using Domain;
using Dto.Request;
using Dto.Response;
using Dto.Search;
using Microsoft.EntityFrameworkCore;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Impl
{
    public class PostServiceImpl : IPostService
    {

        private readonly IMapper _mapper;

        private readonly AppDb _db;

        private readonly IAuthService _userService;

        private readonly IUserActivity _userActivityService;


        public PostServiceImpl(IMapper mapper, AppDb db, IAuthService userService, IUserActivity userActivityService)
        {
            _mapper = mapper;
            _db = db;
            _userService = userService;
            _userActivityService = userActivityService;
        }

        public PostResponseDto Create(PostRequestDto request)
        {
            var userId = _userService.GetCurrentUserId();
            var categories = _db.Categories
                .Where(category => request.Categories.Contains(category.Id))
                .Select(category => new PostCategory { Category = category })
                .ToList();
            var post = new Post
            {
                Title = request.Title,
                Description = request.Description,
                Content = request.Content,
                MainPhoto = request.MainPhoto,
                Categories = categories,
                Photos = request.Photos.Select(src => new Photo { Src = src }).ToList(),
                UserId = userId
            };
            _db.Posts.Add(post);
            _db.SaveChanges();

            _userActivityService.Track($"Created post: {post.Title}");

            return _mapper.Map<PostResponseDto>(post);
        }

        public void Delete(int id)
        {
            var userId = _userService.GetCurrentUserId();
            var post = _db.Posts.FirstOrDefault(post => post.Id == id);
            if (post == null)
            {
                throw new ResourceNotFoundException("Post");
            }
            if (post.UserId != userId)
            {
                throw new ForbiddenActionException();
            }
            _db.Posts.Remove(post);
            _db.SaveChanges();

            _userActivityService.Track($"Deleted post({post.Id}): {post.Title}");
        }

        public Page<PostResponseDto> FindAll(PostSearchRequestDto request)
        {
            var query = _db.Posts.AsQueryable();

            if (request.Title != null)
            {
                query = query.Where(post => post.Title.ToLower().Contains(request.Title.ToLower()));
            }

            if (request.CategoryId != null)
            {
                query = query
                    .Include(post => post.Categories)
                    .Where(post => post.Categories
                        .Any(category => category.CategoryId == request.CategoryId)
                     );
            }

            return query
                .Select(post => _mapper.Map<PostResponseDto>(post))
                .GetPaged(request.Page, request.Size);
        }

        public PostResponseDto FindById(int id)
        {
            var post = _db.Posts.FirstOrDefault(post => post.Id == id);
            if (post == null)
            {
                throw new ResourceNotFoundException("Post");
            }
            return _mapper.Map<PostResponseDto>(post);
        }

        public PostResponseDto Update(int id, PostRequestDto request)
        {
            var userId = _userService.GetCurrentUserId();
            var post = _db.Posts.FirstOrDefault(post => post.Id == id);

            if (post == null)
            {
                throw new ResourceNotFoundException("Post");
            };
            if (post.UserId != userId)
            {
                throw new ForbiddenActionException();
            }

            var categories = _db.Categories
                .Where(category => request.Categories.Contains(category.Id))
                .Select(category => new PostCategory { Category = category })
                .ToList();

            post.Photos.Clear();

            post.Title = request.Title;
            post.Description = request.Description;
            post.Content = request.Content;
            post.MainPhoto = request.MainPhoto;
            post.Categories = categories;
            post.Photos = request.Photos.Select(src => new Photo { Src = src }).ToList();

            _db.SaveChanges();

            _userActivityService.Track($"Updated post({post.Id}): {post.Title}");

            return _mapper.Map<PostResponseDto>(post);
        }
    }
}
