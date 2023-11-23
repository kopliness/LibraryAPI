namespace Library.Business.Dto;

public class BookCreateDto : BaseBookDto
{
    public List<Guid> Authors { get; set; }
}