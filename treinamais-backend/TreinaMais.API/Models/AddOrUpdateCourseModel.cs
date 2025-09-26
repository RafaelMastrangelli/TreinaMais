namespace EducaDev.API.Models
{
    public class AddOrUpdateCourseModel
    {
        public string NomeCurso { get; set; } = string.Empty;

        public string Instrutor { get; set; } = string.Empty;

        public decimal Valor { get; set; }

        public string DescricaoDetalhada { get; set; } = string.Empty;
        
        public string? ImagemBytes { get; set; }
    }
}
