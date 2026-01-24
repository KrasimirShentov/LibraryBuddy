using LibraryBuddy.Domain.Entities;
using LibraryBuddy.ViewModels.Books;

namespace LibraryBuddy.ViewModels.Books;
public class BookListVm
{
    public List<Book> Items { get; set; } = new();

    public BookFilterVm Filter { get; set; } = new();

    public int Page { get; set; }

    public int TotalPages { get; set; }
}
