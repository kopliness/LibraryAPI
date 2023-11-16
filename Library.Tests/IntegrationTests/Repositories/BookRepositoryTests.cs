namespace Library.Tests.IntegrationTests.Repositories;

public class BookRepositoryTests
{
    private readonly Fixture _fixture;

    public BookRepositoryTests()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Fact]
    public async Task GetBookByIdAsync_ReturnsBook_WhenBookExists()
    {
        // Arrange
        var bookModel = _fixture.Create<Book>();
        var options = TestHelper.GetDbContextOptions();
        using (var context = new LibraryContext(options))
        {
            context.Books.Add(bookModel);
            await context.SaveChangesAsync();
        }

        using (var context = new LibraryContext(options))
        {
            var repository = new BookRepository(context);

            // Act
            var result = await repository.ReadByIdAsync(bookModel.Id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(bookModel.Id);
        }
    }

    [Fact]
    public async Task CreateAsync_CreatesBook_WhenBookModelIsValid()
    {
        // Arrange
        var bookModel = _fixture.Create<Book>();
        var options = TestHelper.GetDbContextOptions();
        using (var context = new LibraryContext(options))
        {
            var repository = new BookRepository(context);

            // Act
            await repository.CreateAsync(bookModel);

            // Assert
            var addedBook = await context.Books.FirstOrDefaultAsync(b => b.Id == bookModel.Id);
            addedBook.Should().NotBeNull();
            addedBook.Should().BeEquivalentTo(bookModel);
        }
    }

    [Fact]
    public async Task GetByIsbnAsync_ReturnsBook_WhenBookExists()
    {
        // Arrange
        var bookModel = _fixture.Create<Book>();
        var options = TestHelper.GetDbContextOptions();
        using (var context = new LibraryContext(options))
        {
            context.Books.Add(bookModel);
            await context.SaveChangesAsync();
        }

        using (var context = new LibraryContext(options))
        {
            var repository = new BookRepository(context);

            // Act
            var result = await repository.ReadByIsbnAsync(bookModel.Isbn);

            // Assert
            result.Should().NotBeNull();
            result.Isbn.Should().Be(bookModel.Isbn);
        }
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllBooks()
    {
        // Arrange
        var options = TestHelper.GetDbContextOptions();
        var bookModel = _fixture.Create<Book>();
        using (var context = new LibraryContext(options))
        {
            context.Books.Add(bookModel);
            await context.SaveChangesAsync();
        }

        using (var context = new LibraryContext(options))
        {
            var repository = new BookRepository(context);

            // Act
            var result = repository.ReadAll();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCountGreaterThan(0);
        }
    }

    [Fact]
    public async Task UpdateBookAsync_ShouldUpdateBook()
    {
        // Arrange
        var bookModel = _fixture.Create<Book>();

        var options = TestHelper.GetDbContextOptions();
        using (var context = new LibraryContext(options))
        {
            var bookRepository = new BookRepository(context);
            await bookRepository.CreateAsync(bookModel);
            var newBookModel = _fixture.Create<Book>();

            // Act
            var result = await bookRepository.UpdateAsync(bookModel.Id, newBookModel);

            // Assert
            result.Should().NotBeNull();
            result.Isbn.Should().Be(newBookModel.Isbn);
            result.Title.Should().Be(newBookModel.Title);
            result.Genre.Should().Be(newBookModel.Genre);
            result.Description.Should().Be(newBookModel.Description);
        }
    }

    [Fact]
    public async Task DeleteAsync_RemovesBook()
    {
        // Arrange
        var bookModel = _fixture.Create<Book>();
        var options = TestHelper.GetDbContextOptions();
        using (var context = new LibraryContext(options))
        {
            context.Books.Add(bookModel);
            await context.SaveChangesAsync();
        }

        using (var context = new LibraryContext(options))
        {
            var repository = new BookRepository(context);

            // Act
            await repository.DeleteAsync(bookModel.Id);

            // Assert
            var deletedBook = await repository.ReadByIdAsync(bookModel.Id);
            deletedBook.Should().BeNull();
        }
    }

    [Fact]
    public async Task AddMultipleAuthorsToBook_ShouldSucceed()
    {
        // Arrange
        var bookId = _fixture.Create<Guid>();
        var authorIds = _fixture.Create<List<Guid>>();
        var cancellationToken = new CancellationToken();

        var options = TestHelper.GetDbContextOptions();
        using (var context = new LibraryContext(options))
        {
            context.Database.EnsureDeleted();
            var bookRepository = new BookRepository(context);

            // Act
            await bookRepository.AddAuthorToBook(bookId, authorIds, cancellationToken);

            // Assert
            var bookAuthors = await context.BookAuthors.ToListAsync();
            bookAuthors.Should().HaveCount(authorIds.Count);
        }
    }

    [Fact]
    public async Task AuthorExists_ShouldReturnTrueForExistingAuthor()
    {
        // Arrange
        var authorId = _fixture.Create<Guid>();
        var authors = _fixture.CreateMany<Author>(3).ToList();
        authors[0].Id = authorId;

        var options = TestHelper.GetDbContextOptions();
        using (var context = new LibraryContext(options))
        {
            context.Authors.AddRange(authors);
            await context.SaveChangesAsync();
            var repository = new BookRepository(context);

            // Act
            var result = await repository.AuthorExists(authorId);

            // Assert
            result.Should().BeTrue();
        }
    }
}