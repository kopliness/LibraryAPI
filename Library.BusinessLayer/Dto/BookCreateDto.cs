namespace Library.BusinessLayer.Dto
{
    public class BookCreateDto : BaseBookDto
    {
        public List<Guid> Authors { get; set; }
    }
}