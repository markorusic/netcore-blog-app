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
using Microsoft.IdentityModel.Tokens;
using ResourceException;

namespace Service.Impl
{
    public class UserServiceImpl : IUserService
    {

        private readonly IMapper _mapper;

        private readonly AppDb _db;


        public UserServiceImpl(IMapper mapper, AppDb db)
        {
            _mapper = mapper;
            _db = db;
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

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("supertajnasifra");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}