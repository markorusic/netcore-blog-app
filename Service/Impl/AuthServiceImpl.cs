using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Dao;
using Domain;
using Dto.Request;
using Dto.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Common;

namespace Service.Impl
{
    public class AuthServiceImpl : IAuthService
    {

        private readonly IMapper _mapper;

        private readonly AppDb _db;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMailService _mailService;

        private readonly IConfiguration _configuration;


        public AuthServiceImpl(
            IMapper mapper,
            AppDb db,
            IHttpContextAccessor httpContextAccessor,
            IMailService mailService,
            IConfiguration configuration
        )
        {
            _mapper = mapper;
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _mailService = mailService;
            _configuration = configuration;
        }

        public AuthResponseDto Authenticate(AuthRequestDto request)
        {
            var user = _db.Users.FirstOrDefault((x => x.Email == request.Email));

            if (user == null)
            {
                throw new ResourceNotFoundException("User");
            }

            if (user.Password != request.Password)
            {
                _mailService.Send(user.Email, "Failed login attempt", "");
                throw new HttpException(HttpStatusCode.Unauthorized, "Wrong credentials");
            }

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthResponseDto
            {
                Token = token,
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
            var key = Encoding.ASCII.GetBytes(_configuration["Security:Secret"]);
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