using EducaDev.API.Application.DTOs;
using EducaDev.API.Application.Services.Interfaces;
using EducaDev.API.Core.Entities;
using EducaDev.API.Core.Enums;
using EducaDev.API.Infrastructure.Integrations.Interfaces;
using EducaDev.API.Infrastructure.Persistence.Repositories.Interfaces;
using EducaDev.API.Models;

namespace EducaDev.API.Application.Services
{
    public class ReviewApplicationService : IReviewApplicationService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IOpenAiService _openAiService;
        private readonly ISentimentAnalysisService _sentimentAnalysisService;

        public ReviewApplicationService(
            IReviewRepository reviewRepository,
            IOpenAiService openAiService,
            ISentimentAnalysisService sentimentAnalysisService)
        {
            _reviewRepository = reviewRepository;
            _openAiService = openAiService;
            _sentimentAnalysisService = sentimentAnalysisService;
        }

        public async Task<ServiceResult<IEnumerable<ReviewResultDto>>> GetReviewsByCourseAsync(int courseId)
        {
            var exists = await _reviewRepository.CourseExistsAsync(courseId);
            if (!exists) 
                return ServiceResult<IEnumerable<ReviewResultDto>>.Failure("Curso não encontrado.");

            var items = await _reviewRepository.GetByCourseIdAsync(courseId);
            var reviewDtos = items.Select(r => new ReviewResultDto
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
            }).ToList();
            
            return ServiceResult<IEnumerable<ReviewResultDto>>.Success(reviewDtos);
        }

        public async Task<ServiceResult<ReviewResultDto>> GetReviewByIdAsync(int courseId, int id)
        {
            var review = await _reviewRepository.GetByIdAndCourseIdAsync(courseId, id);
            if (review is null) 
                return ServiceResult<ReviewResultDto>.Failure("Review não encontrado.");
            
            var reviewDto = new ReviewResultDto
            {
                Id = review.Id,
                CourseId = review.CourseId,
                Nota = review.Nota,
                Descricao = review.Descricao,
                Sentimento = review.Sentimento,
                SentimentScore = review.SentimentScore,
                ModerationLabel = review.ModerationLabel,
                Status = review.Status,
                CreatedAtUtc = review.CreatedAtUtc,
                ModeratedAtUtc = review.ModeratedAtUtc
            };
            
            return ServiceResult<ReviewResultDto>.Success(reviewDto);
        }

        public async Task<ServiceResult<ReviewResultDto>> CreateReviewAsync(int courseId, AddReviewModel input)
        {
            var course = await _reviewRepository.GetCourseAsync(courseId);
            if (course is null) 
                return ServiceResult<ReviewResultDto>.Failure("Curso não encontrado.");

            var review = new Review(input.Nota, input.Descricao, courseId);

            var moderationResult = await _openAiService.ModerateTextAsync(input.Descricao);
            review.Status = moderationResult.flagged ? ReviewStatus.Rejected : ReviewStatus.Approved;

            var sentimentResult = await _sentimentAnalysisService.AnalyzeAsync(input.Descricao);
            review.Sentimento = sentimentResult.Sentiment;
            review.SentimentScore = sentimentResult.Score;

            var createdReview = await _reviewRepository.AddAsync(review);

            var reviewDto = new ReviewResultDto
            {
                Id = createdReview.Id,
                CourseId = createdReview.CourseId,
                Nota = createdReview.Nota,
                Descricao = createdReview.Descricao,
                Sentimento = createdReview.Sentimento,
                SentimentScore = createdReview.SentimentScore,
                ModerationLabel = createdReview.ModerationLabel,
                Status = createdReview.Status,
                CreatedAtUtc = createdReview.CreatedAtUtc,
                ModeratedAtUtc = createdReview.ModeratedAtUtc
            };

            return ServiceResult<ReviewResultDto>.Success(reviewDto);
        }

        public async Task<ServiceResult> DeleteReviewAsync(int courseId, int id)
        {
            var deleted = await _reviewRepository.TryDeleteAsync(courseId, id);
            if (!deleted) 
                return ServiceResult.Failure("Review não encontrado.");
            
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<ReviewResultDto>> ApproveReviewAsync(int courseId, int id)
        {
            await _reviewRepository.ApproveAsync(courseId, id);
            var review = await _reviewRepository.GetByIdAndCourseIdAsync(courseId, id);
            if (review is null) 
                return ServiceResult<ReviewResultDto>.Failure("Review não encontrado.");
            
            var reviewDto = new ReviewResultDto
            {
                Id = review.Id,
                CourseId = review.CourseId,
                Nota = review.Nota,
                Descricao = review.Descricao,
                Sentimento = review.Sentimento,
                SentimentScore = review.SentimentScore,
                ModerationLabel = review.ModerationLabel,
                Status = review.Status,
                CreatedAtUtc = review.CreatedAtUtc,
                ModeratedAtUtc = review.ModeratedAtUtc
            };
            
            return ServiceResult<ReviewResultDto>.Success(reviewDto);
        }

        public async Task<ServiceResult<ReviewResultDto>> RejectReviewAsync(int courseId, int id, string? reason = null)
        {
            await _reviewRepository.RejectAsync(courseId, id, reason);
            var review = await _reviewRepository.GetByIdAndCourseIdAsync(courseId, id);
            if (review is null) 
                return ServiceResult<ReviewResultDto>.Failure("Review não encontrado.");
            
            var reviewDto = new ReviewResultDto
            {
                Id = review.Id,
                CourseId = review.CourseId,
                Nota = review.Nota,
                Descricao = review.Descricao,
                Sentimento = review.Sentimento,
                SentimentScore = review.SentimentScore,
                ModerationLabel = review.ModerationLabel,
                Status = review.Status,
                CreatedAtUtc = review.CreatedAtUtc,
                ModeratedAtUtc = review.ModeratedAtUtc
            };
            
            return ServiceResult<ReviewResultDto>.Success(reviewDto);
        }
    }
}