using Dao.Utils;
using Domain;
using Dto.Request;
using Dto.Response;
using Dto.Search;
using System;

namespace Service
{
    public interface IPostService
    {
        public Page<PostResponseDto> FindAll(PostSearchRequestDto request);

        public PostResponseDto FindById(int id);

        public PostResponseDto Create(PostRequestDto request);

        public PostResponseDto Update(PostRequestDto request);

        public void Delete(int id);
    }
}
