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
    public class ProjectsController(IProjectsRepository _projectsRepository, IMapper _mapper) : ControllerBase
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
            return await _projectsRepository.AddAsync(_mapper.Map<ProjectEntity>(projectDto));
        }

        // PUT api/Projects
        [Authorize]
        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody] ProjectDto projectDto)
        {
            return await _projectsRepository.UpdateAsync(_mapper.Map<ProjectEntity>(projectDto), CancellationToken.None);
        }

        // DELETE api/Projects/<Giud>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            return await _projectsRepository.DeleteAsync(id, CancellationToken.None);
        }
    }
}
