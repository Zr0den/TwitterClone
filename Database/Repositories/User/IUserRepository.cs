using Database.Entities;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<User>> SearchAsync(string query);
    }
}
