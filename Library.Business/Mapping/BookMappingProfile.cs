using AutoMapper;
using Library.Business.Dto;
using Library.DAL.Entities;

namespace Library.Business.Mapping;

public class BookMappingProfile : Profile
{
    public BookMappingProfile()
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<BookCreateDto, Book>()
            .ForMember(destination => destination.BookAuthors, opt => opt.NullSubstitute(new List<BookAuthor>())));

        CreateMap<Book, BookCreateDto>()
            .ForMember(dest => dest.Authors,
                opt => opt.MapFrom(src => src.BookAuthors.Select(ur => ur.AuthorId).ToList()))
            .ReverseMap();
        CreateMap<Book, BookReadDto>()
            .ForMember(dest => dest.Authors,
                opt => opt.MapFrom(src => src.BookAuthors.Select(ur => ur.Author).ToList()))
            .ReverseMap();
    }
}