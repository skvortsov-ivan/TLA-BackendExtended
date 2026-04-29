using System.ComponentModel.DataAnnotations;

namespace TLA_BackendExtended.DTOs
{
    public record BobRequestDto
    {
        /// <summary>
        /// The input prompt that will be sent for text generation.
        /// </summary>
        [Required(ErrorMessage = "Prompt is required.")]
        [StringLength(5000, MinimumLength = 1, ErrorMessage = "Prompt must be between 1 and 5000 characters.")]
        public string Prompt { get; init; } = string.Empty;
    }

    /// <summary>
    /// Data Transfer Object returned from the Bob API after generating text.
    /// </summary>
    public record BobResponseDto
    {
        /// <summary>
        /// The original prompt that was sent by the user.
        /// </summary>
        public string Prompt { get; init; } = string.Empty;

        /// <summary>
        /// The generated text response from the AI/model.
        /// </summary>
        public string GeneratedText { get; init; } = string.Empty;

        /// <summary>
        /// The timestamp indicating when the response was created.
        /// </summary>
        public DateTime CreatedAt { get; init; }
    }
}
