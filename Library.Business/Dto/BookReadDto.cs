namespace Library.Business.Dto;

public class BookReadDto : BaseBookDto
{
    public Guid Id { get; set; }
    public List<AuthorCreateDto> Authors { get; set; }

    public DateTime BorrowTime { get; init; }

    public DateTime ReturnTime { get; init; }
}