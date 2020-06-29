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
            CreateMap<Post, PostResponseDto>();
        }
    }
}
