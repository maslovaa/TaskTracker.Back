using Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesksController(IDesksRepository _desksRepository) : ControllerBase
    {
        // GET: api/Desks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeskDto>>> GetAsync()
        {
            return await _desksRepository.GetDesksByPredicateAsync(x => true);
        }

        // GET api/Desks/<Guid>
        [HttpGet("{id}")]
        public async Task<ActionResult<DeskDto>> GetAsync(Guid id)
        {
            return await _desksRepository.FindDeskByIdAsync(id);
        }

        // POST api/Desks
        [HttpPost]
        public async Task<ActionResult<Guid>> Post([FromBody] DeskDto deskDto)
        {
            return await _desksRepository.AddDeskAsync(deskDto);
        }

        // PUT api/Desks
        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody] DeskDto deskDto)
        {
            return await _desksRepository.UpdateDeskAsync(deskDto);
        }

        // DELETE api/Desks/<Giud>
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            return await _desksRepository.DeleteDeskAsync(id);
        }
    }
}
