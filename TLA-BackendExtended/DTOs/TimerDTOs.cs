using System.Collections;
using System.ComponentModel.DataAnnotations;
using TimerModel = TLA_BackendExtended.Models.Timer;

namespace TLA_BackendExtended.DTOs
{
    public record StartTimerRequest(
        [Required(ErrorMessage = "Category is required.")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Category must be between 4 and 50 characters.")]
        string Category
    );

    public record EndTimerRequest(
        [Required(ErrorMessage = "Time interval is required.")]
        [Range(0.01, 24, ErrorMessage = "Time interval must be between 0.01 and 24 [hours].")]
        decimal TimeInterval
    );

    public record UpdateTimerRequest(
        [Required(ErrorMessage = "Time interval is required.")]
        [Range(0.01, 24, ErrorMessage = "Time interval must be between 0.01 and 24 [hours].")]
        decimal TimerInterval,

        [Required(ErrorMessage = "Category is required.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Category must be between 6 and 50 characters.")]
        string Category
    );

    public record DeleteTimerRequest(

    );

    public record TimerResponse(
        int Id,
        decimal? TimeInterval,
        string Category,
        DateTime CreatedAt
    );

    public record GetLatestTimerResponse(
        TimerResponse Timer
    );


    public record PaginationMeta(
        int Page,
        int PageSize,
        int TotalPages,
        int TotalCount,
        bool HasNext,
        bool HasPrevious
    );

    public record PagedResponse<T>(
        IEnumerable<T> Data,
        PaginationMeta Pagination
    );
}
