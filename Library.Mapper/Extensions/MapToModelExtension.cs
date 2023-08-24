using AutoMapper;
using Library.Domain.Dto;
using Library.Domain.Models;

namespace Library.Mapper.Extensions
{
    public static class MapToModelExtension
    {
        public static BookModel? MapToModel(this BookDto bookDto, IMapper mapper)=>mapper.Map<BookModel>(bookDto);
    }
}
