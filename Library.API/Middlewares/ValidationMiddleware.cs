using System.Text;
using Library.Business.Dto;
using Library.Business.Validation;
using Newtonsoft.Json;

namespace Library.API.Middlewares;

public class ValidationMiddleware
{
    private readonly AccountValidator _accountValidator;
    private readonly AuthorValidator _authorValidator;
    private readonly BookValidator _bookValidator;
    private readonly IsbnValidator _isbnValidator;
    private readonly RequestDelegate _next;

    public ValidationMiddleware(RequestDelegate next, BookValidator bookValidator, IsbnValidator isbnValidator,
        AuthorValidator authorValidator, AccountValidator accountValidator)
    {
        _next = next;
        _bookValidator = bookValidator;
        _isbnValidator = isbnValidator;
        _authorValidator = authorValidator;
        _accountValidator = accountValidator;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        var request = httpContext.Request;

        if (request.Path.StartsWithSegments("/book") && request.Method == HttpMethods.Get &&
            request.Path.Value.EndsWith("/isbn"))
        {
            var isbn = request.RouteValues["isbn"].ToString();
            var validationResult = _isbnValidator.Validate(isbn);

            if (!validationResult.IsValid)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsJsonAsync(validationResult.Errors);
                return;
            }
        }
        else if (request.Path.StartsWithSegments("/account"))
        {
            var accountDto = new AccountDto
            {
                Login = request.Query["Login"],
                Password = request.Query["Password"]
            };
            var validationResult = _accountValidator.Validate(accountDto);

            if (!validationResult.IsValid)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsJsonAsync(validationResult.Errors);
                return;
            }
        }
        else if (request.Method == HttpMethods.Post && request.ContentLength > 0)
        {
            request.EnableBuffering();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            var requestContent = Encoding.UTF8.GetString(buffer);

            if (request.Path.StartsWithSegments("/author"))
            {
                var authorCreateDto = JsonConvert.DeserializeObject<AuthorCreateDto>(requestContent);
                var validationResult = _authorValidator.Validate(authorCreateDto);

                if (!validationResult.IsValid)
                {
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await httpContext.Response.WriteAsJsonAsync(validationResult.Errors);
                    return;
                }
            }
            else if (request.Path.StartsWithSegments("/book"))
            {
                if (request.Method == HttpMethods.Post)
                {
                    var bookCreateDto = JsonConvert.DeserializeObject<BookCreateDto>(requestContent);
                    var validationResult = _bookValidator.Validate(bookCreateDto);

                    if (!validationResult.IsValid)
                    {
                        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await httpContext.Response.WriteAsJsonAsync(validationResult.Errors);
                        return;
                    }
                }
            }

            request.Body.Position = 0;
        }


        await _next(httpContext);
    }
}