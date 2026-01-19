using LibraryBuddy.Domain.Entities;
using LibraryBuddy.Domain.Enums;

namespace LibraryBuddy.Services.Interfaces;

public interface IBookService
{
    Task<Book?> GetByIdAsync(int id);
    Task<(IReadOnlyList<Book> Items, IReadOnlyList<string> Genres)> SearchAsync(
        string? q, string? genre, BookStatus? status);

    Task<Book> CreateAsync(Book book);
    Task<bool> UpdateAsync(Book book);
    Task<bool> DeleteAsync(int id);
}
