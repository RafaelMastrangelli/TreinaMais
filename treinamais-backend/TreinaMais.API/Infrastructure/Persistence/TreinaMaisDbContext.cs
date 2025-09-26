using EducaDev.API.Core.Entities;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;

namespace EducaDev.API.Infrastructure.Persistence
{
    public class TreinaMaisContext : DbContext
    {
        public TreinaMaisContext(DbContextOptions<TreinaMaisContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(e =>
            {
                e.Property(p => p.NomeCurso).IsRequired().HasMaxLength(160);
                e.Property(p => p.Instrutor).IsRequired().HasMaxLength(120);
                e.Property(p => p.Valor).HasPrecision(18, 2);
                e.Property(p => p.DescricaoDetalhada).IsRequired();
                e.Property(p => p.Resumo).HasMaxLength(4000);
                e.Property(p => p.ImagemBytes);
            });

            modelBuilder.Entity<Review>(e =>
            {
                e.Property(p => p.Nota).IsRequired();
                e.Property(p => p.Descricao).IsRequired().HasMaxLength(4000);
                e.Property(p => p.Sentimento).HasMaxLength(64);
                e.Property(p => p.ModerationLabel).HasMaxLength(64);
                e.Property(p => p.Status).IsRequired();

                e.HasOne(r => r.Course)
                 .WithMany(c => c.Reviews)
                 .HasForeignKey(r => r.CourseId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
