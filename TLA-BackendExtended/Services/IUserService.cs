using TLA_BackendExtended.Models;

namespace TLA_BackendExtended.Services
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(string username, string password, int age, int weight, string location);
        Task<User> GetUserAsync(string username);
        Task<User> UpdateUserAsync(string username, string password, int age, int weight, string location);
        Task<User> UpdateColourModeAsync(string username, bool colourMode);
        Task DeleteUserAsync(string username);
        Task<List<User>> GetAllUsersAsync();
    }
}
