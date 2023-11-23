namespace Library.Tests.UnitTests.Services;

public class AuthorServiceTests
{
    private readonly Mock<IAuthorRepository> _authorRepositoryMock;
    private readonly AuthorService _authorService;
    private readonly Mock<ILogger<AuthorService>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;

    private readonly AuthorCreateDto authorCreateDto = new()
    {
        FirstName = "TestName",
        LastName = "TestName"
    };

    public AuthorServiceTests()
    {
        _authorRepositoryMock = new Mock<IAuthorRepository>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<AuthorService>>();

        _authorService = new AuthorService(_authorRepositoryMock.Object, _mapperMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task GetAuthors_ShouldReturnAuthors()
    {
        // Arrange
        var authors = new List<Author> { new(), new() };
        var authorReadDtos = new List<AuthorReadDto> { new(), new() };

        _authorRepositoryMock.Setup(r => r.ReadAllAsync()).Returns(Task.FromResult(authors));
        _mapperMock.Setup(m => m.Map<List<AuthorReadDto>>(authors)).Returns(authorReadDtos);

        // Act
        var result = await _authorService.GetAuthorsAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(authorReadDtos.Count);
        _authorRepositoryMock.Verify(r => r.ReadAllAsync(), Times.Once);
    }

    [Fact]
    public async Task AddAuthorAsync_ShouldAddAuthor_WhenAuthorModelIsValid()
    {
        // Arrange
        var authorModel = new Author();

        _authorRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Author>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(authorModel);

        _mapperMock.Setup(m => m.Map<Author, AuthorCreateDto>(authorModel)).Returns(authorCreateDto);

        // Act
        var result = await _authorService.AddAuthorAsync(authorCreateDto);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(authorCreateDto);
        _authorRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Author>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task UpdateAuthorAsync_ShouldUpdateAuthor_WhenAuthorExists()
    {
        // Arrange
        var id = Guid.NewGuid();

        var authorModel = new Author();

        _authorRepositoryMock.Setup(repo => repo.ReadAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(authorModel);

        _authorRepositoryMock.Setup(repo => repo.UpdateAsync(id, It.IsAny<Author>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(authorModel);

        _mapperMock.Setup(m => m.Map<Author, AuthorCreateDto>(authorModel)).Returns(authorCreateDto);

        // Act
        var result = await _authorService.UpdateAuthorAsync(id, authorCreateDto);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(authorCreateDto);
        _authorRepositoryMock.Verify(repo => repo.UpdateAsync(id, It.IsAny<Author>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task DeleteAuthorAsync_ShouldDeleteAuthor_WhenAuthorExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        var authorModel = new Author();
        var authorReadDto = new AuthorReadDto();

        _authorRepositoryMock.Setup(r => r.DeleteAsync(id, CancellationToken.None)).ReturnsAsync(authorModel);
        _mapperMock.Setup(m => m.Map<Author, AuthorReadDto>(authorModel)).Returns(authorReadDto);

        // Act
        var result = await _authorService.DeleteAuthorAsync(id);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(authorReadDto);
        _authorRepositoryMock.Verify(r => r.DeleteAsync(id, CancellationToken.None), Times.Once);
    }
}