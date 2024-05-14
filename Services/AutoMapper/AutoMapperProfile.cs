using AutoMapper;
using Domain.Entities;
using Models.Dto;

namespace Services.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProjectEntity, ProjectDto>();
            CreateMap<ProjectDto, ProjectEntity>();
            CreateMap<TaskEntity, TaskDto>();
            CreateMap<TaskDto, TaskEntity>();
            CreateMap<DeskEntity, DeskDto>();
            CreateMap<DeskDto, DeskEntity>();
            CreateMap<UserDto, UserEntity>().ReverseMap();
            CreateMap<CreatingUserDto, UserEntity>().ReverseMap();
        }
    }
}
