using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController(IProjectsRepository _projectsRepository, IMapper _mapper) : ControllerBase
    {
        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAsync()
        {
            return _mapper.Map<List<ProjectDto>>(await _projectsRepository.GetByPredicateAsync(x => true, CancellationToken.None));
        }

        // GET api/Projects/<Guid>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetAsync(Guid id)
        {
            return _mapper.Map<ProjectDto>(await _projectsRepository.GetByIdAsync(id, CancellationToken.None));
        }

        // POST api/Projects
        [HttpPost]
        public async Task<ActionResult<Guid>> Post([FromBody] ProjectDto projectDto)
        {
            return await _projectsRepository.AddAsync(_mapper.Map<ProjectEntity>(projectDto));
        }

        // PUT api/Projects
        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody] ProjectDto projectDto)
        {
            return await _projectsRepository.UpdateAsync(_mapper.Map<ProjectEntity>(projectDto), CancellationToken.None);
        }

        // DELETE api/Projects/<Giud>
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            return await _projectsRepository.DeleteAsync(id, CancellationToken.None);
        }
    }
}
