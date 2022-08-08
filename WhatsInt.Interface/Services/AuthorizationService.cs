using Infrastructure.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using WhatsInt.Infrastructure.Entities;
using WhatsInt.Infrastructure.Exceptions;
using WhatsInt.Infrastructure.Helpers;
using WhatsInt.Infrastructure.Resources;
using WhatsInt.Model.Dto;

namespace WhatsInt.Interface.Services
{
    public class AuthorizationService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IHttpContextAccessor _httpContext;

        public AuthorizationService(IRepository<User> userRepository, IHttpContextAccessor httpContext)
        {
            _userRepository = userRepository;
            _httpContext = httpContext;
        }

        internal async Task<object?> Authorize()
        {
            var authHeader = _httpContext.HttpContext?.Request.Headers.Authorization.ToString() ?? string.Empty;

            const string basicAuth = "Basic";

            var withoutBasic = !authHeader.Contains(basicAuth);

            if (withoutBasic) throw new AppException(HttpStatusCode.Unauthorized);

            var basicToken = authHeader.Replace(basicAuth, string.Empty).Trim().Base64Decode().Split(':');

            var userToken = basicToken[0];

            var passwordToken = basicToken[1];

            var userDomain = await Login(userToken, passwordToken);

            return GenerateToken(userDomain!);
        }

        public async Task<User?> Login(string email, string password)
        {
            var userFound = await _userRepository.FindOne(x => x.Email == email);

            var invalidUser = userFound == null || userFound.Password!.Decrypt() != password;

            if (invalidUser) throw new AppException(HttpStatusCode.Unauthorized, Messages.InvalidUser);
            
            return userFound;
        }

        public TokenDto GenerateToken(User credentials)
        {
            var claimsIdentity = new Claim[]
            {
                new(ClaimTypes.Name, credentials.Id),
                new(ClaimTypes.Role, "Admin")
            };

            var expiresHours = TimeSpan.FromHours(2);

            var byteKey = Encoding.ASCII.GetBytes(EncryptionHelper.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claimsIdentity),
                Expires = DateTime.UtcNow.Add(expiresHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(byteKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            var generatedToken = tokenHandler.WriteToken(securityToken);

            return new TokenDto
            {
                Expires = expiresHours.TotalSeconds,
                Token = generatedToken,
                Type = "Bearer"
            };
        }
    }
}
