using AutoMapper;
using Library.BusinessLayer.Dto;
using Library.DataLayer.Models;

namespace Library.BusinessLayer.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile() => CreateMap<UserModel, UserDto>()
        .ReverseMap();
}