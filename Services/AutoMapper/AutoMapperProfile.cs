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
            CreateMap<ProjectDto, ProjectEntity>()
                .ForMember(dest => dest.IsActive,
                opt => opt.MapFrom(src => true));
            CreateMap<TaskEntity, TaskDto>();
            CreateMap<TaskDto, TaskEntity>()
                .ForMember(dest => dest.IsActive,
                opt => opt.MapFrom(src => true));
            CreateMap<DeskEntity, DeskDto>();
            CreateMap<DeskDto, DeskEntity>()
                .ForMember(dest => dest.IsActive,
                opt => opt.MapFrom(src => true));
            CreateMap<UserDto, UserEntity>().ReverseMap();
            CreateMap<CreatingUserDto, UserEntity>()
                .ForMember(dest => dest.IsActive,
                opt => opt.MapFrom(src => true));
            CreateMap<RoleEntity, RoleDto>();
            CreateMap<RoleDto, RoleEntity>()
                .ForMember(dest => dest.IsActive,
                opt => opt.MapFrom(src => true));
        }
    }
}
