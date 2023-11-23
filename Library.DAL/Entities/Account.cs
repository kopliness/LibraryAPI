namespace Library.DAL.Entities
{
    public class Account
    {
        public Guid Id { get; init; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

    }
}