using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using Models.DTO;
using Services.Abstractions;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController(IProjectsRepository _projectsRepository, IMapper _mapper, INotificationAdapter _notificationAdapter, IUserEntityService _userService) : ControllerBase
    {
        // GET: api/Projects
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAllWithRelated()
        {
            return _mapper.Map<List<ProjectDto>>(_projectsRepository.GetAllWithRelated());
        }

        // GET api/Projects/<Guid>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetAsync(Guid id)
        {
            return _mapper.Map<ProjectDto>(await _projectsRepository.GetByIdAsync(id, CancellationToken.None));
        }

        // POST api/Projects
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Guid>> Post([FromBody] ProjectDto projectDto)
        {
            var projectId = await _projectsRepository.AddAsync(_mapper.Map<ProjectEntity>(projectDto));
            UserDto user = await _userService.GetByIdAsync(projectDto.OwnerId, CancellationToken.None);

            MessageDto message = new MessageDto
            {
                Email = user.Email,
                Content = $"Проект {projectDto.Name} был создан."
            };

            await _notificationAdapter.ProcessSendAsync(message);

            return projectId;
        }

        // PUT api/Projects
        [Authorize]
        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody] ProjectDto projectDto)
        {
            var isUpdate = await _projectsRepository.UpdateAsync(_mapper.Map<ProjectEntity>(projectDto), CancellationToken.None);
            UserDto user = await _userService.GetByIdAsync(projectDto.OwnerId, CancellationToken.None);

            MessageDto message = new MessageDto
            {
                Email = user.Email,
                Content = $"Проект {projectDto.Name} был изменен."
            };

            await _notificationAdapter.ProcessSendAsync(message);

            return isUpdate;
        }

        // DELETE api/Projects/<Giud>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var isDelete = await _projectsRepository.DeleteAsync(id, CancellationToken.None);

            ProjectDto projectDto = _mapper.Map<ProjectDto>(await _projectsRepository.GetByIdAsync(id, CancellationToken.None));

            UserDto user = await _userService.GetByIdAsync(projectDto.OwnerId, CancellationToken.None);

            MessageDto message = new MessageDto
            {
                Email = user.Email,
                Content = $"Проект {projectDto.Name} был удален."
            };

            await _notificationAdapter.ProcessSendAsync(message);

            return isDelete;
        }
    }
}
