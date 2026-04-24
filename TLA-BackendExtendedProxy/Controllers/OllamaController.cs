using Microsoft.AspNetCore.Mvc;
using TLA_BackendExtendedProxy.DTOs;
using TLA_BackendExtendedProxy.Services;

[ApiController]
[Route("api/ai-requests")]
public class OllamaController : ControllerBase
{
    private readonly IOllamaService _ollamaService;

    public OllamaController(IOllamaService ollamaService)
    {
        _ollamaService = ollamaService;
    }

    [HttpPost]
    public async Task<IActionResult> PromptOllama([FromBody] OllamaRequestDto request)
    {
        Console.WriteLine($"[Controller] Received prompt length: {request?.Prompt?.Length ?? 0}");
        Console.WriteLine($"[Controller] Prompt preview: {request?.Prompt?.Substring(0, Math.Min(200, request?.Prompt?.Length ?? 0))}");
        var response = await _ollamaService.PromptOllamaAsync(request);
        return Ok(response);
    }
}
