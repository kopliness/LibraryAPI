namespace Library.Business.Dto;

public class AuthorReadDto : BaseAuthorDto
{
    public Guid AuthorId { get; set; }
    public List<Guid> BookIds { get; set; }
}