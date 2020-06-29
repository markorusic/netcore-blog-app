using AutoMapper;
using Dao;
using Dao.Utils;
using Domain;
using Dto.Request;
using Dto.Response;
using Dto.Search;
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
            var photos = request.Photos
                .Select(src => new PostPhoto { Src = src })
                .ToList();
            var categories = _db.Categories
                .Where(category => request.Categories.Contains(category.Id))
                .Select(category => new PostCategory { Category = category })
                .ToList();
            var post = new Post {
                Title = request.Title,
                Description = request.Description,
                Content = request.Content,
                MainPhoto = request.MainPhoto,
                Categories = categories,
                Photos = photos,
                UserId = 1
            };
            _db.Posts.Add(post);
            _db.SaveChanges();
            return _mapper.Map<PostResponseDto>(post);
        }

        public void Delete(int id)
        {
            var post = _db.Posts.Find(id);
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
            var post = _db.Posts.Find(id);
            return _mapper.Map<PostResponseDto>(post);
        }

        public PostResponseDto Update(PostRequestDto request)
        {
            throw new NotImplementedException();
        }
    }
}
