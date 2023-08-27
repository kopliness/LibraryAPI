using Library.Database.Context;
using Library.Database.Repository.Interfaces;
using Library.Database.Repository;
using Library.Mapper;
using Microsoft.EntityFrameworkCore;
using Library.Domain.Models;
using Library.Auth.Interfaces;
using Library.Auth;
using Library.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LibraryContext>(opt=>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<JwtOptionsModel>(builder.Configuration.GetSection("Jwt"));

// Add services to the container.
builder.Services.AddAutoMapper(typeof(AppMappingProfile));

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGenerateToken, GenerateToken>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

var jwtOptions = builder.Configuration.GetSection("Jwt")
    .Get<JwtOptionsModel>();

builder.Services.AddAuthenticationWithJwtBearer(jwtOptions);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithJwtSecurity();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
