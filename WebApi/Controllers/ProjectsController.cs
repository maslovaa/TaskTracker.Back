using Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController(IProjectsRepository _projectsRepository) : ControllerBase
    {
        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAsync()
        {
            return await _projectsRepository.GetProjectsByPredicateAsync(x => true);
        }

        // GET api/Projects/<Guid>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetAsync(Guid id)
        {
            return await _projectsRepository.FindProjectByIdAsync(id);
        }

        // POST api/Projects
        [HttpPost]
        public async Task<ActionResult<Guid>> Post([FromBody] ProjectDto projectDto)
        {
            return await _projectsRepository.AddProjectAsync(projectDto);
        }

        // PUT api/Projects
        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody] ProjectDto projectDto)
        {
            return await _projectsRepository.UpdateProjectAsync(projectDto);
        }

        // DELETE api/Projects/<Giud>
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            return await _projectsRepository.DeleteProjectAsync(id);
        }
    }
}
