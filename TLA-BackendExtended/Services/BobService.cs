using TLA_BackendExtended.Clients;
using TLA_BackendExtended.DTOs;
using TLA_BackendExtended.Models;

namespace TLA_BackendExtended.Services
{
    public class BobService : IBobService
    {
        private readonly IAiBobClient _client;
        private static readonly List<BobContent> _aiContentList = new();
        private static int _nextId = 1;

        public BobService(IAiBobClient client)
        {
            _client = client;
        }

        public async Task<BobResponseDto> CreateAsync(string prompt)
        {
            string instructions = "You are Bob, a helpful assistant, present yourself and be happy and really helpful, also you know extra much about timers and clocks and can help a lot with stuff like that, also you know how study mode works with pomodoro in our digital timer app also you know about workouts and how exercise mode works in our timer app, and also you can just help with random stuff. You are also speaking to a client on the digital timer app so be respectful and helpful";

            string finalPrompt = $"INSTRUCTIONS: {instructions}. RESPOND TO THE FOLLOWING PROMPT: {prompt}";

            var proxyRequest = new BobRequestDto
            {
                Prompt = finalPrompt
            };

            var bobResponse = await _client.SendPromptAsync(proxyRequest);

            var model = new BobContent
            {
                Id = _nextId++,
                Prompt = proxyRequest.Prompt,
                GeneratedText = bobResponse.GeneratedText,
                CreatedAt = DateTime.Now
            };

            _aiContentList.Add(model);

            return new BobResponseDto
            {
                Prompt = model.Prompt,
                GeneratedText = model.GeneratedText,
                CreatedAt = model.CreatedAt
            };
        }
    }
}
