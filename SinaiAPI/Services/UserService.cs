using SinaiAPI.Data;
using SinaiAPI.Models;

namespace SinaiAPI.Services
{
    public class UserService(SinaiDbContext context)
    {
        public User? GetUser(int id)
        {
            return context.Users.SingleOrDefault(x => x.Id == id);
        }

        public IQueryable<User> GetUsers()
        {
            return context.Users;
        }

        public void PostUser(User user)
        {
            if (user != null)
            {
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

                var userModel = new User
                {
                    Email = user.Email,
                    Username = user.Username,
                    Password = passwordHash,
                    Role = User.RoleType.User,
                };

                context.Users.Add(userModel);
                context.SaveChanges();
            }
            else
            {
                throw new ArgumentException(nameof(user));
            }
        }

        public bool DeleteUser(int id)
        {
            var user = context.Users.SingleOrDefault(x => x.Id == id) 
                ?? throw new KeyNotFoundException("User not found");

            context.Remove(user);
            context.SaveChanges();

            return true;
        }

        public void UpdateUser(int id, User updateUser)
        {
            var user = context.Users.SingleOrDefault(x => x.Id == id)
                ?? throw new KeyNotFoundException($"User with ID {id} not found.");

            user.Email = updateUser.Email;
            user.Username = updateUser.Username;
            user.Password = updateUser.Password;
            user.Role = updateUser.Role;

            context.SaveChanges();
        }
    }
}
