using AutoMapper;
using Domain.Entities;
using Models.Dto;
using Models.DTO;
using Models.DTO.IdentityDtos;

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

            CreateMap<UserEntity, UserDto>();
            CreateMap<UserDto, UserEntity>()
                .ForMember(dest => dest.IsActive,
                opt => opt.MapFrom(src => true));

            CreateMap<CreatingUserDto, UserEntity>()
                .ForMember(dest => dest.IsActive,
                opt => opt.MapFrom(src => true));
            CreateMap<UserEntity, CreatingUserDto>();

            CreateMap<RoleEntity, RoleDto>();
            CreateMap<RoleDto, RoleEntity>()
                .ForMember(dest => dest.IsActive,
                opt => opt.MapFrom(src => true));

            CreateMap<StatusEntity, StatusDto>();
            CreateMap<StatusDto, StatusEntity>()
                .ForMember(dest => dest.IsActive,
                opt => opt.MapFrom(src => true));

            CreateMap<RequestRegisterUserDto, UserEntity>()
                .ForMember(dest => dest.IsActive,
                opt => opt.MapFrom(src => true));
            CreateMap<UserEntity, RequestRegisterUserDto>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());
        }
    }
}
