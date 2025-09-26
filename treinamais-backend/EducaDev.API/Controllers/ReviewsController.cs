using EducaDev.API.Application.DTOs;
using EducaDev.API.Application.Services.Interfaces;
using EducaDev.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EducaDev.API.Controllers
{
    [ApiController]
    [Route("api/courses/{courseId:int}/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewApplicationService _reviewApplicationService;

        public ReviewsController(IReviewApplicationService reviewApplicationService)
        {
            _reviewApplicationService = reviewApplicationService;
        }

        // GET: api/courses/{courseId}/reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewResultDto>>> GetAll(int courseId)
        {
            var result = await _reviewApplicationService.GetReviewsByCourseAsync(courseId);
            if (!result.IsSuccess)
                return NotFound(result.ErrorMessage);

            return Ok(result.Data);
        }

        // GET: api/courses/{courseId}/reviews/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ReviewResultDto>> GetById(int courseId, int id)
        {
            var result = await _reviewApplicationService.GetReviewByIdAsync(courseId, id);
            if (!result.IsSuccess)
                return NotFound(result.ErrorMessage);

            return Ok(result.Data);
        }

        // POST: api/courses/{courseId}/reviews
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ReviewResultDto>> Create(int courseId, [FromBody] AddReviewModel input)
        {
            var result = await _reviewApplicationService.CreateReviewAsync(courseId, input);
            if (!result.IsSuccess)
                return NotFound(result.ErrorMessage);

            return CreatedAtAction(nameof(GetById), new { courseId, id = result.Data!.Id }, result.Data);
        }


        // DELETE: api/courses/{courseId}/reviews/{id}
        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete(int courseId, int id)
        {
            var result = await _reviewApplicationService.DeleteReviewAsync(courseId, id);
            if (!result.IsSuccess)
                return NotFound(result.ErrorMessage);

            return NoContent();
        }

        // PATCH: api/courses/{courseId}/reviews/{id}/approve
        [HttpPatch("{id:int}/approve")]
        public async Task<ActionResult<ReviewResultDto>> Approve(int courseId, int id)
        {
            var result = await _reviewApplicationService.ApproveReviewAsync(courseId, id);
            if (!result.IsSuccess)
                return NotFound(result.ErrorMessage);

            return Ok(result.Data);
        }

        // PATCH: api/courses/{courseId}/reviews/{id}/reject
        [HttpPatch("{id:int}/reject")]
        public async Task<ActionResult<ReviewResultDto>> Reject(int courseId, int id, [FromQuery] string? reason = null)
        {
            var result = await _reviewApplicationService.RejectReviewAsync(courseId, id, reason);
            if (!result.IsSuccess)
                return NotFound(result.ErrorMessage);

            return Ok(result.Data);
        }
    }
}
