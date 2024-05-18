using DataAccess.Repositories;
using Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(IRolesRepository _rolesRepository) : ControllerBase
    {
        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAsync()
        {
            return await _rolesRepository.GetRolesByPredicateAsync(x => true);
        }

        // GET api/Roles/<Guid>
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetAsync(Guid id)
        {
            return await _rolesRepository.FindRoleByIdAsync(id);
        }

        // POST api/Roles
        [HttpPost]
        public async Task<ActionResult<Guid>> Post([FromBody] RoleDto roleDto)
        {
            return await _rolesRepository.AddRoleAsync(roleDto);
        }

        // PUT api/Roles
        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody] RoleDto roleDto)
        {
            return await _rolesRepository.UpdateRoleAsync(roleDto);
        }

        // DELETE api/Roles/<Giud>
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            return await _rolesRepository.DeleteRoleAsync(id);
        }
    }
}
