using AutoMapper;
using Library.BusinessLayer.Dto;
using Library.DataLayer.Models;

namespace Library.BusinessLayer.Mapping;

public class AuthorMappingProfile : Profile
{
    public AuthorMappingProfile()
    {
        CreateMap<AuthorModel, AuthorCreateDto>()
            .ReverseMap();
        CreateMap<AuthorModel, AuthorReadDto>()
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.BookIds, opt => opt.MapFrom(src => src.BookAuthors.Select(ba => ba.Book.Id)))
            .ReverseMap();
    }
}

