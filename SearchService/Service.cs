using Database.Entities;
using Database.Repositories;
using Helpers;

namespace SearchService
{
    public class Service()
    {
        private readonly IUserRepository _userRepository;
        private readonly MessageClient<UserCreateDto> _newUserClient;

        public UserService(IUserRepository userRepository, MessageClient<UserCreateDto> newUserClient)
        {
            _userRepository = userRepository;
            _newUserClient = newUserClient;
        }

        public void Start()
        {

        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<User>> SearchAsync(string query)
        {
            return await _userRepository.SearchAsync(query);
        }
    }
}
