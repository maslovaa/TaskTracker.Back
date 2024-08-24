using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(ITasksRepository _tasksRepository, IMapper _mapper) : ControllerBase
    {
        // GET: api/Tasks
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetAsync()
        {
            return _mapper.Map<List<TaskDto>>(await _tasksRepository.GetByPredicateAsync(x => true, CancellationToken.None));
        }

        // GET: api/Tasks
        //[Authorize]
        [HttpGet("{deskId}/{statusId}")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetAsync(Guid deskId, Guid statusId)
        {
            return _mapper.Map<List<TaskDto>>(await _tasksRepository.GetByPredicateAsync(x => x.DeskId == deskId && x.StatusId == statusId, CancellationToken.None));
        }

        // GET api/Tasks/<Guid>
        //[Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetAsync(Guid id)
        {
            return _mapper.Map<TaskDto>(await _tasksRepository.GetByIdAsync(id, CancellationToken.None));
        }

        // POST api/Tasks
        //[Authorize]
        [HttpPost]
        public async Task<ActionResult<Guid>> Post([FromBody] TaskDto taskDto)
        {
            return await _tasksRepository.AddAsync(_mapper.Map<TaskEntity>(taskDto));
        }

        // PUT api/Tasks
        //[Authorize]
        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody] TaskDto taskDto)
        {
            return await _tasksRepository.UpdateAsync(_mapper.Map<TaskEntity>(taskDto), CancellationToken.None);
        }

        // DELETE api/Tasks/<Giud>
        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            return await _tasksRepository.DeleteAsync(id, CancellationToken.None);
        }
    }
}
