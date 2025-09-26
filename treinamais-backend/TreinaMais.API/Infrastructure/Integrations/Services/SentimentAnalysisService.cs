using System.Globalization;
using EducaDev.API.Infrastructure.Integrations.Interfaces;

namespace EducaDev.API.Infrastructure.Integrations.Services
{
    public class SentimentAnalysisService : ISentimentAnalysisService
    {
        private readonly IOpenAiService _openAiService;

        public SentimentAnalysisService(IOpenAiService openAiService)
        {
            _openAiService = openAiService;
        }

        public async Task<(string Sentiment, double Score)> AnalyzeAsync(string text)
        {
            var systemPrompt = "Classifique o sentimento do texto a seguir em 'positivo', 'neutro', ou 'negativo', " +
                               "e retorne também um score entre -1 e 1 (quanto mais próximo de 1, mais forte o sentimento). Formato de saída: sentimento|score";

            var fullPrompt = $"{systemPrompt}\n\nTexto para análise: {text}";
            
            var response = await _openAiService.GenerateTextAsync(fullPrompt);
            
            var parts = response.Split('|');

            if (parts.Length != 2)
                throw new InvalidOperationException("Invalid sentiment analysis response format");

            var sentiment = parts[0].Trim();

            if (!double.TryParse(parts[1].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out var score))
            {
                throw new InvalidOperationException("Unable to parse sentiment score");
            }

            return (sentiment, score);
        }
    }
}