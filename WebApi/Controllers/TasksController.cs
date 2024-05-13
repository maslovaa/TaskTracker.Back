using DataAccess.Repositories;
using Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(ITasksRepository tasksRepository) : ControllerBase
    {
        private readonly ITasksRepository _tasksRepository = tasksRepository;
        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetAsync()
        {
            return await _tasksRepository.GetTasksByPredicateAsync(x => true);
        }

        // GET api/Tasks/<Guid>
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetAsync(Guid id)
        {
            return await _tasksRepository.FindTaskByIdAsync(id);
        }

        // POST api/Tasks
        [HttpPost]
        public async Task<ActionResult<Guid>> Post([FromBody] TaskDto taskDto)
        {
            return await _tasksRepository.AddTaskAsync(taskDto);
        }

        // PUT api/Tasks
        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody] TaskDto taskDto)
        {
            return await _tasksRepository.UpdateTaskAsync(taskDto);
        }

        // DELETE api/Tasks/<Giud>
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            return await _tasksRepository.DeleteTaskAsync(id);
        }
    }
}
