namespace Library.Shared.Exceptions
{
    public class BookExistsException: Exception
    {
        public BookExistsException(string message) : base(message)
        {
        }
    }
}
