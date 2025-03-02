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

        public User? GetUser(int id)
        {
            return _context.Users.SingleOrDefault(x => x.Id == id);
        }

        public IQueryable<User> GetUsers()
        {
            return _context.Users;
        }

        public void PostUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentException(nameof(user));
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

            var userModel = new User
            {
                Email = user.Email,
                Username = user.Username,
                Password = passwordHash,
                Role = User.RoleType.User,
            };

            _context.Users.Add(userModel);
            _context.SaveChanges();
        }

        public bool DeleteUser(int id)
        {
            var user = _context.Users.SingleOrDefault(x => x.Id == id);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            _context.Remove(user);
            _context.SaveChanges();

            return true;
        }

        public void UpdateUser(int id, User updateUser)
        {
            var user = _context.Users.SingleOrDefault(x => x.Id == id);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            user.Email = updateUser.Email;
            user.Username = updateUser.Username;
            user.Password = updateUser.Password;
            user.Role = updateUser.Role;

            _context.SaveChanges();
        }
    }
}
