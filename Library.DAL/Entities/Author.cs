namespace Library.DAL.Entities;

public class Author
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public List<BookAuthor> BookAuthors { get; set; } = new();
}