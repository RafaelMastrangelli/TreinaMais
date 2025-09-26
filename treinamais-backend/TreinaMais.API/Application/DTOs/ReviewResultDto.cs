using EducaDev.API.Core.Enums;

namespace EducaDev.API.Application.DTOs
{
    public class ReviewResultDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public double Nota { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string? Sentimento { get; set; }
        public double? SentimentScore { get; set; }
        public string? ModerationLabel { get; set; }
        public ReviewStatus Status { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModeratedAtUtc { get; set; }
    }
}