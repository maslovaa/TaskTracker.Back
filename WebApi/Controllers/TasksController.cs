using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using Models.DTO;
using Services.Abstractions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(ITasksRepository _tasksRepository, IMapper _mapper, INotificationAdapter _notificationAdapter, IUserEntityService _userService) : ControllerBase
    {
        // GET: api/Tasks
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetAsync()
        {
            return _mapper.Map<List<TaskDto>>(await _tasksRepository.GetByPredicateAsync(x => true, CancellationToken.None));
        }

        // GET: api/Tasks
        [Authorize]
        [HttpGet("{deskId}/{statusId}")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetAsync(Guid deskId, Guid statusId)
        {
            return _mapper.Map<List<TaskDto>>(await _tasksRepository.GetByPredicateAsync(x => x.DeskId == deskId && x.StatusId == statusId, CancellationToken.None));
        }

        // GET api/Tasks/<Guid>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetAsync(Guid id)
        {
            return _mapper.Map<TaskDto>(await _tasksRepository.GetByIdAsync(id, CancellationToken.None));
        }

        // POST api/Tasks
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Guid>> Post([FromBody] TaskDto taskDto)
        {
            var taskId = await _tasksRepository.AddAsync(_mapper.Map<TaskEntity>(taskDto));

            UserDto user = await _userService.GetByIdAsync(taskDto.PerformerId, CancellationToken.None);

            MessageDto message = new MessageDto
            {
                Email = user.Email,
                Content = $"Задача {taskDto.Head} была создана."
            };

            await _notificationAdapter.ProcessSendAsync(message);

            return taskId;
        }

        // PUT api/Tasks
        [Authorize]
        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody] TaskDto taskDto)
        {
            var isUpdate = await _tasksRepository.UpdateAsync(_mapper.Map<TaskEntity>(taskDto), CancellationToken.None);

            UserDto user = await _userService.GetByIdAsync(taskDto.PerformerId, CancellationToken.None);

            MessageDto message = new MessageDto
            {
                Email = user.Email,
                Content = $"Задача {taskDto.Head} была изменена."
            };

            await _notificationAdapter.ProcessSendAsync(message);


            return isUpdate;
        }

        // DELETE api/Tasks/<Giud>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var isDelete = await _tasksRepository.DeleteAsync(id, CancellationToken.None);

            TaskDto taskDto = _mapper.Map<TaskDto>(await _tasksRepository.GetByIdAsync(id, CancellationToken.None));

            UserDto user = await _userService.GetByIdAsync(taskDto.PerformerId, CancellationToken.None);

            MessageDto message = new MessageDto
            {
                Email = user.Email,
                Content = $"Задача {taskDto.Head} была удалена."
            };

            await _notificationAdapter.ProcessSendAsync(message);

            return isDelete;
        }
    }
}
