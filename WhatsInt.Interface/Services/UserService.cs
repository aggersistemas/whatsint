using Infrastructure.Repository;
using WhasInt.Infrastructure.Entities;
using Whatsint.Model;

namespace WhatsInt.Interface.Services
{
    public class UserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<object?> Login(UserDto user)
        {
            var userFound = await _userRepository.FindOne(x => x.Email == user.Email);

            return userFound?.Password == user.Password ? new { Id = userFound.Id, Name = userFound.Name } : null;    
        }
    }
}
