using elasticsearch_demo_project.Features.Publisher.Commands;
using elasticsearch_demo_project.Features.Publisher.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace elasticsearch_demo_project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublisherController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PublisherController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getPublishers")]
        public async Task<IActionResult> GetPublisher([FromQuery] GetPublisherQuery query)
        {
            var publishers = await _mediator.Send(query);

            if (!publishers.Any())
            {
                return NotFound("No publishers found for the provided codes.");
            }

            return Ok(publishers);
        }

        [HttpPost("createPublishers")]
        public async Task<IActionResult> AddPublishers([FromBody] AddPublisherCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut("updatePublishers")]
        public async Task<IActionResult> UpdatePublishers([FromBody] UpdatePublisherCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("deletePublishers")]
        public async Task<IActionResult> DeletePublishers([FromBody] DeletePublisherCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
