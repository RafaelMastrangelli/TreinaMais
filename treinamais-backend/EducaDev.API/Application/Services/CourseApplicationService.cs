using EducaDev.API.Application.DTOs;
using EducaDev.API.Application.Services.Interfaces;
using EducaDev.API.Core.Entities;
using EducaDev.API.Infrastructure.Integrations.Interfaces;
using EducaDev.API.Infrastructure.Persistence.Repositories.Interfaces;
using EducaDev.API.Models;

namespace EducaDev.API.Application.Services
{
    public class CourseApplicationService : ICourseApplicationService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IOpenAiService _openAiService;
        private readonly ILeonardoAiService _leonardoAiService;

        public CourseApplicationService(
            ICourseRepository courseRepository,
            IOpenAiService openAiService,
            ILeonardoAiService leonardoAiService)
        {
            _courseRepository = courseRepository;
            _openAiService = openAiService;
            _leonardoAiService = leonardoAiService;
        }

        public async Task<ServiceResult<IEnumerable<CourseDto>>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllWithReviewsAsync();
            var courseDtos = courses.Select(c =>
            {
                var validReviews = c.Reviews.Where(r => r.Nota >= 1 && r.Nota <= 5).ToList();
                var averageRating = validReviews.Any()
                    ? Math.Round(validReviews.Average(r => r.Nota), 1)
                    : (double?)null;

                return new CourseDto
                {
                    Id = c.Id,
                    NomeCurso = c.NomeCurso,
                    Instrutor = c.Instrutor,
                    Valor = c.Valor,
                    DescricaoDetalhada = c.DescricaoDetalhada,
                    Resumo = c.Resumo,
                    ImagemBytes = c.ImagemBytes,
                    CoverUrl = c.CoverUrl,
                    QuantidadeReview = c.Reviews.Count,
                    AverageRating = averageRating,
                    CreatedAtUtc = c.CreatedAtUtc,
                    UpdatedAtUtc = c.UpdatedAtUtc,
                    Reviews = c.Reviews.Select(r => new ReviewDto
                    {
                        Id = r.Id,
                        CourseId = r.CourseId,
                        Nota = r.Nota,
                        Descricao = r.Descricao,
                        Sentimento = r.Sentimento,
                        SentimentScore = r.SentimentScore,
                        ModerationLabel = r.ModerationLabel,
                        Status = r.Status,
                        CreatedAtUtc = r.CreatedAtUtc,
                        ModeratedAtUtc = r.ModeratedAtUtc
                    }).ToList()
                };
            }).ToList();

