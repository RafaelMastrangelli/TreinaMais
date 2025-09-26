using EducaDev.API.Application.DTOs;
using EducaDev.API.Models;
using Microsoft.AspNetCore.Http;

namespace EducaDev.API.Application.Services.Interfaces
{
    public interface ICourseApplicationService
    {
        Task<ServiceResult<IEnumerable<CourseDto>>> GetAllCoursesAsync();
        Task<ServiceResult<CourseDto>> GetCourseByIdAsync(int id);
        Task<ServiceResult<CourseResultDto>> CreateCourseAsync(AddOrUpdateCourseModel input);
        Task<ServiceResult<CourseResultDto>> UpdateCourseAsync(int id, AddOrUpdateCourseModel input);
        Task<ServiceResult> DeleteCourseAsync(int id);
        Task<ServiceResult<ImageUploadResultDto>> UploadCourseImageAsync(int id, IFormFile file);
    }
}