namespace EducaDev.API.Application.DTOs
{
    public class CourseResultDto
    {
        public int Id { get; set; }
        public string NomeCurso { get; set; } = string.Empty;
        public string Instrutor { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public string DescricaoDetalhada { get; set; } = string.Empty;
        public string? Resumo { get; set; }
        public byte[]? ImagemBytes { get; set; }
        public string? CoverUrl { get; set; }
        public int QuantidadeReview { get; set; }
        public double? AverageRating { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
    }
}