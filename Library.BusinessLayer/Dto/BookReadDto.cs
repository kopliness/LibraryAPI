namespace Library.BusinessLayer.Dto
{
    public class BookReadDto : BaseBookDto
    {
        public Guid Id{ get; set; }
        public List<AuthorCreateDto> Authors{ get; set; }
    }
}