namespace Library.DataLayer.Models;

public class AuthorModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public List<BookAuthor> BookAuthors { get; set; } = new(); 
}