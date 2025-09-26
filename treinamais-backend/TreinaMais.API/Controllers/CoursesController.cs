using EducaDev.API.Application.DTOs;
using EducaDev.API.Application.Services.Interfaces;
using EducaDev.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EducaDev.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseApplicationService _courseApplicationService;
        
        public CoursesController(ICourseApplicationService courseApplicationService)
        {
            _courseApplicationService = courseApplicationService;
        }

        // GET: api/courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAll()
        {
            var result = await _courseApplicationService.GetAllCoursesAsync();
            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Data);
        }

        // GET: api/courses/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CourseDto>> GetById(int id)
        {
            var result = await _courseApplicationService.GetCourseByIdAsync(id);
            if (!result.IsSuccess)
                return NotFound(result.ErrorMessage);

            return Ok(result.Data);
        }

        // POST: api/courses
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CourseResultDto>> Create(AddOrUpdateCourseModel input)
        {
            var result = await _courseApplicationService.CreateCourseAsync(input);
            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
        }

        // PUT: api/courses/5
        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<ActionResult<CourseResultDto>> Update(int id, [FromBody] AddOrUpdateCourseModel input)
        {
            var result = await _courseApplicationService.UpdateCourseAsync(id, input);
            if (!result.IsSuccess)
                return NotFound(result.ErrorMessage);

            return Ok(result.Data);
        }

        // DELETE: api/courses/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _courseApplicationService.DeleteCourseAsync(id);
            if (!result.IsSuccess)
                return NotFound(result.ErrorMessage);

            return NoContent();
        }

        // POST: api/courses/5/image
        [HttpPost("{id:int}/image")]
        public async Task<ActionResult<ImageUploadResultDto>> UploadImage(int id, IFormFile file)
        {
            var result = await _courseApplicationService.UploadCourseImageAsync(id, file);
            if (!result.IsSuccess)
            {
                if (result.ErrorMessage!.Contains("não encontrado"))
                    return NotFound(result.ErrorMessage);
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

    }
}
