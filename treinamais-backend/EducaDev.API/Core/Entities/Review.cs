using EducaDev.API.Core.Enums;

namespace EducaDev.API.Core.Entities
{
    public class Review
    {
        public Review()
        {
            
        }
        public Review(double nota, string descricao, int courseId)
        {
            Nota = nota;
            Descricao = descricao;
            CourseId = courseId;

            CreatedAtUtc = DateTime.UtcNow;
        }

        public int Id { get; set; }

        public int CourseId { get; set; }
        public Course? Course { get; set; }

        public double Nota { get; set; }

        public string Descricao { get; set; } = string.Empty;

        // Campos para analytics/moderação
        public string? Sentimento { get; set; }            // e.g., "positive", "neutral", "negative"
        public double? SentimentScore { get; set; }        // e.g., -1..+1
        public string? ModerationLabel { get; set; }       // e.g., "clean", "profanity", "hate", ...
        public ReviewStatus Status { get; set; } = ReviewStatus.Approved;

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? ModeratedAtUtc { get; set; } = DateTime.UtcNow;
    }
}
