using AutoMapper;
using DataAccess.Repositories;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(IRolesRepository _rolesRepository, IMapper _mapper) : ControllerBase
    {
        // GET: api/Roles
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAsync()
        {
            return _mapper.Map<List<RoleDto>>(await _rolesRepository.GetByPredicateAsync(x => true, CancellationToken.None));
        }

        // GET api/Roles/<Guid>
        //[Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetAsync(Guid id)
        {
            return _mapper.Map<RoleDto>(await _rolesRepository.GetByIdAsync(id, CancellationToken.None));
        }

        // POST api/Roles
        //[Authorize]
        [HttpPost]
        public async Task<ActionResult<Guid>> Post([FromBody] RoleDto roleDto)
        {
            return await _rolesRepository.AddAsync(_mapper.Map<RoleEntity>(roleDto));
        }

        // PUT api/Roles
        //[Authorize]
        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody] RoleDto roleDto)
        {
            return await _rolesRepository.UpdateAsync(_mapper.Map<RoleEntity>(roleDto), CancellationToken.None);
        }

        // DELETE api/Roles/<Giud>
        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            return await _rolesRepository.DeleteAsync(id, CancellationToken.None);
        }
    }
}
