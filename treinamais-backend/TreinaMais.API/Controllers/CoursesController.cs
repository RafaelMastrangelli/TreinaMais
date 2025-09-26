using EducaDev.API.Application.DTOs;
using EducaDev.API.Application.Services.Interfaces;
using EducaDev.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EducaDev.API.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento de cursos
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseApplicationService _courseApplicationService;
        
        public CoursesController(ICourseApplicationService courseApplicationService)
        {
            _courseApplicationService = courseApplicationService;
        }

        /// <summary>
        /// Obtém todos os cursos disponíveis
        /// </summary>
        /// <returns>Lista de cursos</returns>
        /// <response code="200">Lista de cursos retornada com sucesso</response>
        /// <response code="400">Erro ao buscar cursos</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CourseDto>), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAll()
        {
            var result = await _courseApplicationService.GetAllCoursesAsync();
            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Data);
        }

        /// <summary>
        /// Obtém um curso específico por ID
        /// </summary>
        /// <param name="id">ID do curso</param>
        /// <returns>Dados do curso</returns>
        /// <response code="200">Curso encontrado</response>
        /// <response code="404">Curso não encontrado</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CourseDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CourseDto>> GetById(int id)
        {
            var result = await _courseApplicationService.GetCourseByIdAsync(id);
            if (!result.IsSuccess)
                return NotFound(result.ErrorMessage);

            return Ok(result.Data);
        }

        /// <summary>
        /// Cria um novo curso
        /// </summary>
        /// <param name="input">Dados do curso a ser criado</param>
        /// <returns>Dados do curso criado</returns>
        /// <response code="201">Curso criado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(CourseResultDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<CourseResultDto>> Create(AddOrUpdateCourseModel input)
        {
            var result = await _courseApplicationService.CreateCourseAsync(input);
            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
        }

        /// <summary>
        /// Atualiza um curso existente
        /// </summary>
        /// <param name="id">ID do curso</param>
        /// <param name="input">Novos dados do curso</param>
        /// <returns>Dados do curso atualizado</returns>
        /// <response code="200">Curso atualizado com sucesso</response>
        /// <response code="404">Curso não encontrado</response>
        /// <response code="401">Não autorizado</response>
        [HttpPut("{id:int}")]
        [Authorize]
        [ProducesResponseType(typeof(CourseResultDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<CourseResultDto>> Update(int id, [FromBody] AddOrUpdateCourseModel input)
        {
            var result = await _courseApplicationService.UpdateCourseAsync(id, input);
            if (!result.IsSuccess)
                return NotFound(result.ErrorMessage);

            return Ok(result.Data);
        }

        /// <summary>
        /// Remove um curso
        /// </summary>
        /// <param name="id">ID do curso</param>
        /// <returns>Sem conteúdo</returns>
        /// <response code="204">Curso removido com sucesso</response>
        /// <response code="404">Curso não encontrado</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _courseApplicationService.DeleteCourseAsync(id);
            if (!result.IsSuccess)
                return NotFound(result.ErrorMessage);

            return NoContent();
        }

        /// <summary>
        /// Faz upload de imagem para um curso
        /// </summary>
        /// <param name="id">ID do curso</param>
        /// <param name="file">Arquivo de imagem</param>
        /// <returns>Resultado do upload</returns>
        /// <response code="200">Imagem enviada com sucesso</response>
        /// <response code="400">Erro no upload</response>
        /// <response code="404">Curso não encontrado</response>
        [HttpPost("{id:int}/image")]
        [ProducesResponseType(typeof(ImageUploadResultDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
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
