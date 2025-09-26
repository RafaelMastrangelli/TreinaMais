using EducaDev.API.Core.Entities;

namespace EducaDev.API.Infrastructure.Persistence.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<IEnumerable<Course>> GetAllWithReviewsAsync();
        Task<Course?> GetByIdAsync(int id);
        Task<Course?> GetByIdWithReviewsAsync(int id);
        Task<bool> ExistsAsync(int id);
        
        Task<Course> AddAsync(Course course);
        Task UpdateAsync(Course course);
        Task DeleteAsync(int id);
        Task<bool> TryDeleteAsync(int id);
        
        Task<Course?> GetByIdForImageUploadAsync(int id);
        Task SaveChangesAsync();
    }
}