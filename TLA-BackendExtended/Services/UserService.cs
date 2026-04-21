using System.Runtime.InteropServices;
using TLA_BackendExtended.Exceptions;
using TLA_BackendExtended.Models;

namespace TLA_BackendExtended.Services
{
    public class UserService : IUserService
    {
        private static readonly List<User> _users = new();
        private static int _nextId = 1;

        // Create a user
        public Task<User> CreateUserAsync(string username, string password, int age, int weight, string location, bool darkMode)
        {
            if (_users.Any(u => u.Username.Equals(username)))
                throw new UserAlreadyExistsException($"User '{username}' already exists.");

            var user = new User
            {
                Id = _nextId++,
                Username = username,
                Password = password,
                Age = age,
                Weight = weight,
                Location = location,
                DarkMode = darkMode,
                CreatedAt = DateTime.Now
            };

            _users.Add(user);
            return Task.FromResult(user);
        }

        // Find a user by Id
        public Task<User> GetUserByIdAsync(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);

            if (user == null)
                throw new UserNotFoundException($"User with ID: '{id}' not found.");

            return Task.FromResult(user);
        }

        // Find a user by Username
        public Task<User> GetUserByUsernameAsync(string username)
        {
            var user = _users.FirstOrDefault(u => u.Username.Equals(username));

            if (user == null)
                throw new UserNotFoundException($"User with Username:'{username}' not found.");

            return Task.FromResult(user);
        }

        // Update user info
        public Task<User> UpdateUserAsync(string username, string password, int age, int weight, string location)
        {
            var user = _users.FirstOrDefault(u => u.Username.Equals(username));

            if (user == null)
                throw new UserNotFoundException($"User '{username}' not found.");

            user.Password = password;
            user.Age = age;
            user.Weight = weight;
            user.Location = location;

            return Task.FromResult(user);
        }

        // Update dark mode
        public Task<User> UpdateColourModeAsync(string username, bool darkMode)
        {
            var user = _users.FirstOrDefault(u => u.Username.Equals(username));

            if (user == null)
                throw new UserNotFoundException($"User '{username}' not found.");

            user.DarkMode = darkMode;
            return Task.FromResult(user);
        }

        // Delete a user
        public Task DeleteUserAsync(string username)
        {
            var user = _users.FirstOrDefault(u => u.Username.Equals(username));

            if (user == null)
                throw new UserNotFoundException($"User '{username}' not found.");

            _users.Remove(user);
            return Task.CompletedTask;
        }

        // Return all users
        public Task<List<User>> GetAllUsersAsync()
        {
            return Task.FromResult(_users.ToList());
        }
    }
}
