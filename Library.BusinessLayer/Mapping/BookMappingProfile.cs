using AutoMapper;
using Library.BusinessLayer.Dto;
using Library.DataLayer.Models;

namespace Library.BusinessLayer.Mapping;

public class BookMappingProfile : Profile
{
    public BookMappingProfile()
    {
        CreateMap<BookModel, BookCreateDto>()
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.BookAuthors.Select(ur => ur.AuthorId).ToList()))
            .ReverseMap();
        CreateMap<BookModel, BookReadDto>()
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.BookAuthors.Select(ur => ur.Author)))
            .ReverseMap();
    }
}