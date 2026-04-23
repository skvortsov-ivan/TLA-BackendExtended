using System.ComponentModel.DataAnnotations;

namespace TLA_BackendExtended.DTOs
{
    public record BobRequestDto
    {
        [Required(ErrorMessage = "Prompt is required.")]
        [StringLength(5000, MinimumLength = 1, ErrorMessage = "Prompt must be between 1 and 5000 characters.")]
        public string Prompt { get; init; } = string.Empty;
    }


    public record BobResponseDto
    {
        public string Prompt { get; init; } = string.Empty;
        public string GeneratedText { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }
    }
}

