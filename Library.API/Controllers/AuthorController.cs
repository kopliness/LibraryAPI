using Library.Business.Dto;
using Library.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.API.Controllers;

[ApiController]
[Authorize]
[Route("author")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all authors", Description = "Get a list of all authors")]
    [SwaggerResponse(200, "Returns a list of AuthorReadDto", typeof(List<AuthorReadDto>))]
    [SwaggerResponse(401, "If user is not authorized")]
    [SwaggerResponse(500, "If there is an internal server error")]
    public async Task<IActionResult> GetAuthors()
    {
        return Ok(await _authorService.GetAuthorsAsync());
    }


    [HttpPost]
    [SwaggerOperation(Summary = "Add a author", Description = "Add a specific author")]
    [SwaggerResponse(201, "Returns the added author", typeof(AuthorCreateDto))]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(401, "If user is not authorized")]
    [SwaggerResponse(500, "If there is an internal server error")]
    public async Task<IActionResult> AddAuthor(AuthorCreateDto authorCreateDto,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var author = await _authorService.AddAuthorAsync(authorCreateDto, cancellationToken);

        return Created($"/api/authors/{author}", author);
    }

    [HttpPut("{id:guid}")]
    [SwaggerOperation(Summary = "Update a author", Description = "Update a specific author")]
    [SwaggerResponse(200, "Returns the updated author", typeof(AuthorCreateDto))]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(401, "If user is not authorized")]
    [SwaggerResponse(404, "If a author with the specified ID is not found")]
    [SwaggerResponse(500, "If there is an internal server error")]
    public async Task<IActionResult> UpdateAuthor(Guid id, AuthorCreateDto authorCreateDto,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var author = await _authorService.UpdateAuthorAsync(id, authorCreateDto, cancellationToken);

        return Ok(author);
    }

    [HttpDelete("{id:guid}")]
    [SwaggerOperation(Summary = "Delete a author", Description = "Delete a specific author")]
    [SwaggerResponse(200, "Author deleted successfully")]
    [SwaggerResponse(401, "If user is not authorized")]
    [SwaggerResponse(404, "If a author with the specified ID is not found")]
    [SwaggerResponse(500, "If there is an internal server error")]
    public async Task<IActionResult> DeleteAuthor(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var author = await _authorService.DeleteAuthorAsync(id, cancellationToken);

        return Ok(author);
    }
}