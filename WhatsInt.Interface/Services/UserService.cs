using System.Net;
using Infrastructure.Repository;
using System.Security.Claims;
using WhatsInt.Infrastructure.Entities;
using WhatsInt.Infrastructure.Exceptions;
using WhatsInt.Infrastructure.Helpers;
using WhatsInt.Infrastructure.Resources;
using WhatsInt.Interface.Helpers;
using WhatsInt.Model.Dto;

namespace WhatsInt.Interface.Services
{
    public class UserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IHttpContextAccessor _httpContext;

        public UserService(IRepository<User> userRepository, IHttpContextAccessor httpContext)
        {
            _userRepository = userRepository;

            _httpContext = httpContext;
        }

        public async Task<UserDto> Create(UserDto user)
        {
            var userFound = await FindUserByEmail(user.Email);

            if (userFound != null) throw new AppException(HttpStatusCode.Conflict, Messages.UserCreated);

            var userDomain = User.CreateOrUpdate(user.Name, user.Email, user.Password);

            await _userRepository.Add(userDomain);

            var userDto = MapperHelper.Map<UserDto>(userDomain);

            userDto.Password = userDto.Password.Decrypt()?.Base64Encode();

            return userDto;
        }

        internal async Task<UserDto> FindUserById(string id)
        {
            var userFound = await FindUser(id);

            userFound!.Password = userFound.Password!.Decrypt()?.Base64Encode();

            return MapperHelper.Map<UserDto>(userFound);
        }

        private async Task<User> FindUser(string id)
        {
            var userFound = await _userRepository.FindOne(x => x.Id == id);

            if (userFound == null)  throw new AppException(HttpStatusCode.NotFound, Messages.UserNotFound);

            return userFound;
        }

        public async Task<UserDto?> FindUserByEmail(string? email)
        {
            var userFound = await _userRepository.FindOne(x => x.Email == email);

            return MapperHelper.Map<UserDto?>(userFound);
        }

        public async Task<UserDto> Update(UserDto user)
        {
            var clientId = _httpContext.HttpContext?.User.Claims.First(c => c.Type == ClaimTypes.Name).Value ?? string.Empty;

            var userDomain = await FindUser(clientId);

            var userVerified = User.CreateOrUpdate(user.Name, user.Email, user.Password, userDomain.Id);

            await _userRepository.Update(userVerified);

            var userDto = MapperHelper.Map<UserDto>(userDomain);

            userDto.Password = userDto.Password.Decrypt()?.Base64Encode();

            return userDto;
        }
    }
}
