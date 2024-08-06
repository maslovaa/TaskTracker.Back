using AutoMapper;
using DataAccess.Repositories;
using Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Dto;
using Models.DTO;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusesController(IStatusesRepository _statusesRepository, IMapper _mapper) : ControllerBase
    {
        // GET api/Statuses/<Guid>
        [HttpGet("{id}")]
        public async Task<ActionResult<StatusDto>> GetAsync(Guid id)
        {
            return _mapper.Map<StatusDto>(await _statusesRepository.GetByIdAsync(id, CancellationToken.None));
        }

        [HttpGet]
        public async Task<ActionResult<List<StatusDto>>> GetAsync()
        {
            return _mapper.Map<List<StatusDto>>(await _statusesRepository.GetAll().ToListAsync());
        }
    }
}
