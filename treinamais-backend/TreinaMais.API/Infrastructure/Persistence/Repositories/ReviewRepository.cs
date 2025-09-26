using EducaDev.API.Core.Entities;
using EducaDev.API.Core.Enums;
using EducaDev.API.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EducaDev.API.Infrastructure.Persistence.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly TreinaMaisContext _context;

        public ReviewRepository(TreinaMaisContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetByCourseIdAsync(int courseId)
        {
            return await _context.Reviews
                .Where(r => r.CourseId == courseId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Review?> GetByIdAndCourseIdAsync(int courseId, int reviewId)
        {
            return await _context.Reviews
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.CourseId == courseId && r.Id == reviewId);
        }

        public async Task<bool> CourseExistsAsync(int courseId)
        {
            return await _context.Courses.AnyAsync(c => c.Id == courseId);
        }

        public async Task<Course?> GetCourseAsync(int courseId)
        {
            return await _context.Courses.FindAsync(courseId);
        }

        public async Task<Review> AddAsync(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task UpdateReviewStatusAsync(int courseId, int reviewId, ReviewStatus status, string? reason = null)
        {
            var review = await _context.Reviews
                .FirstOrDefaultAsync(r => r.CourseId == courseId && r.Id == reviewId);
            
            if (review != null)
            {
                review.Status = status;
                review.ModeratedAtUtc = DateTime.UtcNow;
                if (!string.IsNullOrEmpty(reason))
                {
                    review.ModerationLabel = reason;
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int courseId, int reviewId)
        {
            var review = await _context.Reviews
                .FirstOrDefaultAsync(r => r.CourseId == courseId && r.Id == reviewId);
            
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> TryDeleteAsync(int courseId, int reviewId)
        {
            var review = await _context.Reviews
                .FirstOrDefaultAsync(r => r.CourseId == courseId && r.Id == reviewId);
            
            if (review == null)
                return false;

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Review>> GetPendingModerationAsync()
        {
            return await _context.Reviews
                .Where(r => r.Status == ReviewStatus.Pending)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetBySentimentAsync(string sentiment)
        {
            return await _context.Reviews
                .Where(r => r.Sentimento == sentiment)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task ApproveAsync(int courseId, int reviewId)
        {
            await UpdateReviewStatusAsync(courseId, reviewId, ReviewStatus.Approved);
        }

        public async Task RejectAsync(int courseId, int reviewId, string? reason = null)
        {
            await UpdateReviewStatusAsync(courseId, reviewId, ReviewStatus.Rejected, reason ?? "policy_violation");
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}