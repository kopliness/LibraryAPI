namespace Library.Domain.Models
{
    public class BookModel
    {
        public Guid Id { get; init; }

        public string Isbn { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public DateTime BorrowTime { get; init; }

        public DateTime ReturnTime { get; set; }
    }
}
