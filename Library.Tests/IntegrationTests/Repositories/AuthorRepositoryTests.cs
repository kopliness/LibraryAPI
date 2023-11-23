namespace Library.Tests.IntegrationTests.Repositories;

public class AuthorRepositoryTests
{
    private readonly Fixture _fixture;

    public AuthorRepositoryTests()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateAuthor_WhenAuthorModelIsValid()
    {
        // Arrange
        var authorModel = _fixture.Create<Author>();
        var options = TestHelper.GetDbContextOptions();
        using (var context = new LibraryContext(options))
        {
            var repository = new AuthorRepository(context);

            // Act
            await repository.CreateAsync(authorModel);

            // Assert
            var addedAuthor = await context.Authors.FirstOrDefaultAsync(a => a.Id == authorModel.Id);
            addedAuthor.Should().NotBeNull();
            addedAuthor.Should().BeEquivalentTo(authorModel);
        }
    }

    [Fact]
    public async Task ReadAll_ShouldReturnAllAuthors()
    {
        // Arrange
        var options = TestHelper.GetDbContextOptions();
        var authorModels = _fixture.CreateMany<Author>(3).ToList();
        using (var context = new LibraryContext(options))
        {
            context.Database.EnsureDeleted();
            context.Authors.AddRange(authorModels);
            await context.SaveChangesAsync();
        }

        using (var context = new LibraryContext(options))
        {
            var repository = new AuthorRepository(context);

            // Act
            var result = await repository.ReadAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(authorModels.Count);
        }
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateAuthor_WhenAuthorExists()
    {
        // Arrange
        var authorModel = _fixture.Create<Author>();
        var options = TestHelper.GetDbContextOptions();
        using (var context = new LibraryContext(options))
        {
            context.Authors.Add(authorModel);
            await context.SaveChangesAsync();
        }

        using (var context = new LibraryContext(options))
        {
            var repository = new AuthorRepository(context);
            var updatedAuthorModel = _fixture.Build<Author>()
                .With(a => a.Id, authorModel.Id)
                .Create();

            // Act
            var result = await repository.UpdateAsync(authorModel.Id, updatedAuthorModel);

            // Assert
            result.Should().NotBeNull();
            result.FirstName.Should().Be(updatedAuthorModel.FirstName);
            result.LastName.Should().Be(updatedAuthorModel.LastName);
        }
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveAuthor()
    {
        // Arrange
        var authorModel = _fixture.Create<Author>();
        var options = TestHelper.GetDbContextOptions();
        using (var context = new LibraryContext(options))
        {
            context.Authors.Add(authorModel);
            await context.SaveChangesAsync();
        }

        using (var context = new LibraryContext(options))
        {
            var repository = new AuthorRepository(context);

            // Act
            await repository.DeleteAsync(authorModel.Id);

            // Assert
            var deletedAuthor = await context.Authors.FirstOrDefaultAsync(a => a.Id == authorModel.Id);
            deletedAuthor.Should().BeNull();
        }
    }
}