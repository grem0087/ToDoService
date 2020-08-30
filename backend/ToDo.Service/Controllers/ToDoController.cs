using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ToDo.Api.Controllers.Dtos;
using ToDo.Database.Entities;
using ToDo.Database.Repository;
using ToDo.Service.Controllers.Dtos;
using ToDo.Service.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ToDo.Service.Controllers
{
    [Route("/ToDo/v1")]
    [Authorize]
    public class ToDo : ControllerBase
    {
        private readonly IToDoRepository _ToDoRepository;
        private readonly AbstractValidator<CreateToDoDto> _ToDoValidator;
        private readonly IMapper _mapper;

        public ToDo(IToDoRepository ToDoRepository, IMapper mapper, AbstractValidator<CreateToDoDto> ToDoValidator)
        {
            _ToDoRepository = ToDoRepository;
            _mapper = mapper;
            _ToDoValidator = ToDoValidator;
        }

        [HttpGet("listAll")]
        public async Task<IActionResult> ListAllForTable([FromQuery]string tableId)
        {
            var result = await QueryDataFromRepository(new SearchRequestDto { TableId = tableId });
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<IActionResult> Create([FromBody] CreateToDoDto ToDoDto)
        {
            var validationResult = _ToDoValidator.Validate(ToDoDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(BadRequestMessageHelper.BadRequestFormat(validationResult.Errors));
            }

            var entity = _mapper.Map<CreateToDoDto, ToDoEntity>(ToDoDto);
            await _ToDoRepository.AddAsync(entity);
            return Created($"{entity.Id}", null);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateToDoDto ToDoDto)
        {
            var validationResult = _ToDoValidator.Validate(ToDoDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(BadRequestMessageHelper.BadRequestFormat(validationResult.Errors));
            }

            var entity = _mapper.Map<CreateToDoDto, ToDoEntity>(ToDoDto);
            entity.Id = int.Parse(id);
            await _ToDoRepository.UpdateAsync(entity);

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _ToDoRepository.RemoveAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }

        [Route("search")]
        [HttpPost]
        public async Task<SearchResultDto> Search(SearchRequestDto request)
        {
            IEnumerable<ToDoDto> result = await QueryDataFromRepository(request);

            return new SearchResultDto()
            {
                Items = result
            };
        }

        private async Task<IEnumerable<ToDoDto>> QueryDataFromRepository(SearchRequestDto request)
        {
            var predicate = PredicateBuilderHelper.CreateSearchPredicate(request);
            var result = (await _ToDoRepository.GetListAsync(predicate))
                .Select(entity => _mapper.Map<ToDoEntity, ToDoDto>(entity));
            return result;
        }
    }
}