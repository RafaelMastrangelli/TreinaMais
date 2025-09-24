using EducaDev.API.Core.Entities;
using EducaDev.API.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EducaDev.API.Infrastructure.Persistence.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly EducaDevContext _context;

        public CourseRepository(EducaDevContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Courses
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetAllWithReviewsAsync()
        {
            return await _context.Courses
                .Include(c => c.Reviews)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _context.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Course?> GetByIdWithReviewsAsync(int id)
        {
            return await _context.Courses
                .Include(c => c.Reviews)
                .AsNoTracking()
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Courses
                .AnyAsync(c => c.Id == id);
        }

        public async Task<Course> AddAsync(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task UpdateAsync(Course course)
        {
            course.UpdatedAtUtc = DateTime.UtcNow;
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> TryDeleteAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return false;

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Course?> GetByIdForImageUploadAsync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}