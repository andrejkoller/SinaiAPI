using SinaiAPI.Models;

namespace SinaiAPI.Services
{
    public class UserService
    {
        private readonly SinaiDbContext _context;

        public UserService(SinaiDbContext context)
        {
            _context = context;
        }

        public IQueryable<User> GetUsers()
        {
            return _context.Users;
        }

        public Models.User? GetUser(int id)
        {
            return _context.Users.SingleOrDefault(x => x.Id == id);
        }
    }
}
