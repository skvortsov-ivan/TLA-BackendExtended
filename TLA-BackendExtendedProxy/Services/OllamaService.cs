using OllamaSharp;
using System.Text;
using TLA_BackendExtendedProxy.DTOs;
using TLA_BackendExtendedProxy.Services;

public class OllamaService : IOllamaService
{
    private readonly IOllamaApiClient _ollama;

    public OllamaService(IOllamaApiClient ollama)
    {
        _ollama = ollama;
    }

    public async Task<OllamaResponseDto> PromptOllamaAsync(OllamaRequestDto request)
    {
        _ollama.SelectedModel = "gemma3:4b";

        string fullResponse = "";

        await foreach (var stream in _ollama.GenerateAsync(request.Prompt))
        {
            fullResponse += stream.Response;
        }

        return new OllamaResponseDto
        {
            GeneratedText = fullResponse
        };
    }
}
