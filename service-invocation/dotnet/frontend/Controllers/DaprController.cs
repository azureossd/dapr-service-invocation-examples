using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace frontend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DaprController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public DaprController(IHttpClientFactory httpClientFactory) =>
        _httpClientFactory = httpClientFactory;

    [HttpGet(Name = "Dapr")]
    public async Task<string> OnGetAsync()
    {
        var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            "http://localhost:8080/api/weatherforecast")
        {
            Headers =
            {
                { HeaderNames.Accept, "application/vnd.github.v3+json" },
                { HeaderNames.UserAgent, "HttpRequestsSample" }
            }
        };

        var httpClient = _httpClientFactory.CreateClient();
        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
        var contentStream =
            await httpResponseMessage.Content.ReadAsStringAsync();
        
        return contentStream;
    }
}
