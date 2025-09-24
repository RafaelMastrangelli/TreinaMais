using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.ComponentModel.DataAnnotations;

namespace EducaDev.API.Core.Entities
{
    public class Course
    {
        public Course()
        {
            
        }
        public Course(string nomeCurso, string instrutor, decimal valor, string descricaoDetalhada)
        {
            NomeCurso = nomeCurso;
            Instrutor = instrutor;
            Valor = valor;
            DescricaoDetalhada = descricaoDetalhada;

            CreatedAtUtc = DateTime.UtcNow;
        }

        public int Id { get; set; }

        public string NomeCurso { get; set; } = string.Empty;

        public string Instrutor { get; set; } = string.Empty;

        public decimal Valor { get; set; }

        public string DescricaoDetalhada { get; set; } = string.Empty;

        /// <summary>
        /// Resumo gerado via IA a partir de DescricaoDetalhada.
        /// </summary>
        public string? Resumo { get; set; }

        /// <summary>
        /// Imagem do curso (guardada como bytes). Será moderada via IA antes de salvar.
        /// </summary>
        public byte[]? ImagemBytes { get; set; }
        
        public string? CoverUrl { get; set; }

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAtUtc { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
