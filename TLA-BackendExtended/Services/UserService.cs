using TLA_BackendExtended.Models;

namespace TLA_BackendExtended.Services
{
    public class UserService : IUserService
    {
        private static readonly List<User> _users = new();

        public async Task<User> CreateUserAsync(string username, string password, int age, int weight, string location, bool darkMode)
        {
            // Check if user exists
            if (_users.Any(u => u.Username.Equals(username)))
                throw new Exception($"User '{username}' already exists.");

            var user = new User
            {
                Username = username,
                Password = password,
                Age = age,
                Weight = weight,
                Location = location,
                DarkMode = darkMode
            };

            _users.Add(user);
            return Task.FromResult(user);
        }
    }
}
