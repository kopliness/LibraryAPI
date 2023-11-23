namespace Library.Tests.UnitTests.Services;

public class BookServiceTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<BookService>> _loggerMock;
    private readonly BookService _bookService;
    BookCreateDto bookCreateDto = new ()
    {
        Isbn = "1234567844490",
        Title = "Test Book",
        Genre = "Fiction",
        Description = "This is a test book",
        Authors = new List<Guid> { Guid.NewGuid() },
    };
    public BookServiceTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<BookService>>();

        _bookService = new BookService(_bookRepositoryMock.Object, _mapperMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task AddBookAsync_ShouldReturnBook_WhenBookModelIsValid()
    {
        // Arrange
        CancellationToken cancellationToken = default;

        var bookModel = new Book();

        _bookRepositoryMock.Setup(repo => repo.ReadByIsbnAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Book)null);

        _bookRepositoryMock.Setup(repo => repo.AuthorExists(It.IsAny<Guid>()))
            .ReturnsAsync(true);

        _bookRepositoryMock.Setup(repo => repo.AddAuthorToBook(It.IsAny<Guid>(), It.IsAny<List<Guid>>(), cancellationToken))
         .Returns(Task.CompletedTask);

        _bookRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(bookModel);

        _mapperMock.Setup(m => m.Map<Book,BookCreateDto>(bookModel)).Returns(bookCreateDto);

        // Act
        var result = await _bookService.AddBookAsync(bookCreateDto);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(bookCreateDto);
    }

    [Fact]
    public async Task GetBookByIdAsync_ShouldReturnBook_WhenBookExists()
    {
        // Arrange
        var id = Guid.NewGuid();

        var bookModel = new Book { Id = id };
        var bookReadDto = new BookReadDto { Id = id };

        _bookRepositoryMock.Setup(r => r.ReadByIdAsync(id, CancellationToken.None)).ReturnsAsync(bookModel);

        _mapperMock.Setup(m => m.Map<Book, BookReadDto>(bookModel)).Returns(bookReadDto);

        // Act
        var result = await _bookService.GetBookByIdAsync(id);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(bookReadDto);
        _bookRepositoryMock.Verify(r => r.ReadByIdAsync(id, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetBookByIsbnAsync_ShouldReturnBook_WhenBookExists()
    {
        // Arrange
        var isbn = "2358795465125";

        var bookModel = new Book { Isbn = isbn };
        var bookReadDto = new BookReadDto { Isbn = isbn };

        _bookRepositoryMock.Setup(r => r.ReadByIsbnAsync(isbn, CancellationToken.None)).ReturnsAsync(bookModel);

        _mapperMock.Setup(m => m.Map<Book, BookReadDto>(bookModel)).Returns(bookReadDto);

        // Act
        var result = await _bookService.GetBookByIsbnAsync(isbn);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(bookReadDto);
        _bookRepositoryMock.Verify(r => r.ReadByIsbnAsync(isbn, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetBooks_ShouldReturnBooks()
    {
        // Arrange
        var bookModels = new List<Book> { new Book(), new Book() };
        var bookReadDtos = new List<BookReadDto> { new BookReadDto(), new BookReadDto() };

        _bookRepositoryMock.Setup(r => r.ReadAllAsync()).Returns(Task.FromResult(bookModels));

        _mapperMock.Setup(m => m.Map<List<BookReadDto>>(bookModels)).Returns(bookReadDtos);

        // Act
        var result = await _bookService.GetBooksAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(bookReadDtos.Count);
        _bookRepositoryMock.Verify(r => r.ReadAllAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateBookAsync_ShouldUpdateBook_WhenBookExists()
    {
        // Arrange
        var id = Guid.NewGuid();

        var bookModel = new Book();

        _bookRepositoryMock.Setup(repo => repo.ReadByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(bookModel);

        _bookRepositoryMock.Setup(repo => repo.ReadByIsbnAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Book)null);

        _bookRepositoryMock.Setup(repo => repo.AuthorExists(It.IsAny<Guid>()))
            .ReturnsAsync(true);

        _bookRepositoryMock.Setup(repo => repo.UpdateAsync(id, It.IsAny<Book>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(bookModel);

        _mapperMock.Setup(m => m.Map<Book, BookCreateDto>(bookModel)).Returns(bookCreateDto);

        // Act
        var result = await _bookService.UpdateBookAsync(id, bookCreateDto);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(bookCreateDto);
        _bookRepositoryMock.Verify(repo => repo.UpdateAsync(id, It.IsAny<Book>(), It.IsAny<CancellationToken>()), Times.Once);
    }


    [Fact]
    public async Task DeleteBookAsync_ShouldDeleteBook_WhenBookExists()
    {
        // Arrange
        var id = Guid.NewGuid();

        var bookModel = new Book();
        var bookReadDto = new BookReadDto();

        _bookRepositoryMock.Setup(r => r.DeleteAsync(id, CancellationToken.None)).ReturnsAsync(bookModel);

        _mapperMock.Setup(m => m.Map<Book, BookReadDto>(bookModel)).Returns(bookReadDto);

        // Act
        var result = await _bookService.DeleteBookAsync(id);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(bookReadDto);
        _bookRepositoryMock.Verify(r => r.DeleteAsync(id, CancellationToken.None), Times.Once);
    }

}