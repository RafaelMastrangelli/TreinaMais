namespace EducaDev.API.Infrastructure.Integrations
{
    public class LeonardoAiConfigurations
    {
        public string ApiKey { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = "https://cloud.leonardo.ai/";
        public string DefaultModelId { get; set; } = "de7d3faf-762f-48e0-b3b7-9d0ac3a3fcf3";
        public string DefaultStyleUUID { get; set; } = "111dc692-d470-4eec-b791-3475abac4c46";
    }
}