using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using TLA_BackendExtended.DTOs;
using TLA_BackendExtended.Services;

namespace TLA_BackendExtended.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Creates a new user
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var user = await _userService.CreateUserAsync(
                request.Username,
                request.Password,
                request.Age,
                request.Weight,
                request.Location,
                request.DarkMode
            );

            return Ok(user);
        }
        // Get a user by username
        [HttpGet("by-username/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _userService.GetUserByUsernameAsync(username);

            return Ok(user);
        }

        // Get a user by id V1
        [HttpGet("by-id/{id:int}/V1")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetUserByIdV1(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            return Ok(user);
        }

        // Get a user by id V2
        [HttpGet("by-id/{id:int}/V2")]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> GetUserByIdV2(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            var upgraded = new UserInformationResponseV2
            {
                Id = user.Id,
                Username = user.Username,
                Age = user.Age,
                Weight = user.Weight,
                Location = user.Location,
                DisplayName = $"{user.Username}#{user.Id}"
            };

            return Ok(upgraded);
        }



        // Get all users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // Update user info
        [HttpPut("{username}")]
        public async Task<IActionResult> UpdateUser(string username, [FromBody] UpdateUserRequest request)
        {
            var updated = await _userService.UpdateUserAsync(
                username,
                request.Password,
                request.Age,
                request.Weight,
                request.Location
            );

            return Ok(updated);
        }

        // Update dark mode
        [HttpPut("{username}/darkmode")]
        public async Task<IActionResult> UpdateDarkMode(string username, [FromBody] UpdateColourModeRequest request)
        {
            var updated = await _userService.UpdateColourModeAsync(username, request.DarkMode);
            return Ok(updated);
        }

        // Delete a user
        [Authorize(Roles = "Admin")]
        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteUser(string username)
        {
            await _userService.DeleteUserAsync(username);
            return Ok($"User '{username}' deleted.");
        }

    }
}
