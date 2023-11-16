using AutoMapper;
using Library.Business.Dto;
using Library.DAL.Models;

namespace Library.Business.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile() => CreateMap<User, UserDto>()
        .ReverseMap();
}