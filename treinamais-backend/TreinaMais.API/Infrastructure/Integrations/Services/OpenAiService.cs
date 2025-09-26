using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using EducaDev.API.Infrastructure.Integrations.Interfaces;
using Microsoft.Extensions.Options;

namespace EducaDev.API.Infrastructure.Integrations.Services
{
    public class OpenAiService : IOpenAiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly OpenAiConfigurations _openAiConfigurations;

        public OpenAiService(IHttpClientFactory httpClientFactory, IOptions<OpenAiConfigurations> options)
        {
            _httpClientFactory = httpClientFactory;
            _openAiConfigurations = options.Value;
        }

        public async Task<string> GenerateTextAsync(string prompt)
        {
            var client = CreateClient();

            var payload = new
            {
                input = prompt,
                model = _openAiConfigurations.TextModel
            };

            var json = JsonSerializer.Serialize(payload);

            var response = await client.PostAsync(
                _openAiConfigurations.TextUrl,
                new StringContent(json, Encoding.UTF8, "application/json")
            );

            var responseText = await response.Content.ReadAsStringAsync();
            var responseApiResponse = JsonSerializer.Deserialize<ResponsesApiResponse>(responseText);
            var text = responseApiResponse?.output?.First(o => o.type == "message").content?.First().text;

            return text ?? string.Empty;
        }

        public async Task<ModerationsResultItem> ModerateTextAsync(string text)
        {
            var client = CreateClient();

            var payload = new
            {
                input = text
            };

            var json = JsonSerializer.Serialize(payload);

            var response = await client.PostAsync(
                _openAiConfigurations.ModerationUrl,
                new StringContent(json, Encoding.UTF8, "application/json")
            );

            var responseText = await response.Content.ReadAsStringAsync();
            var moderationsApiResponse = JsonSerializer.Deserialize<ModerationsApiResponse>(responseText);

            return moderationsApiResponse.results.First();
        }

        public async Task<ModerationsResultItem> ModerateImageAsync(string base64Image)
        {
            var client = CreateClient();

            var payload = new ModerationImageRequest
            {
                model = "omni-moderation-latest",
                input = new[]
                {
                    new Input
                    {
                        type = "image_url",
                        image_url = new Image_url()
                        {
                            url = base64Image
                        }
                    }
                }
            };

            var json = JsonSerializer.Serialize(payload);

            var response = await client.PostAsync(
                _openAiConfigurations.ModerationUrl,
                new StringContent(json, Encoding.UTF8, "application/json")
            );

            var responseText = await response.Content.ReadAsStringAsync();
            var moderationsApiResponse = JsonSerializer.Deserialize<ModerationsApiResponse>(responseText);

            return moderationsApiResponse.results.First();
        }

        private HttpClient CreateClient()
        {
            var client = _httpClientFactory.CreateClient("openai");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _openAiConfigurations.ApiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}