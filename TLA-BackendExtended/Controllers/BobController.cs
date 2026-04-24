using Microsoft.AspNetCore.Mvc;
using TLA_BackendExtended.DTOs;
using TLA_BackendExtended.Services;

namespace TLA_BackendExtended.Controllers
{
    [ApiController]
    [Route("api/ai-bob/prompts")]
    public class BobController : ControllerBase
    {
        private readonly IBobService _service;

        public BobController(IBobService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> SendPrompt([FromQuery] string prompt)
        {
            var result = await _service.CreateAsync(prompt);
            return Ok(result);
        }
    }
}


//using Microsoft.AspNetCore.Mvc;

//namespace TLA_BackendExtended.Controllers
//{
//    [ApiController]
//    [Route("api/ai-bob/prompts")]
//    public class BobController : ControllerBase
//    {
//        private readonly IHttpClientFactory _httpClientFactory;

//        public BobController(IHttpClientFactory httpClientFactory)
//        {
//            _httpClientFactory = httpClientFactory;
//        }

//        [HttpPost]
//        public async Task<IActionResult> SendPromptToAi([FromBody] string prompt, [FromServices] IConfiguration configuration)
//        {

//            string instructions = "You are Bob, a helpful assistant, present yourself and be happy and really helpful, also you know extra much about timers and clocks and can help a lot with stuff like that, also you know how study mode works with pomodoro in our digital timer app also you know about workouts and how exercise mode works in our timer app, and also you can just help with random stuff. You are also speaking to a client on the digital timer app so be respectful and helpful";

//            var client = _httpClientFactory.CreateClient("LLM_Proxy_Client");
//            client.BaseAddress = new Uri("http://localhost:5155/");
//            client.DefaultRequestHeaders.Add("ServiceCommunicationApiKey", configuration["ServiceCommunicationApiKey"]);
//            string text = $"INSTRUCTIONS: {instructions}. RESPOND TO THE FOLLOWING PROMPT: {prompt}";
//            var response = await client.PostAsJsonAsync("api/ai/ask", text);
//            var aiText = await response.Content.ReadAsStringAsync();
//            string aiResponse = $"AI response to: {prompt}\n- - - - - - - -\n{aiText}";
//            return Ok(aiResponse);
//        }
//    }
//}