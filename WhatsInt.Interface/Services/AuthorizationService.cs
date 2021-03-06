using Infrastructure.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using WhatsInt.Common.Helpers;
using WhatsInt.Infrastructure.Entities;
using WhatsInt.Infrastructure.Exceptions;
using WhatsInt.Model;
using WhatsInt.Interface.Helpers;

namespace WhatsInt.Interface.Services
{
    public class AuthorizationService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IHttpContextAccessor _context;
        public AuthorizationService(IRepository<User> userRepository, IHttpContextAccessor context)
        {
            _userRepository = userRepository;
            _context = context;
        }
        internal async Task<object?> Authorize()
        {
            var authHeader = _context.HttpContext?.Request.Headers.Authorization.ToString() ?? string.Empty;

            const string basic = "Basic";

            if (!authHeader.Contains(basic)) throw new AppException(HttpStatusCode.Unauthorized);

            var basicToken = authHeader.Replace(basic, string.Empty).Trim().Base64Decode().Split(':');

            var userToken = basicToken[0];

            var passwordToken = basicToken[1];

            var dbUser = await Login(userToken, passwordToken);

            return GenerateToken(dbUser!);
        }

        public async Task<User?> Login(string email, string password)
        {
            var userFound = await _userRepository.FindOne(x => x.Email == email);

            if (userFound == null) throw new AppException(HttpStatusCode.Unauthorized, "User not exist");
            
            if (userFound.Password != password) throw new AppException(HttpStatusCode.Unauthorized, "Invalid password");

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

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claimsIdentity),
                Expires = DateTime.UtcNow.Add(expiresHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(EncryptionHelper.Secret)), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            var generatedToken = tokenHandler.WriteToken(securityToken);

            return new()
            {
                Expires = expiresHours.TotalSeconds,
                Token = generatedToken,
                Type = "Bearer"
            };
        }
    }
}
