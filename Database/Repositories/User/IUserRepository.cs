using Database.Entities;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}
