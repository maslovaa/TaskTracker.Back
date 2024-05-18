using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Models.Dto;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public class RolesRepository(DataContext _context, IMapper _mapper) : IRolesRepository
    {
        public async Task<Guid> AddRoleAsync(RoleDto roleDto)
        {
            try
            {
                RoleEntity roleEntity = _mapper.Map<RoleEntity>(roleDto);
                var res = await _context.RolesEntities.AddAsync(roleEntity);
                await _context.SaveChangesAsync();
                return res.Entity.Id;
            }
            catch
            {
                return Guid.Empty;
            }
        }

        public async Task<bool> DeleteRoleAsync(Guid roleId)
        {
            try
            {
                var role = await _context.RolesEntities.FindAsync(roleId);
                _context.RolesEntities.Remove(role);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<RoleDto> FindRoleByIdAsync(Guid roleId)
        {
            try
            {
                RoleEntity roleEntity = await _context.RolesEntities.FindAsync(roleId);
                RoleDto roleDto = _mapper.Map<RoleDto>(roleEntity);
                return roleDto;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<RoleDto>> GetRolesByPredicateAsync(Expression<Func<RoleEntity, bool>> predicate)
        {
            try
            {
                List<RoleEntity> roleEntity = await _context.RolesEntities.Where(predicate).ToListAsync();
                List<RoleDto> roleDto = _mapper.Map<List<RoleDto>>(roleEntity);
                return roleDto;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateRoleAsync(RoleDto roleDto)
        {
            try
            {
                RoleEntity roleEntity = _mapper.Map<RoleEntity>(roleDto);
                _context.RolesEntities.Update(roleEntity);
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
