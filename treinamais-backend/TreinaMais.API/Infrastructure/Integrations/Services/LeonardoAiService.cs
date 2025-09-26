using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using EducaDev.API.Infrastructure.Integrations.Interfaces;
using Microsoft.Extensions.Options;

namespace EducaDev.API.Infrastructure.Integrations.Services
{
    public class LeonardoAiService : ILeonardoAiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly LeonardoAiConfigurations _leonardoConfigurations;

        public LeonardoAiService(IHttpClientFactory httpClientFactory, IOptions<LeonardoAiConfigurations> options)
        {
            _httpClientFactory = httpClientFactory;
            _leonardoConfigurations = options.Value;
        }

        public async Task<string> CreateGenerationAsync(string prompt)
        {
            var client = CreateClient();

            var body = new
            {
                modelId = _leonardoConfigurations.DefaultModelId,
                contrast = 3.5,
                prompt = prompt,
                num_images = 3,
                width = 1472,
                height = 832,
                alchemy = false,
                styleUUID = _leonardoConfigurations.DefaultStyleUUID,
                enhancePrompt = false
            };

            var json = JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/rest/v1/generations", content);
            var stringResponse = await response.Content.ReadAsStringAsync();
            
            var leonardoResponse = JsonSerializer.Deserialize<LeonardoGenerationResponse>(stringResponse);
            var generationId = leonardoResponse.sdGenerationJob?.generationId;

            return generationId ?? throw new InvalidOperationException("Failed to create Leonardo generation");
        }

        public async Task<List<string>> GetGenerationImagesAsync(string generationId)
        {
            var client = CreateClient();

            var response = await client.GetAsync($"api/rest/v1/generations/{generationId}");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var imagesGeneration = JsonSerializer.Deserialize<LeonardoGetGenerationResponse>(stringResponse);

            var urls = imagesGeneration.generations_by_pk.generated_images
                .Select(img => img.url)
                .ToList();

            return urls;
        }

        public async Task<List<string>> WaitForGenerationCompletionAsync(string generationId, int maxWaitTimeSeconds = 60)
        {
            var client = CreateClient();
            var attempts = 0;
            var maxAttempts = maxWaitTimeSeconds / 5; // Check every 5 seconds

            while (attempts < maxAttempts)
            {
                try
                {
                    var response = await client.GetAsync($"api/rest/v1/generations/{generationId}");
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    var imagesGeneration = JsonSerializer.Deserialize<LeonardoGetGenerationResponse>(stringResponse);

                    // Check if generation is complete and has images
                    if (imagesGeneration.generations_by_pk.generated_images?.Any() == true)
                    {
                        var urls = imagesGeneration.generations_by_pk.generated_images
                            .Select(img => img.url)
                            .Where(url => !string.IsNullOrEmpty(url))
                            .ToList();

                        if (urls.Any())
                        {
                            return urls;
                        }
                    }

                    // If status indicates failure, stop waiting
                    var status = imagesGeneration.generations_by_pk.status?.ToLower();
                    if (status == "failed" || status == "error")
                    {
                        throw new InvalidOperationException($"Leonardo.AI generation failed with status: {status}");
                    }

                    await Task.Delay(5000); // Wait 5 seconds before next check
                    attempts++;
                }
                catch (Exception ex) when (attempts < maxAttempts - 1)
                {
                    // Log the exception but continue retrying unless it's the last attempt
                    await Task.Delay(5000);
                    attempts++;
                }
            }

            throw new TimeoutException($"Leonardo.AI generation did not complete within {maxWaitTimeSeconds} seconds");
        }

        private HttpClient CreateClient()
        {
            var client = _httpClientFactory.CreateClient("leonardo");

            client.BaseAddress = new Uri(_leonardoConfigurations.BaseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _leonardoConfigurations.ApiKey);

            return client;
        }
    }
}