using System.Net;
using Infrastructure.Repository;
using System.Security.Claims;
using WhatsInt.Common.Helpers;
using WhatsInt.Infrastructure.Entities;
using WhatsInt.Infrastructure.Exceptions;
using WhatsInt.Interface.Helpers;
using WhatsInt.Model;

namespace WhatsInt.Interface.Services
{
    public class UserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IHttpContextAccessor _context;

        public UserService(IRepository<User> userRepository, IHttpContextAccessor context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public async Task<UserDto> Created(UserDto user)
        {
            var userFound = await FindUserByEmail(user.Email);

            if (userFound != null) throw new AppException(HttpStatusCode.Conflict, "User already created");

            var userDomain = User.CreateOrUpdate(user.Name, user.Email, user.Password);

            userDomain.Password = user.Password?.Encrypt();

            await _userRepository.Add(userDomain);

            userDomain.Password = userDomain.Password!.DecryptToBase64();

            return MapperHelper.Map<UserDto>(userDomain);
        }

        internal async Task<UserDto> FindUserById(string id)
        {
            var userFound = await FindUser(id);

            userFound!.Password = userFound.Password!.DecryptToBase64();

            return MapperHelper.Map<UserDto>(userFound);
        }

        private async Task<User> FindUser(string id)
        {
            var userFound = await _userRepository.FindOne(x => x.Id == id);

            if (userFound == null)
                throw new AppException(HttpStatusCode.NotFound, "User not found");

            return userFound;
        }

        public async Task<UserDto?> FindUserByEmail(string? email)
        {
            var userFound = await _userRepository.FindOne(x => x.Email == email);

            return MapperHelper.Map<UserDto?>(userFound);
        }

        public async Task<UserDto> Update(UserDto user)
        {
            var clientId = _context?.HttpContext?.User.Claims.First(c => c.Type == ClaimTypes.Name).Value ?? string.Empty;

            var userDomain = await FindUser(clientId);

            var userVerified = User.CreateOrUpdate(user.Name, user.Email, user.Password, userDomain.Id);

            userDomain.Password = user.Password?.Encrypt();

            await _userRepository.Update(userVerified);

            userVerified.Password = userVerified.Password!.DecryptToBase64();

            return MapperHelper.Map<UserDto>(userVerified);
        }
    }
}
