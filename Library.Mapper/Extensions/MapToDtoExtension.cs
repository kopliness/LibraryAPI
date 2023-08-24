using AutoMapper;
using Library.Domain.Dto;
using Library.Domain.Models;

namespace Library.Mapper.Extensions
{
    public static class MapToDtoExtension
    {
        public static BookDto MapToDto(this BookModel? bookModel, IMapper mapper) => mapper.Map<BookDto>(bookModel);
    }
}
