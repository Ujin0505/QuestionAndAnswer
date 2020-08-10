using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuestionAndAnswer.Application.Answers.Commands;
using QuestionAndAnswer.Application.Answers.Queries;

namespace QuestionAndAnswer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnswersController: ControllerBase
    {
        private readonly IMediator _mediator;

        public AnswersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnswer(int id)
        {
            var result = await _mediator.Send(new GetAnswerQuery(id));
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnswer([FromBody]CreateAnswerCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == null)
                return BadRequest("Can not create answer");

            return CreatedAtAction(nameof(GetAnswer), new {id = result.Id});
        }
        
        
    }
}