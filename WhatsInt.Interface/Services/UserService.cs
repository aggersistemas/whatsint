using Infrastructure.Repository;
using WhatsInt.Infrastructure.Entities;
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
            try
            {
                var userFound = await FindUserByEmail(user.Email);

                if (userFound != null)
                    return null;

                var userDb = MapperHelper.Map<User>(user);

                await _userRepository.Add(userDb);

                return MapperHelper.Map<UserDto?>(userDb);
            }
            catch (Exception ex)
            {
                return new UserDto();
            }
        }

        public async Task<UserDto?> FindUserByEmail(string email)
        {
            var user = await _userRepository.FindOne(x => x.Email == email);

            return MapperHelper.Map<UserDto?>(user);
        }

        public async Task<User?> GetUser(UserDto user)
        {
            var userFound = await _userRepository.FindOne(x => x.Email == user.Email);

            if (userFound != null)
                return null;

            return userFound?.Password == user.Password ? userFound : null;
        }
    }
}
