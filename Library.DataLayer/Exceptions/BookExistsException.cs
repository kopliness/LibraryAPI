namespace Library.DataLayer.Exceptions
{
    public class BookExistsException: Exception
    {
        public BookExistsException(string message) : base(message)
        {
        }
    }
}
