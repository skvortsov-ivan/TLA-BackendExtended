using System.ComponentModel.DataAnnotations;

namespace TLA_BackendExtended.DTOs
{
    // Request for creating a user
    public record CreateUserRequest(
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Username must be between 6 and 50 characters.")]
        string Username,

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 50 characters.")]
        string Password,

        [Required(ErrorMessage = "Age is required.")]
        [Range(0, 120, ErrorMessage = "Age must be between 0 and 120 [years].")]
        int Age,

        [Required(ErrorMessage = "Weight is required.")]
        [Range(2, 635, ErrorMessage = "Weight must be between 2 and 635 [kg].")]
        int Weight,

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Location must be between 2 and 100 characters.")]
        string Location,

        [Required(ErrorMessage = "Light/Dark mode is required.")]
        bool DarkMode
    );

    // Request for viewing user information
    public record UserInformationRequest(
    );

    // Response for viewing user information
    public record UserInformationResponse(
        string Username,
        string Password,
        int Age,
        int Weight,
        string Location
    );

    // V2 Response for viewing user information
    public record UserInformationResponseV2
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public int Age { get; set; }
        public int Weight { get; set; }
        public string Location { get; set; } = string.Empty;
        
        // V2
        public string DisplayName { get; set; } = string.Empty;
    }


    // Request for updating user information
    public record UpdateUserRequest(
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Username must be between 6 and 50 characters.")]
        string Username,

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 50 characters.")]
        string Password,

        [Required(ErrorMessage = "Age is required.")]
        [Range(0, 120, ErrorMessage = "Age must be between 0 and 120 [years].")]
        int Age,

        [Required(ErrorMessage = "Weight is required.")]
        [Range(2, 635, ErrorMessage = "Weight must be between 2 and 635 [kg].")]
        int Weight,

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Location must be between 2 and 100 characters.")]
        string Location
    );

    // Request for deleting user
    public record DeleteUserRequest(

    );

    // Request for updating Colour Mode for specific user
    public record UpdateColourModeRequest(
        [Required(ErrorMessage = "Light/Dark mode is required.")]
        bool DarkMode
    );
}
