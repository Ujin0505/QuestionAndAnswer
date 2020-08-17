using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuestionAndAnswer.Application.Common.Interfaces;
using QuestionAndAnswer.Application.Models;
using QuestionAndAnswer.Application.Questions.Commands;
using QuestionAndAnswer.Application.Questions.Queries;


namespace QuestionAndAnswer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IQuestionMemoryCacheService _questionMemoryCacheService;

        public QuestionsController(IMediator mediator, IQuestionMemoryCacheService questionMemoryCacheService)
        {
            _mediator = mediator;
            _questionMemoryCacheService = questionMemoryCacheService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestion(int id)
        {
            var cachedResponse = _questionMemoryCacheService.Get(id);
            if (cachedResponse != null)
                return Ok(cachedResponse);
            
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
            
            _questionMemoryCacheService.Set(result);

            return CreatedAtAction(nameof(GetQuestion), new {id = result.Id}, result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion([FromBody]UpdateQuestionCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == false)
                return BadRequest("Cannot update question");
            
            _questionMemoryCacheService.Remove(command.Id);

            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var result = await _mediator.Send(new DeleteQuestionCommand{Id = id});
            if (result == false)
                return NotFound();
            
            _questionMemoryCacheService.Remove(id);

            return NoContent();
        }
        
        
       
    }
}