using EducaDev.API.Application.DTOs;
using EducaDev.API.Application.Services.Interfaces;
using EducaDev.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EducaDev.API.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento de avaliações de cursos
    /// </summary>
    [ApiController]
    [Route("api/courses/{courseId:int}/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewApplicationService _reviewApplicationService;

        public ReviewsController(IReviewApplicationService reviewApplicationService)
        {
            _reviewApplicationService = reviewApplicationService;
        }

        /// <summary>
        /// Obtém todas as avaliações de um curso
        /// </summary>
        /// <param name="courseId">ID do curso</param>
        /// <returns>Lista de avaliações do curso</returns>
        /// <response code="200">Lista de avaliações retornada com sucesso</response>
        /// <response code="404">Curso não encontrado</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ReviewResultDto>), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<ReviewResultDto>>> GetAll(int courseId)
        {
            var result = await _reviewApplicationService.GetReviewsByCourseAsync(courseId);
            if (!result.IsSuccess)
                return NotFound(result.ErrorMessage);

            return Ok(result.Data);
        }

        /// <summary>
        /// Obtém uma avaliação específica por ID
        /// </summary>
        /// <param name="courseId">ID do curso</param>
        /// <param name="id">ID da avaliação</param>
        /// <returns>Dados da avaliação</returns>
        /// <response code="200">Avaliação encontrada</response>
        /// <response code="404">Avaliação não encontrada</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ReviewResultDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ReviewResultDto>> GetById(int courseId, int id)
        {
            var result = await _reviewApplicationService.GetReviewByIdAsync(courseId, id);
            if (!result.IsSuccess)
                return NotFound(result.ErrorMessage);

            return Ok(result.Data);
        }

        /// <summary>
        /// Cria uma nova avaliação para um curso
        /// </summary>
        /// <param name="courseId">ID do curso</param>
        /// <param name="input">Dados da avaliação</param>
        /// <returns>Dados da avaliação criada</returns>
        /// <response code="201">Avaliação criada com sucesso</response>
        /// <response code="404">Curso não encontrado</response>
        /// <response code="401">Não autorizado</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(ReviewResultDto), 201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<ReviewResultDto>> Create(int courseId, [FromBody] AddReviewModel input)
        {
            var result = await _reviewApplicationService.CreateReviewAsync(courseId, input);
            if (!result.IsSuccess)
                return NotFound(result.ErrorMessage);

            return CreatedAtAction(nameof(GetById), new { courseId, id = result.Data!.Id }, result.Data);
        }

        /// <summary>
        /// Remove uma avaliação
        /// </summary>
        /// <param name="courseId">ID do curso</param>
        /// <param name="id">ID da avaliação</param>
        /// <returns>Sem conteúdo</returns>
        /// <response code="204">Avaliação removida com sucesso</response>
        /// <response code="404">Avaliação não encontrada</response>
        /// <response code="401">Não autorizado</response>
        [HttpDelete("{id:int}")]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Delete(int courseId, int id)
        {
            var result = await _reviewApplicationService.DeleteReviewAsync(courseId, id);
            if (!result.IsSuccess)
                return NotFound(result.ErrorMessage);

            return NoContent();
        }

        /// <summary>
        /// Aprova uma avaliação
        /// </summary>
        /// <param name="courseId">ID do curso</param>
        /// <param name="id">ID da avaliação</param>
        /// <returns>Dados da avaliação aprovada</returns>
        /// <response code="200">Avaliação aprovada com sucesso</response>
        /// <response code="404">Avaliação não encontrada</response>
        [HttpPatch("{id:int}/approve")]
        [ProducesResponseType(typeof(ReviewResultDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ReviewResultDto>> Approve(int courseId, int id)
        {
            var result = await _reviewApplicationService.ApproveReviewAsync(courseId, id);
            if (!result.IsSuccess)
                return NotFound(result.ErrorMessage);

            return Ok(result.Data);
        }

        /// <summary>
        /// Rejeita uma avaliação
        /// </summary>
        /// <param name="courseId">ID do curso</param>
        /// <param name="id">ID da avaliação</param>
        /// <param name="reason">Motivo da rejeição (opcional)</param>
        /// <returns>Dados da avaliação rejeitada</returns>
        /// <response code="200">Avaliação rejeitada com sucesso</response>
        /// <response code="404">Avaliação não encontrada</response>
        [HttpPatch("{id:int}/reject")]
        [ProducesResponseType(typeof(ReviewResultDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ReviewResultDto>> Reject(int courseId, int id, [FromQuery] string? reason = null)
        {
            var result = await _reviewApplicationService.RejectReviewAsync(courseId, id, reason);
            if (!result.IsSuccess)
                return NotFound(result.ErrorMessage);

            return Ok(result.Data);
        }
    }
}
