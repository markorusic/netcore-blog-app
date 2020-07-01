using AutoMapper;
using Dao;
using Dao.Utils;
using Domain;
using Dto.Response;
using Dto.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Impl
{
    public class UserActivityImpl : IUserActivity
    {

        private readonly IMapper _mapper;

        private readonly AppDb _db;

        private readonly IAuthService _userService;

        public UserActivityImpl(IMapper mapper, AppDb db, IAuthService userService)
        {
            _mapper = mapper;
            _db = db;
            _userService = userService;
        }

        public Page<UserActivityResponseDto> FindAll(UserActivitySearchDto request)
        {
            var query = _db.UserActivites.AsQueryable();

            if (request.UserId != null)
            {
                query = query.Where(activity => activity.UserId == request.UserId);
            }

            if (request.ActionType != null)
            {
                query = query.Where(activity => activity.ActionType.ToLower().Contains(request.ActionType.ToLower()));
            }

            if (request.CreatedAt != null)
            {
                query = query.Where(activity => activity.CreatedAt.CompareTo(request.CreatedAt) >= 0);
            }

            return query
                .Select(activity => _mapper.Map<UserActivityResponseDto>(activity))
                .GetPaged(request.Page, request.Size);
        }

        public void Track(string actionType)
        {
            var userId = _userService.GetCurrentUserId();
            _db.UserActivites.Add(new UserActivity {
                UserId = userId,
                CreatedAt = DateTime.Now,
                ActionType = actionType
            });
            _db.SaveChanges();
        }
    }
}
