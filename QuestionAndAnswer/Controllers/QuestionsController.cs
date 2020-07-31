using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuestionAndAnswer.Application.Models;
using QuestionAndAnswer.Application.Questions.Commands;
using QuestionAndAnswer.Application.Questions.Queries;
using QuestionAndAnswer.Persistence;

namespace QuestionAndAnswer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public QuestionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestion(int id)
        {
            var result = await _mediator.Send(new GetQuestionQuery(id));
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        
        [HttpGet]
        public async Task<IEnumerable<QuestionResponce>> GetQuestions(int id)
        {
            return await _mediator.Send(new GetQuestionsQuery());
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == null)
                return BadRequest("Cannot create question");

            return CreatedAtAction("GetQuestion", new {id = result.Id}, result);
        }
    }
}