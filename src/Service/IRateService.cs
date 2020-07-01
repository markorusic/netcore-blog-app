using Dao.Utils;
using Dto.Request;
using Dto.Response;
using Dto.Search;

namespace Service
{
    public interface IRateService
    {
        public Page<RateResponseDto> FindAll(int postId, PageableUserSearchDto request);

        public RateResponseDto Create(int postId, RateRequestDto request);

        public RateResponseDto Update(int postId, RateRequestDto request);

        public void Delete(int postId);
    }
}
