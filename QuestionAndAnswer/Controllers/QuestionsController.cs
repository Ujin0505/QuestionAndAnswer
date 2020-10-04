using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<QuestionDto>> GetQuestions(string search)
        {
            return await _mediator.Send(new GetQuestionsQuery() {Search = search});
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == null)
                return BadRequest("Cannot create question");
            
            _questionMemoryCacheService.Set(result);

            return CreatedAtAction(nameof(GetQuestion), new {id = result.Id}, result);
        }


        [Authorize(Policy = "Author")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateQuestion([FromBody]UpdateQuestionCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == false)
                return BadRequest("Cannot update question");
            
            _questionMemoryCacheService.Remove(command.Id);

            return NoContent();
        }
        
        [Authorize(Policy = "Author")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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