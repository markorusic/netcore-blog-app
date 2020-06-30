using AutoMapper;
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
using System.Net;
using System.Text;

namespace Service.Impl
{
    public class RateServiceImpl : IRateService
    {
        private readonly IMapper _mapper;

        private readonly AppDb _db;

        private readonly IAuthService _userService;

        public RateServiceImpl(IMapper mapper, AppDb db, IAuthService userService)
        {
            _mapper = mapper;
            _db = db;
            _userService = userService;
        }

        private Rate GetUserRate(int postId)
        {
            var userId = _userService.GetCurrentUserId();
            var post = _db.Posts.FirstOrDefault(post => post.Id == postId);

            if (post == null)
            {
                throw new ResourceNotFoundException("Post");
            };

            return _db.Entry(post)
                .Collection(p => p.Rates)
                .Query()
                .FirstOrDefault(p => p.UserId == userId);
        }

        public RateResponseDto Create(int postId, RateRequestDto request)
        {
            var userId = _userService.GetCurrentUserId();
            var userRate = GetUserRate(postId);

            if (userRate != null)
            {
                throw new HttpException(HttpStatusCode.UnprocessableEntity, "You have already reated this post.");
            }

            var rate = new Rate
            {
                Value = request.Value,
                PostId = postId,
                UserId = userId
            };
            _db.Rates.Add(rate);
            _db.SaveChanges();

            return _mapper.Map<RateResponseDto>(rate);
        }

        public void Delete(int postId)
        {
            var userRate = GetUserRate(postId);
            if (userRate == null)
            {
                throw new ResourceNotFoundException("Rate");
            }
            _db.Rates.Remove(userRate);
            _db.SaveChanges();
        }

        public Page<RateResponseDto> FindAll(int postId, PageableUserSearchDto request)
        {
            var query = _db.Rates.AsQueryable()
                .Where(rate=> rate.PostId == postId);

            if (request.UserId != null)
            {
                query = query.Where(rate => rate.UserId == request.UserId);
            }

            return query
                .Select(rate => _mapper.Map<RateResponseDto>(rate))
                .GetPaged(request.Page, request.Size);
        }

        public RateResponseDto Update(int postId, RateRequestDto request)
        {
            var userRate = GetUserRate(postId);
            if (userRate == null)
            {
                throw new ResourceNotFoundException("Rate");
            }

            userRate.Value = request.Value;
            _db.SaveChanges();

            return _mapper.Map<RateResponseDto>(userRate);
        }
    }
}
