using AutoMapper;
using Library.Business.Dto;
using Library.DAL.Entities;

namespace Library.Business.Mapping;

public class AuthorMappingProfile : Profile
{
    public AuthorMappingProfile()
    {
        CreateMap<Author, AuthorCreateDto>()
            .ReverseMap();
        CreateMap<Author, AuthorReadDto>()
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.BookIds, opt => opt.MapFrom(src => src.BookAuthors.Select(ba => ba.Book.Id)))
            .ReverseMap();
    }
}