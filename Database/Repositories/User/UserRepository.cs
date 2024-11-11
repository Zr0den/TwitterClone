using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Database.Entities;

namespace Database.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        Context _context;
        public UserRepository(Context context) : base(context) 
        {
            _context = context;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
