using EducaDev.API.Application.DTOs;
using EducaDev.API.Models;

namespace EducaDev.API.Application.Services.Interfaces
{
    public interface IReviewApplicationService
    {
        Task<ServiceResult<IEnumerable<ReviewResultDto>>> GetReviewsByCourseAsync(int courseId);
        Task<ServiceResult<ReviewResultDto>> GetReviewByIdAsync(int courseId, int id);
        Task<ServiceResult<ReviewResultDto>> CreateReviewAsync(int courseId, AddReviewModel input);
        Task<ServiceResult> DeleteReviewAsync(int courseId, int id);
        Task<ServiceResult<ReviewResultDto>> ApproveReviewAsync(int courseId, int id);
        Task<ServiceResult<ReviewResultDto>> RejectReviewAsync(int courseId, int id, string? reason = null);
    }
}