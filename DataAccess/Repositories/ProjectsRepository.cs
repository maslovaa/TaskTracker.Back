using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Models.Dto;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public class ProjectsRepository(DataContext dataContext, IMapper mapper) : IProjectsRepository
    {
        private readonly DataContext _context = dataContext;
        private readonly IMapper _mapper = mapper;

        public async Task<Guid> AddProjectAsync(ProjectDto projectDto)
        {
            try
            {
                ProjectEntity projectEntity = _mapper.Map<ProjectEntity>(projectDto);
                var res = await _context.ProjectEntities.AddAsync(projectEntity);
                await _context.SaveChangesAsync();
                return res.Entity.Id;
            }
            catch
            {
                return Guid.Empty;
            }
        }

        public async Task<bool> DeleteProjectAsync(Guid projectId)
        {
            try
            {
                var project = await _context.ProjectEntities.FindAsync(projectId);
                _context.ProjectEntities.Remove(project);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ProjectDto> FindProjectByIdAsync(Guid projectId)
        {
            try
            {
                ProjectEntity projectEntity = await _context.ProjectEntities.FindAsync(projectId);
                ProjectDto projectDto = _mapper.Map<ProjectDto>(projectEntity);
                return projectDto;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<ProjectDto>> GetProjectsByPredicateAsync(Expression<Func<ProjectEntity, bool>> predicate)
        {
            try
            {
                List<ProjectEntity> projectEntity = await _context.ProjectEntities.Where(predicate).ToListAsync();
                List<ProjectDto> projectDto = _mapper.Map<List<ProjectDto>>(projectEntity);
                return projectDto;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateProjectAsync(ProjectDto projectDto)
        {
            try
            {
                ProjectEntity projectEntity = _mapper.Map<ProjectEntity>(projectDto);
                _context.ProjectEntities.Update(projectEntity);
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
