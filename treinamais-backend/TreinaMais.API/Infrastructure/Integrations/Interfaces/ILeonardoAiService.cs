namespace EducaDev.API.Infrastructure.Integrations.Interfaces
{
    public interface ILeonardoAiService
    {
        Task<string> CreateGenerationAsync(string prompt);
        Task<List<string>> GetGenerationImagesAsync(string generationId);
        Task<List<string>> WaitForGenerationCompletionAsync(string generationId, int maxWaitTimeSeconds = 60);
    }
}