using AutoMapper;
using Domain;
using Dto.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<User, UserResponseDto>();
            CreateMap<Post, PostResponseDto>();
            CreateMap<Comment, CommentResponseDto>();
            CreateMap<Rate, RateResponseDto>();
            CreateMap<Category, CategoryResponseDto>();
            CreateMap<Photo, PhotoResponseDto>();
            CreateMap<UserActivity, UserActivityResponseDto>();
        }
    }
}
