namespace Library.Tests.IntegrationTests.Repositories;
public class UserRepositoryTests
{
    private readonly Fixture _fixture;

    public UserRepositoryTests()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Fact]
    public async Task GetUserAsync_ReturnsUser_WhenUserExists()
    {
        // Arrange
        var userModel = _fixture.Create<UserModel>();
        var options = TestHelper.GetDbContextOptions();
        using (var context = new LibraryContext(options))
        {
            context.Users.Add(userModel);
            await context.SaveChangesAsync();
        }

        using (var context = new LibraryContext(options))
        {
            var repository = new UserRepository(context);

            // Act
            var result = await repository.GetUserAsync(userModel);

            // Assert
            result.Should().NotBeNull();
            result.Login.Should().Be(userModel.Login);
            result.Password.Should().Be(userModel.Password);
        }
    }

    [Fact]
    public async Task RegisterUserAsync_CreatesUser_WhenUserModelIsValid()
    {
        // Arrange
        var userModel = _fixture.Create<UserModel>();
        var options = TestHelper.GetDbContextOptions();
        using (var context = new LibraryContext(options))
        {
            var repository = new UserRepository(context);

            // Act
            await repository.RegisterUserAsync(userModel);

            // Assert
            var addedUser = await context.Users.FirstOrDefaultAsync(u => u.Login == userModel.Login);
            addedUser.Should().NotBeNull();
            addedUser.Should().BeEquivalentTo(userModel);
        }
    }
}