using TLA_BackendExtendedProxy.DTOs;

namespace TLA_BackendExtendedProxy.Services
{
    public interface IOllamaService
    {
        Task<OllamaResponseDto> PromptOllamaAsync(OllamaRequestDto request);
    }

}
