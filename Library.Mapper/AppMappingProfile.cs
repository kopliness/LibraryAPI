using AutoMapper;
using Library.Domain.Dto;
using Library.Domain.Models;

namespace Library.Mapper
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile() => CreateMap<BookModel, BookDto>()
            .ReverseMap();
    }
}
