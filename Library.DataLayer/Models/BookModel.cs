namespace Library.DataLayer.Models
{
    public class BookModel
    {
        public Guid Id { get; init; }

        public string Isbn { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }

        public string Description { get; set; }
        
        public List<BookAuthor> BookAuthors { get; set; } = new();
        
        public DateTime BorrowTime { get; init; }

        public DateTime ReturnTime { get; init; }
    }
}