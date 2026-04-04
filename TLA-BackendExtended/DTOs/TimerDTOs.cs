using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace TLA_BackendExtended.DTOs
{
    public record StartTimerRequest(
        [Required(ErrorMessage = "Category is required.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Category must be between 6 and 50 characters.")]
        string Category
    );

    public record EndTimerRequest(
        [Required(ErrorMessage = "Time interval is required.")]
        [Range(0.01, 24, ErrorMessage = "Time interval must be between 0.01 and 24 [hours].")]
        decimal TimeInterval
    );

    public record TimerResponse(
        decimal TimeInterval,
        string Category,
        DateTime CreatedAt
    );

    //double hours = (end - start).TotalHours;
}
