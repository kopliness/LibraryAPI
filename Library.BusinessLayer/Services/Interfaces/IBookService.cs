﻿using Library.BusinessLayer.Dto;
using Library.DataLayer.Models;

namespace Library.BusinessLayer.Services.Interfaces
{
    public interface IBookService
    {
        Task<BookCreateDto> AddBookAsync(BookCreateDto bookCreateDto, CancellationToken cancellationToken = default);
        List<BookReadDto> GetBooks();

        Task<BookReadDto?> GetBookByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<BookReadDto?> GetBookByIsbnAsync(string isbn, CancellationToken cancellationToken = default);

        Task<BookCreateDto?> UpdateBookAsync(Guid id, BookCreateDto bookCreateDto, CancellationToken cancellationToken = default);
        Task<BookReadDto?> DeleteBookAsync(Guid id, CancellationToken cancellationToken = default);
    }
}