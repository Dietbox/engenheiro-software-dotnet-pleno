using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces.Infra;
using ECommerce.Domain.Interfaces.Repository;
using ECommerce.Domain.Interfaces.Services;
using ECommerce.Domain.Models;

namespace ECommerce.Service.Service
{
    public class UserService : IUserService
    {
        private readonly IAccessManager _accessManager;
        private readonly IGenericRepository<User> _userRepository;

        public UserService(IAccessManager accessManager, IGenericRepository<User> userRepository)
        {
            _accessManager = accessManager;
            _userRepository = userRepository;
        }

        public async Task<bool> CreateUserApp(UserAppRegister userApp)
        {
            var userCreated = await _accessManager.CreateUser(userApp);

            if (userCreated is not null)
            {
                var newUser = new User(userApp.FullName, userApp.BirthDate, userCreated);

                await _userRepository.AddAsync(newUser);

                await _userRepository.SaveAsync();

                return true;
            }

            return false;
        }
    }
}
