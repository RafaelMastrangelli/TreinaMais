using EducaDev.API.Infrastructure.Integrations;

namespace EducaDev.API.Infrastructure.Integrations.Interfaces
{
    public interface IOpenAiService
    {
        Task<string> GenerateTextAsync(string prompt);
        Task<ModerationsResultItem> ModerateTextAsync(string text);
        Task<ModerationsResultItem> ModerateImageAsync(string base64Image);
    }
}