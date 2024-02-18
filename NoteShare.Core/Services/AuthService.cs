using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NoteShare.Data.Entities;
using NoteShare.Models.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace NoteShare.Core.Services
{
    public interface IAuthService
    {
        Task Register(RegisterDto registerDto);
        Task<AuthResponseDto> Login(LoginDto loginDto);
        Task<User?> GetUser();
    }

    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;

        public AuthService(IConfiguration configuration, IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContext, IMapper mapper)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _httpContext = httpContext;
            _mapper = mapper;
        }

        public async Task Register(RegisterDto registerDto)
        {
            if (await _unitOfWork.GetDbSet<User>().AnyAsync(u => u.Email.ToUpper().Equals(registerDto.Email.ToUpper()) || u.UserName.ToUpper().Equals(registerDto.UserName.ToUpper())))
            {
                throw new Exception("A felhasználó már létezik");
            }
            switch (registerDto.UserType)
            {
                case UserType.Teacher:
                    var teacher = _mapper.Map<Teacher>(registerDto);
                    if (!await _unitOfWork.GetRepository<Student>().Exists(teacher.SchoolId))
                    {
                        throw new Exception("Az iskola nem található");
                    }
                    await CreateUser(teacher, registerDto.Password);
                    break;
                case UserType.Student:
                    var student = _mapper.Map<Student>(registerDto);
                    if (!await _unitOfWork.GetRepository<Student>().Exists(student.SchoolId))
                    {
                        throw new Exception("Az iskola nem található");
                    }
                    await CreateUser(student, registerDto.Password);
                    break;
                case UserType.Parent:
                    var parent = _mapper.Map<Parent>(registerDto);
                    await CreateUser(parent, registerDto.Password);
                    break;
                default:
                    throw new Exception("Nem megfelelő felhasználói típus");
            }
        }

        public async Task<AuthResponseDto> Login(LoginDto loginDto)
        {
            var user = await _unitOfWork.GetDbSet<User>().FirstOrDefaultAsync(u => u.Email.ToUpper().Equals(loginDto.Email.ToUpper())) ?? throw new Exception("A felhasználó nem található");

            bool isValidUser = VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt);

            if (user == null || isValidUser == false)
            {
                throw new Exception($"A felhasználó nem található");
            }
            var token = await GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                UserName = user.UserName,
                UserType = user.UserType
            };
        }

        private async Task<string> GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("uid",user.Id),
                new Claim("userType", user.UserType.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(Convert.ToInt32(_configuration["Jwt:DurationInDays"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<User?> GetUser()
        {
            /*
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            string authorization = httpContext.HttpContext.Request.Headers["Authorization"];
            if (authorization == null)
            {
                return null;
            }
            string token = "";
            if (authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = authorization["Bearer ".Length..].Trim();
                if (string.IsNullOrWhiteSpace(token))
                {
                    return null;
                }
            }
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(token);
            var userId = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == "uid")?.Value;
            */
            var userId = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userId))
            {
                return null;
            }
            var user = await _unitOfWork.GetRepository<User>().GetByIdAsync(userId);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private async Task CreateUser<TUser>(TUser user, string password) where TUser : User
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _ = await _unitOfWork.GetRepository<TUser>().AddAsync(user) ?? throw new Exception("A felhasználó regisztrációja sikertelen");
        }
    }
}
