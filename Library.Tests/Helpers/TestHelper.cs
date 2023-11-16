public static class TestHelper
{
    public static DbContextOptions<LibraryContext> GetDbContextOptions()
    {
        return new DbContextOptionsBuilder<LibraryContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }
}