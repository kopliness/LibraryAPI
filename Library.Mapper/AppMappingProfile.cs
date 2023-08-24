using AutoMapper;
using Library.Domain.Dto;

namespace Library.Mapper
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile() => CreateMap<BookModel, BookDto>()
            .ReverseMap();
    }
}
