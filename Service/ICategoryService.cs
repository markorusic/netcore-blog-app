using Dao.Utils;
using Dto.Request;
using Dto.Response;
using Dto.Search;

namespace Service
{
    public interface ICategoryService
    {
        public Page<CategoryResponseDto> FindAll(CategorySearchDto request);

        public CategoryResponseDto FindById(int categoryId);

        public CategoryResponseDto Create(CategoryRequestDto request);

        public CategoryResponseDto Update(int categoryId, CategoryRequestDto request);

        public void Delete(int categoryId);
    }
}
