namespace EducaDev.API.Infrastructure.Integrations.Interfaces
{
    public interface ISentimentAnalysisService
    {
        Task<(string Sentiment, double Score)> AnalyzeAsync(string text);
    }
}