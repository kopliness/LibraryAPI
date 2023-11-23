namespace Library.Tests.IntegrationTests.Repositories;

public class AccountRepositoryTests
{
    private readonly Fixture _fixture;

    public AccountRepositoryTests()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Fact]
    public async Task GetAccountAsync_ReturnsAccount_WhenAccountExists()
    {
        // Arrange
        var accountModel = _fixture.Create<Account>();
        var options = TestHelper.GetDbContextOptions();
        using (var context = new LibraryContext(options))
        {
            context.Accounts.Add(accountModel);
            await context.SaveChangesAsync();
        }

        using (var context = new LibraryContext(options))
        {
            var repository = new AccountRepository(context);

            // Act
            var result = await repository.GetAccountAsync(accountModel);

            // Assert
            result.Should().NotBeNull();
            result.Login.Should().Be(accountModel.Login);
            result.Password.Should().Be(accountModel.Password);
        }
    }

    [Fact]
    public async Task RegisterAccountAsync_CreatesAccount_WhenAccountModelIsValid()
    {
        // Arrange
        var accountModel = _fixture.Create<Account>();
        var options = TestHelper.GetDbContextOptions();
        using (var context = new LibraryContext(options))
        {
            var repository = new AccountRepository(context);

            // Act
            await repository.RegisterAccountAsync(accountModel);

            // Assert
            var addedAccount = await context.Accounts.FirstOrDefaultAsync(u => u.Login == accountModel.Login);
            addedAccount.Should().NotBeNull();
            addedAccount.Should().BeEquivalentTo(accountModel);
        }
    }
}