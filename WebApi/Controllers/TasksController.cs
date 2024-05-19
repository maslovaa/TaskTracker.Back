using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(ITasksRepository _tasksRepository, IMapper _mapper) : ControllerBase
    {
        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetAsync()
        {
            return _mapper.Map<List<TaskDto>>(await _tasksRepository.GetByPredicate(x => true, CancellationToken.None));
        }

        // GET api/Tasks/<Guid>
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetAsync(Guid id)
        {
            return _mapper.Map<TaskDto>(await _tasksRepository.GetByIdAsync(id, CancellationToken.None));
        }

        // POST api/Tasks
        [HttpPost]
        public async Task<ActionResult<Guid>> Post([FromBody] TaskDto taskDto)
        {
            return await _tasksRepository.AddAsync(_mapper.Map<TaskEntity>(taskDto));
        }

        // PUT api/Tasks
        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody] TaskDto taskDto)
        {
            return await _tasksRepository.Update(_mapper.Map<TaskEntity>(taskDto));
        }

        // DELETE api/Tasks/<Giud>
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            return await _tasksRepository.Delete(id, CancellationToken.None);
        }
    }
}
