using Infrastructure.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WhasInt.Infrastructure.Entities;
using Whatsint.Model;
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
        internal async Task<object> Authorize()
        {
            var authHeader = _context?.HttpContext?.Response.Headers.Authorization.ToString();

            if (!authHeader.Contains("Basic"))
                return null;

            return null;            
        }

        public TokenDto GenerateToken(User credentials)
        {
            var claimsIdentity = new Claim[]
            {
                new(ClaimTypes.Name, credentials.Name),
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

            return new TokenDto
            {
                Expires = expiresHours.TotalSeconds,
                Token = generatedToken,
                Type = "Bearer"
            };
        }
    }
}
