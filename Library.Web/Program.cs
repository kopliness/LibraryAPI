using Library.Database.Context;
using Library.Database.Repository.Interfaces;
using Library.Database.Repository;
using Library.Mapper;
using Microsoft.EntityFrameworkCore;
using Library.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LibraryContext>(opt=>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<JwtOptionsModel>(builder.Configuration.GetSection("Jwt"));

// Add services to the container.
builder.Services.AddAutoMapper(typeof(AppMappingProfile));

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
