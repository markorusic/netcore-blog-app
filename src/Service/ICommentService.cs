using Dao.Utils;
using Dto.Request;
using Dto.Response;
using Dto.Search;

namespace Service
{
    public interface ICommentService
    {
        public Page<CommentResponseDto> FindAll(int postId, PageableUserSearchDto request);

        public CommentResponseDto Create(int postId, CommentRequestDto request);

        public CommentResponseDto Update(int commentId, CommentRequestDto request);

        public void Delete(int id);
    }
}
