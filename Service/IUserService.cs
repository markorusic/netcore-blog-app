using Dto.Request;
using Dto.Response;

namespace Service
{
    public interface IUserService
    {
        AuthResponseDto Authenticate(AuthRequestDto model);
    }
}