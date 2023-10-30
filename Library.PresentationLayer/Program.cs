using System.Text.Json.Serialization;
using Library.DataLayer.Context;
using Library.DataLayer.Repository.Interfaces;
using Library.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using Library.DataLayer.Models;
using Library.BusinessLayer.Services.Interfaces;
using Library.BusinessLayer.Services;
using Library.PresentationLayer.Extensions;
using Library.PresentationLayer.Middlewares;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LibraryContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<JwtOptionsModel>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();


var jwtOptions = builder.Configuration.GetSection("Jwt")
    .Get<JwtOptionsModel>();

builder.Services.AddAuthenticationWithJwtBearer(jwtOptions);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();

builder.Services.AddValidators();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithJwtSecurity();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorExceptionHandling>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();