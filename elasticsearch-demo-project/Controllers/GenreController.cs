using elasticsearch_demo_project.Features.Genre.Commands;
using elasticsearch_demo_project.Features.Genre.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace elasticsearch_demo_project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GenreController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getGenre")]
        public async Task<IActionResult> GetGenres([FromQuery] GetGenresQuery query)
        {
            var genres = await _mediator.Send(query);

            if (!genres.Any())
            {
                return NotFound("No genres found for the provided codes.");
            }

            return Ok(genres);
        }

        [HttpPost("createGenre")]
        public async Task<IActionResult> AddGenre([FromBody] AddGenreCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut("updateGenre/{genreCode}")]
        public async Task<IActionResult> UpdateGenre([FromBody] UpdateGenreCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("deleteGenre/{genreCode}")]
        public async Task<IActionResult> DeleteGenre([FromBody] DeleteGenreCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
