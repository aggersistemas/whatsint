using Infrastructure.Repository;
using System.Security.Claims;
using WhatsInt.Infrastructure.Entities;
using WhatsInt.Interface.Exceptions;
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

        public async Task<UserDto?> Created(UserDto user)
        {
            var userFound = await FindUserByEmail(user.Email);

            if (userFound != null) throw new AppException(System.Net.HttpStatusCode.Conflict, "User already created");

            var userDb = MapperHelper.Map<User>(user);

            await _userRepository.Add(userDb);

            return MapperHelper.Map<UserDto?>(userDb);
        }

        internal async Task<UserDto?> FindUserById(string id)
        {
            var userFound = await _userRepository.FindOne(x => x.Id == id);

            if (userFound == null) 
                throw new AppException(System.Net.HttpStatusCode.NotFound, "User not found");

            return MapperHelper.Map<UserDto?>(userFound);
        }

        public async Task<UserDto?> FindUserByEmail(string email)
        {
            var userFound = await _userRepository.FindOne(x => x.Email == email);

            return MapperHelper.Map<UserDto?>(userFound);
        }

        public async Task<UserDto?> Update(UserDto user)
        {
            var clientId = _context?.HttpContext?.User.Claims.First(c => c.Type == ClaimTypes.Name).Value ?? string.Empty;            

            var findUser = await FindUserById(clientId);

            if (findUser == null) throw new AppException(System.Net.HttpStatusCode.Unauthorized, "Login error");

            var userUpdate = MapperHelper.Map<User>(user);

            await _userRepository.Update(userUpdate);

            return MapperHelper.Map<UserDto?>(userUpdate);
        }
    }
}
