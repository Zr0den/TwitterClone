using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Database.Entities;
using System.Data.Entity;

namespace Database.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        Context _context;
        public UserRepository(Context context) : base(context) 
        {
            _context = context;
        }

        public async Task<List<User>> GetByNameAsync(string name)
        {
            return await _context.Users.Where(x => x.Name == name).ToListAsync();
        }

        public async Task<List<User>> GetByUserTagAsync(string name)
        {
            return await _context.Users.Where(x => x.UserTag == name).ToListAsync();
        }
    }
}
