using TLA_BackendExtended.DTOs;

namespace TLA_BackendExtended.Services
{
    public interface IBobService
    {
        Task<BobResponseDto> CreateAsync(string prompt);
    }
}
