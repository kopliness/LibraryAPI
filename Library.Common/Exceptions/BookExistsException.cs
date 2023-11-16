namespace Library.Common.Exceptions
{
    public class BookExistsException : Exception
    {
        public BookExistsException(string message) : base(message)
        {
        }
    }
}