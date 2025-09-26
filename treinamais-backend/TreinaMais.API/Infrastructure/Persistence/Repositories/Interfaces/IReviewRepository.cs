using EducaDev.API.Core.Entities;
using EducaDev.API.Core.Enums;

namespace EducaDev.API.Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetByCourseIdAsync(int courseId);
        Task<Review?> GetByIdAndCourseIdAsync(int courseId, int reviewId);
        Task<bool> CourseExistsAsync(int courseId);
        Task<Course?> GetCourseAsync(int courseId);
        
        Task<Review> AddAsync(Review review);
        Task UpdateReviewStatusAsync(int courseId, int reviewId, ReviewStatus status, string? reason = null);
        Task DeleteAsync(int courseId, int reviewId);
        Task<bool> TryDeleteAsync(int courseId, int reviewId);
        
        Task<IEnumerable<Review>> GetPendingModerationAsync();
        Task<IEnumerable<Review>> GetBySentimentAsync(string sentiment);
        Task ApproveAsync(int courseId, int reviewId);
        Task RejectAsync(int courseId, int reviewId, string? reason = null);
        
        Task SaveChangesAsync();
    }
}