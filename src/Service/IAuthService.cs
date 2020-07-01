using Dto.Request;
using Dto.Response;

namespace Service
{
    public interface IAuthService
    {
        AuthResponseDto Authenticate(AuthRequestDto model);

        int GetCurrentUserId();
    }
}