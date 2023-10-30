using Library.BusinessLayer.Dto;
using Library.BusinessLayer.Services.Interfaces;
using Library.BusinessLayer.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.PresentationLayer.Controllers;
[ApiController]
[Authorize]
[Route("author")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;
    private readonly AuthorValidator _authorValidator;
    
    public AuthorController(IAuthorService authorService, AuthorValidator authorValidator)
    {
        _authorService = authorService;
        _authorValidator = authorValidator;
    }
    
    [HttpGet]
    public Task<IActionResult> GetAuthors() => Task.FromResult<IActionResult>(Ok( _authorService.GetAuthors()));
    
    [HttpPost]
    public async Task<IActionResult> AddAuthor(AuthorCreateDto authorCreateDto, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
            
        var validationResult = _authorValidator.Validate(authorCreateDto);
            
        if(!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var author = await _authorService.AddAuthorAsync(authorCreateDto, cancellationToken);

        return Ok(author);
    }
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAuthor(Guid id, AuthorCreateDto authorCreateDto,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
            
        var validationResult = _authorValidator.Validate(authorCreateDto);
  
        if(!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var author = await _authorService.UpdateAuthorAsync(id, authorCreateDto, cancellationToken);

        return Ok(author);
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAuthor(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
            
       var author = await _authorService.DeleteAuthorAsync(id, cancellationToken);

        return Ok(author);
    }
}