namespace Library.DataLayer.Models.Dto
{
    public record BookDto(string Isbn, string Title, string Genre, string Description, string Author,
        DateTime BorrowTime, DateTime ReturnTime);
}
