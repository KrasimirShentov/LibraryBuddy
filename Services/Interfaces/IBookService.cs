using LibraryBuddy.Domain.Entities;
using LibraryBuddy.Domain.Enums;
using LibraryBuddy.ViewModels.Common;

namespace LibraryBuddy.Services.Interfaces;

public interface IBookService
{
    Task<Book?> GetByIdAsync(int id);
    Task<(PagedResult<Book> Result, IReadOnlyList<string> Genres)> SearchAsync(
        string? q,
        string? genre,
        BookStatus? status,
        int page,
        int pageSize);

    Task<Book> CreateAsync(Book book);
    Task<bool> UpdateAsync(Book book);
    Task<bool> DeleteAsync(int id);
}
