using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Models.Dto;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public class DesksRepository(DataContext dataContext, IMapper mapper) : IDesksRepository
    {
        private readonly DataContext _context = dataContext;
        private readonly IMapper _mapper = mapper;

        public async Task<Guid> AddDeskAsync(DeskDto deskDto)
        {
            try
            {
                DeskEntity deskEntity = _mapper.Map<DeskEntity>(deskDto);
                var res = await _context.DeskEntities.AddAsync(deskEntity);
                await _context.SaveChangesAsync();
                return res.Entity.Id;
            }
            catch
            {
                return Guid.Empty;
            }
        }

        public async Task<bool> DeleteDeskAsync(Guid deskId)
        {
            try
            {
                var desk = await _context.DeskEntities.FindAsync(deskId);
                _context.DeskEntities.Remove(desk);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<DeskDto> FindDeskByIdAsync(Guid deskId)
        {
            try
            {
                DeskEntity deskEntity = await _context.DeskEntities.FindAsync(deskId);
                DeskDto deskDto = _mapper.Map<DeskDto>(deskEntity);
                return deskDto;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<DeskDto>> GetDesksByPredicateAsync(Expression<Func<DeskEntity, bool>> predicate)
        {
            try
            {
                List<DeskEntity> deskEntity = await _context.DeskEntities.Where(predicate).ToListAsync();
                List<DeskDto> deskDto = _mapper.Map<List<DeskDto>>(deskEntity);
                return deskDto;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateDeskAsync(DeskDto deskDto)
        {
            try
            {
                DeskEntity deskEntity = _mapper.Map<DeskEntity>(deskDto);
                _context.DeskEntities.Update(deskEntity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
