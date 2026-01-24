using LibraryBuddy.Data;
using LibraryBuddy.Domain.Entities;
using LibraryBuddy.Domain.Enums;
using LibraryBuddy.Services.Interfaces;
using LibraryBuddy.ViewModels.Common;
using Microsoft.EntityFrameworkCore;

namespace LibraryBuddy.Services.Implementations;

public class BookService : IBookService
{
    private readonly LibraryBuddyDbContext _db;

    public BookService(LibraryBuddyDbContext db) => _db = db;

    public Task<Book?> GetByIdAsync(int id) =>
        _db.Books.Include(b => b.Loans).FirstOrDefaultAsync(b => b.Id == id);

    public async Task<(PagedResult<Book> Result, IReadOnlyList<string> Genres)> SearchAsync(
    string? q,
    string? genre,
    BookStatus? status,
    int page,
    int pageSize)
    {
        var query = _db.Books.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.Trim();
            query = query.Where(b =>
                b.Title.Contains(term) || b.Author.Contains(term));
        }

        if (!string.IsNullOrWhiteSpace(genre))
            query = query.Where(b => b.Genre == genre);

        if (status is not null)
            query = query.Where(b => b.Status == status);

        var totalItems = await query.CountAsync();

        var items = await query
            .OrderBy(b => b.Title)
            .ThenBy(b => b.Author)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var genres = await _db.Books
            .AsNoTracking()
            .Select(b => b.Genre)
            .Distinct()
            .OrderBy(g => g)
            .ToListAsync();

        return (
            new PagedResult<Book>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems
            },
            genres
        );
    }


    public async Task<Book> CreateAsync(Book book)
    {
        _db.Books.Add(book);
        await _db.SaveChangesAsync();
        return book;
    }

    public async Task<bool> UpdateAsync(Book book)
    {
        var existing = await _db.Books.FirstOrDefaultAsync(b => b.Id == book.Id);
        if (existing is null) return false;

        existing.Title = book.Title;
        existing.Author = book.Author;
        existing.Genre = book.Genre;
        existing.Description = book.Description;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == id);
        if (book is null) return false;

        _db.Books.Remove(book);
        await _db.SaveChangesAsync();
        return true;
    }
}
