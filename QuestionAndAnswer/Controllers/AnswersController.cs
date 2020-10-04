using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestionAndAnswer.Application.Answers.Commands;
using QuestionAndAnswer.Application.Answers.Queries;

namespace QuestionAndAnswer.Controllers
{
    [ApiController]
    [Route("api/questions/")]
    public class AnswersController: ControllerBase
    {
        private readonly IMediator _mediator;

        public AnswersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("{questionId}/answers/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAnswer(int id)
        {
            var result = await _mediator.Send(new GetAnswerQuery(id));
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("{questionId}/answers")]
        public async Task<IActionResult> CreateAnswer([FromBody]CreateAnswerCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == null)
                return BadRequest("Can not create answer");

            return CreatedAtAction(nameof(GetAnswer), new {questionId = result.QuestionId, id = result.Id}, result);
        }
    }
}