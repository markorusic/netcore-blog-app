using Dao.Utils;
using Domain;
using Dto.Request;
using Dto.Response;
using Dto.Search;
using System;

namespace Service
{
    public interface ICommentService
    {
        public Page<CommentResponseDto> FindAll(int postId, CommentSearchRequestDto request);

        public CommentResponseDto Create(int postId, CommentRequestDto request);

        public CommentResponseDto Update(int commentId, CommentRequestDto request);

        public void Delete(int id);
    }
}
