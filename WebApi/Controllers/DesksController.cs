using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using Services.Abstractions;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesksController(IDesksRepository _desksRepository, IMapper _mapper) : ControllerBase
    {
        // GET: api/Desks
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeskDto>>> GetAsync()
        {
            return _mapper.Map<List<DeskDto>>(await _desksRepository.GetByPredicateAsync(x => x.IsActive, CancellationToken.None));
        }

        // GET api/Desks/<Guid>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<DeskDto>> GetAsync(Guid id)
        {
            return _mapper.Map<DeskDto>(await _desksRepository.GetByIdAsync(id, CancellationToken.None));
        }

        // POST api/Desks
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Guid>> Post([FromBody] DeskDto deskDto)
        {
            return await _desksRepository.AddAsync(_mapper.Map<DeskEntity>(deskDto));
        }

        // PUT api/Desks
        [Authorize]
        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody] DeskDto deskDto)
        {
            return await _desksRepository.UpdateAsync(_mapper.Map<DeskEntity>(deskDto), CancellationToken.None);
        }

        // DELETE api/Desks/<Giud>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            return await _desksRepository.DeleteAsync(id, CancellationToken.None);
        }
    }
}
