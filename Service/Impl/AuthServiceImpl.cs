using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Dao;
using Domain;
using Dto.Request;
using Dto.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using ResourceException;

namespace Service.Impl
{
    public class AuthServiceImpl : IAuthService
    {

        private readonly IMapper _mapper;

        private readonly AppDb _db;

        private readonly IHttpContextAccessor _httpContextAccessor;


        public AuthServiceImpl(
            IMapper mapper,
            AppDb db,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _mapper = mapper;
            _db = db;
            _httpContextAccessor = httpContextAccessor;

        }

        public AuthResponseDto Authenticate(AuthRequestDto request)
        {
            var user = _db.Users.FirstOrDefault((x => x.Email == request.Email && x.Password == request.Password));

            if (user == null)
            {
                throw new ResourceNotFoundException("User");
            }

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthResponseDto
            {
                Token = "token",
                User = _mapper.Map<UserResponseDto>(user)
            };
        }

        public int GetCurrentUserId()
        {
            var id = _httpContextAccessor.HttpContext.User.Identity.Name;
            return int.Parse(id);
        }

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("supertajnasifra");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}