using Infrastructure.Repository;
using WhatsInt.Infrastructure.Entities;
using WhatsInt.Interface.Exceptions;
using WhatsInt.Interface.Helpers;
using WhatsInt.Model;

namespace WhatsInt.Interface.Services
{
    public class UserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
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

            if (userFound == null) throw new AppException(System.Net.HttpStatusCode.NoContent, "User not found");

            return MapperHelper.Map<UserDto?>(userFound);
        }

        public async Task<UserDto?> FindUserByEmail(string email)
        {
            var userFound = await _userRepository.FindOne(x => x.Email == email);

            return MapperHelper.Map<UserDto?>(userFound);
        }

        public async Task<UserDto?> Update(UserDto loggedUser, UserDto user)
        {
            if (await GetUser(loggedUser) == null) throw new AppException(System.Net.HttpStatusCode.Unauthorized, "Login error");

            var userUpdate = MapperHelper.Map<User>(user);

            await _userRepository.Update(userUpdate);

            return MapperHelper.Map<UserDto?>(userUpdate);
        }

        public async Task<UserDto?> GetUser(UserDto user)
        {
            var userFound = await FindUserByEmail(user.Email);

            if (userFound != null)
                return null;

            return userFound?.Password == user.Password ? MapperHelper.Map<UserDto?>(userFound) : null;
        }
    }
}
