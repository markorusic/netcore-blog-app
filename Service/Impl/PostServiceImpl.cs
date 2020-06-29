using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dao;
using Dao.Utils;
using Domain;
using Dto.Request;
using Dto.Response;
using Dto.Search;
using Microsoft.EntityFrameworkCore;
using ResourceException;
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

        public PostServiceImpl(IMapper mapper, AppDb db)
        {
            _mapper = mapper;
            _db = db;
        }

        public PostResponseDto Create(PostRequestDto request)
        {

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
                UserId = 1
            };
            _db.Posts.Add(post);
            _db.SaveChanges();
            return _mapper.Map<PostResponseDto>(post);
        }

        public void Delete(int id)
        {
            var post = _db.Posts.Find(id);
            if (post == null)
            {
                throw new ResourceNotFoundException("Post");
            }
            _db.Posts.Remove(post);
            _db.SaveChanges();
        }

        public Page<PostResponseDto> FindAll(PostSearchRequestDto request)
        {
            var query = _db.Posts.AsQueryable();

            if (request.Title != null)
            {
                query = query.Where(post => post.Title.ToLower().Contains(request.Title.ToLower()));
            }

            return query
                .Select(post => _mapper.Map<PostResponseDto>(post))
                .GetPaged(request.Page, request.Size);
        }

        public PostResponseDto FindById(int id)
        {
            var post = _db.Posts.First(post => post.Id == id);
            if (post == null)
            {
                throw new ResourceNotFoundException("Post");
            }
            return _mapper.Map<PostResponseDto>(post);
        }

        public PostResponseDto Update(int id, PostRequestDto request)
        {
            var post = _db.Posts.First(post => post.Id == id);

            if (post == null)
            {
                throw new ResourceNotFoundException("Post");
            };

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

            _db.Posts.Update(post);
            _db.SaveChanges();
            return _mapper.Map<PostResponseDto>(post);
        }
    }
}