            return ServiceResult<IEnumerable<CourseDto>>.Success(courseDtos);
        }

        public async Task<ServiceResult<CourseDto>> GetCourseByIdAsync(int id)
        {
            var course = await _courseRepository.GetByIdWithReviewsAsync(id);
            if (course is null)
                return ServiceResult<CourseDto>.Failure("Curso não encontrado.");

            var validReviews = course.Reviews.Where(r => r.Nota >= 1 && r.Nota <= 5).ToList();
            var averageRating = validReviews.Any()
                ? Math.Round(validReviews.Average(r => r.Nota), 1)
                : (double?)null;

            var courseDto = new CourseDto
            {
                Id = course.Id,
                NomeCurso = course.NomeCurso,
                Instrutor = course.Instrutor,
                Valor = course.Valor,
                DescricaoDetalhada = course.DescricaoDetalhada,
                Resumo = course.Resumo,
                ImagemBytes = course.ImagemBytes,
                CoverUrl = course.CoverUrl,
                QuantidadeReview = course.Reviews.Count,
                AverageRating = averageRating,
                CreatedAtUtc = course.CreatedAtUtc,
                UpdatedAtUtc = course.UpdatedAtUtc,
                Reviews = course.Reviews.Select(r => new ReviewDto
                {
                    Id = r.Id,
                    CourseId = r.CourseId,
                    Nota = r.Nota,
                    Descricao = r.Descricao,
                    Sentimento = r.Sentimento,
                    SentimentScore = r.SentimentScore,
                    ModerationLabel = r.ModerationLabel,
                    Status = r.Status,
                    CreatedAtUtc = r.CreatedAtUtc,
                    ModeratedAtUtc = r.ModeratedAtUtc
                }).ToList()
            };

            return ServiceResult<CourseDto>.Success(courseDto);
        }

        public async Task<ServiceResult<CourseResultDto>> CreateCourseAsync(AddOrUpdateCourseModel input)
        {
            var course = new Course(input.NomeCurso, input.Instrutor, input.Valor, input.DescricaoDetalhada);

            course.Resumo = await _openAiService.GenerateTextAsync("Resuma o seguinte texto: " + course.DescricaoDetalhada);

            if (!string.IsNullOrWhiteSpace(input.ImagemBytes))
            {
                var imageData = input.ImagemBytes;

                if (imageData.StartsWith("data:"))
                {
                    // Por exemplo, data:image/jpeg;base64,dshai2edhaw...
                    var base64Data = imageData.Split(",")[1];
                    var bytes = Convert.FromBase64String(base64Data);
                    course.ImagemBytes = bytes;
                }
                else
                {
                    var bytes = Convert.FromBase64String(imageData);
                    course.ImagemBytes = bytes;
                }
            }
            else
            {
                var prompt = $"Create a modern, professional course cover image for {course.NomeCurso}, this image shoudl represent the course topic {course.DescricaoDetalhada}. Focus on vibrant colors, depth, and a clean, innovative atmosphere without any text.";

                var generationId = await _leonardoAiService.CreateGenerationAsync(prompt);

                var imageUrls = await _leonardoAiService.WaitForGenerationCompletionAsync(generationId);

                course.CoverUrl = imageUrls.First();
            }
            
            // var generationId = await _leonardoAiService.CreateGenerationAsync(
            //     "Create a modern, futuristic scene with abstract geometric shapes, glowing technological elements, and a developer working at a sleek computer. Focus on vibrant colors, depth, and a clean, innovative atmosphere without any text.");

            // await Task.Delay(10_000);

            // var imageUrls = await _leonardoAiService.GetGenerationImagesAsync(generationId);
            // course.CoverUrl = imageUrls.First();

            var createdCourse = await _courseRepository.AddAsync(course);

            var resultDto = new CourseResultDto
            {
                Id = createdCourse.Id,
                NomeCurso = createdCourse.NomeCurso,
                Instrutor = createdCourse.Instrutor,
                Valor = createdCourse.Valor,
                DescricaoDetalhada = createdCourse.DescricaoDetalhada,
                Resumo = createdCourse.Resumo,
                ImagemBytes = createdCourse.ImagemBytes,
                CoverUrl = createdCourse.CoverUrl,
                QuantidadeReview = 0, // New course has no reviews
                AverageRating = null, // New course has no rating
                CreatedAtUtc = createdCourse.CreatedAtUtc,
                UpdatedAtUtc = createdCourse.UpdatedAtUtc
            };

            return ServiceResult<CourseResultDto>.Success(resultDto);
        }

        public async Task<ServiceResult<CourseResultDto>> UpdateCourseAsync(int id, AddOrUpdateCourseModel input)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course is null) 
                return ServiceResult<CourseResultDto>.Failure("Curso não encontrado.");

            var descriptionChanged = course.DescricaoDetalhada != input.DescricaoDetalhada;

            course.NomeCurso = input.NomeCurso;
            course.Instrutor = input.Instrutor;
            course.Valor = input.Valor;
            course.DescricaoDetalhada = input.DescricaoDetalhada;

            if (descriptionChanged)
            {
                course.Resumo = await _openAiService.GenerateTextAsync("Resuma o seguinte texto: " + course.DescricaoDetalhada);
            }

            await _courseRepository.UpdateAsync(course);

            // Need to get reviews for accurate counts and rating
            var courseWithReviews = await _courseRepository.GetByIdWithReviewsAsync(id);
            var validReviews = courseWithReviews?.Reviews.Where(r => r.Nota >= 1 && r.Nota <= 5).ToList() ?? new List<Core.Entities.Review>();
            var averageRating = validReviews.Any()
                ? Math.Round(validReviews.Average(r => r.Nota), 1)
                : (double?)null;

            var resultDto = new CourseResultDto
            {
                Id = course.Id,
                NomeCurso = course.NomeCurso,
                Instrutor = course.Instrutor,
                Valor = course.Valor,
                DescricaoDetalhada = course.DescricaoDetalhada,
                Resumo = course.Resumo,
                ImagemBytes = course.ImagemBytes,
                CoverUrl = course.CoverUrl,
                QuantidadeReview = courseWithReviews?.Reviews.Count ?? 0,
                AverageRating = averageRating,
                CreatedAtUtc = course.CreatedAtUtc,
                UpdatedAtUtc = course.UpdatedAtUtc
            };

            return ServiceResult<CourseResultDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteCourseAsync(int id)
        {
            var deleted = await _courseRepository.TryDeleteAsync(id);
            if (!deleted) 
                return ServiceResult.Failure("Curso não encontrado.");

            return ServiceResult.Success();
        }

        public async Task<ServiceResult<ImageUploadResultDto>> UploadCourseImageAsync(int id, IFormFile file)
        {
            var course = await _courseRepository.GetByIdForImageUploadAsync(id);
            if (course is null) 
                return ServiceResult<ImageUploadResultDto>.Failure("Curso não encontrado.");

            if (file is null || file.Length == 0) 
                return ServiceResult<ImageUploadResultDto>.Failure("Arquivo inválido.");

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var bytes = ms.ToArray();

            var base64UrlString = $"data:image/jpeg;base64,{Convert.ToBase64String(bytes)}";
            var result = await _openAiService.ModerateImageAsync(base64UrlString);

            if (result.flagged)
            {
                return ServiceResult<ImageUploadResultDto>.Failure("Imagem contém conteúdo inapropriado.");
            }

            course.ImagemBytes = bytes;
            await _courseRepository.UpdateAsync(course);

            var resultDto = new ImageUploadResultDto
            {
                Message = "Imagem recebida e armazenada (pendente de moderação de conteúdo).",
                Success = true
            };

            return ServiceResult<ImageUploadResultDto>.Success(resultDto);
        }
    }
}